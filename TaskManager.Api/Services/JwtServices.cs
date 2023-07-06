using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using TaskManager.Api.Data;
using TaskManager.Api.Entity;
using TaskManager.Api.Services.Abstracted;
using TaskManager.Command.Models;
using Task = System.Threading.Tasks.Task;

namespace TaskManager.Api.Services
{
    public class JwtServices : IJwtServices
    {
        private readonly ApplicationContext _context;
        private readonly IConfiguration _configuration;
        private readonly DateTime _expiresToken = DateTime.UtcNow.AddHours(4);
        private readonly DateTime _expiresRefreshToken = DateTime.UtcNow.AddMonths(2);

        public JwtServices(ApplicationContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        #region Public Metod
        public async Task<AuthResponse> GetRefreshTokenAsync(string ipAddress, int userId, string email)
        {
            var refreshToken = GenerateRefreshToken();
            var accessToken = GenerateToken(email);

            return await SaveTokenDetails(ipAddress, userId, accessToken, refreshToken);
        }

        public async Task<AuthResponse> GetTokenAsync(AuthRequest authRequest, string ipAddress)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email.Equals(authRequest.Email)
            && u.Password.Equals(authRequest.Password));
            if (user is null)
                return await Task.FromResult<AuthResponse>(null);
            string tokenString = GenerateToken(user.Email);
            string refreshTokenString = GenerateRefreshToken();
            return await SaveTokenDetails(ipAddress, user.Id, tokenString, refreshTokenString);
        }

        public Task<bool> IsTokenValid(string accessToken, string ipAddress)
        {
            var isValid = _context.UserRefreshTokens.FirstOrDefault(x => x.Token == accessToken
            && x.IpAddress == ipAddress) != null;
            return Task.FromResult(isValid);
        }
        #endregion
        #region Private Metod
        private async Task<AuthResponse> SaveTokenDetails(string ipAddress, int userId, string tokenString, string refreshTokenString)
        {
            var refreshToken = _context.UserRefreshTokens.FirstOrDefault(x => x.IpAddress == ipAddress && x.UserId == userId);
            if (refreshToken is null)
            {
                refreshToken = new UserRefreshToken
                {
                    CreatedDate = DateTime.UtcNow,
                    IpAddress = ipAddress,
                    IsInvalidated = false,
                    UserId = userId,
                };
                await _context.UserRefreshTokens.AddAsync(refreshToken);
            }
            refreshToken.ExpirationDate = _expiresRefreshToken;
            refreshToken.RefreshToken = refreshTokenString;
            refreshToken.Token = tokenString;

            await _context.SaveChangesAsync();
            return await Task.FromResult(new AuthResponse
            {
                Token = tokenString,
                RefreshToken = refreshTokenString,
                IsSuccess = true,
                ExpiresRefreshToken = _expiresRefreshToken,
                ExpiresToken = _expiresToken,
            });
        }

        private string GenerateRefreshToken()
        {
            var byteArray = RandomNumberGenerator.GetBytes(64);
            return Convert.ToBase64String(byteArray);
        }

        private string GenerateToken(string userEmail)
        {
            var jwtKey = _configuration.GetValue<string>("JwtSettings:Key");
            var keyByte = Encoding.ASCII.GetBytes(jwtKey);

            var tokenHandler = new JwtSecurityTokenHandler();

            var descriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, userEmail)
                }),
                Expires = _expiresToken,

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyByte),
                SecurityAlgorithms.HmacSha256),
            };

            var token = tokenHandler.CreateToken(descriptor);
            string tokenString = tokenHandler.WriteToken(token);
            return tokenString;
        }

        #endregion
    }
}
