using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using TaskManager.Core.Contracts.Repository;
using TaskManager.Core.Contracts.Services;
using TaskManager.Core.Entities;
using TaskManager.Core.Exceptions;
using TaskManager.Core.Models;

namespace TaskManager.Infrastructure.Services
{
    public class JwtService(IConfiguration configuration, IUserRepository userRepository, IUserRefreshTokenRepository refreshTokenRepository, IEncryptService encryptService) : IJwtService
    {

        private readonly IUserRepository _userRepository = userRepository;
        private readonly IUserRefreshTokenRepository _refreshTokenRepository = refreshTokenRepository;
        private readonly IEncryptService _encryptService = encryptService;

        private readonly IConfiguration _configuration = configuration;
        private readonly DateTime _expiresToken = DateTime.UtcNow.AddMinutes(1);
        private readonly DateTime _expiresRefreshToken = DateTime.UtcNow.AddMonths(2);

        #region Public Metod
        public async Task<AuthResponse> GetRefreshTokenAsync(string ipAddress, Guid userId, string email)
        {
            var refreshToken = GenerateRefreshToken();
            var accessToken = GenerateToken(email);

            return await SaveTokenDetails(ipAddress, userId, accessToken, refreshToken);
        }

        public async Task<AuthResponse> GetTokenAsync(AuthRequest authRequest, string ipAddress)
        {
            var user = await _userRepository.GetUserByEmail(authRequest.Email);
            var hashPassword = _encryptService.HashPassword(authRequest.Password, user.Salt);

            if (user.Password.SequenceEqual(hashPassword))
                throw new BadRequestException("The email or password is incorrect");

            string tokenString = GenerateToken(user.Email);
            string refreshTokenString = GenerateRefreshToken();
            return await SaveTokenDetails(ipAddress, user.Id, tokenString, refreshTokenString);
        }

        public async Task<User> IsTokenValid(string accessToken, string ipAddress)
        {
            var refreshToken = await _refreshTokenRepository.GetFirstByConditionAsync(x => x.Token == accessToken && x.IpAddress == ipAddress);
            var user = await _userRepository.GetByIdAsync(refreshToken.UserId);
            return user;
        }
        #endregion

        #region Private Metod
        private async Task<AuthResponse> SaveTokenDetails(string ipAddress, Guid userId, string tokenString, string refreshTokenString)
        {
            var refreshToken = await _refreshTokenRepository.GetFirstByConditionAsync(x => x.IpAddress == ipAddress && x.UserId == userId);
            if (refreshToken is null)
            {
                refreshToken = new UserRefreshToken
                {
                    Id = Guid.NewGuid(),
                    CreatedDate = DateTime.UtcNow,
                    IpAddress = ipAddress,
                    IsInvalidated = false,
                    UserId = userId,
                    ExpirationDate = _expiresRefreshToken,
                    RefreshToken = refreshTokenString,
                    Token = tokenString,
                };
                await _refreshTokenRepository.AddAsync(refreshToken);
            }
            else
            {
                refreshToken.ExpirationDate = _expiresRefreshToken;
                refreshToken.RefreshToken = refreshTokenString;
                refreshToken.Token = tokenString;
                await _refreshTokenRepository.UpdateAsync(refreshToken);
            }


            return new AuthResponse
            {
                Token = tokenString,
                RefreshToken = refreshTokenString,
                ExpiresRefreshToken = _expiresRefreshToken,
                ExpiresToken = _expiresToken,
            };
        }

        private static string GenerateRefreshToken()
        {
            var byteArray = RandomNumberGenerator.GetBytes(64);
            return Convert.ToBase64String(byteArray);
        }

        private string GenerateToken(string userEmail)
        {
            var jwtKey = _configuration.GetValue<string>("JwtSettings:Key");
            if (jwtKey is null)
                throw new ArgumentNullException(nameof(jwtKey), "JwtSettings:Key is null");

            var keyByte = Encoding.ASCII.GetBytes(jwtKey);

            var tokenHandler = new JwtSecurityTokenHandler();

            var descriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new(ClaimTypes.Email, userEmail)
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
