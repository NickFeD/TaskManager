using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManager.Api.Data;
using TaskManager.Api.Services.Abstracted;
using TaskManager.Command.Models;

namespace TaskManager.Api.Services
{
    public class JwtServices : IJwtServices
    {
        private readonly ApplicationContext _context;
        private readonly IConfiguration _configuration;
        private readonly DateTime _expires = DateTime.UtcNow.AddMinutes(1);

        public JwtServices(ApplicationContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<string> GetTokenAsync(AuthRequest authRequest)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email.Equals(authRequest.Email)
            && u.Password.Equals(authRequest.Password));
            if (user is null)
                return await Task.FromResult<string>(null);

            var jwtKey = _configuration.GetValue<string>("JwtSettings:Key");
            var keyByte = Encoding.ASCII.GetBytes(jwtKey);

            var tokenHandler = new JwtSecurityTokenHandler();

            var descriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, user.Email)
                }),
                Expires = _expires,

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyByte),
                SecurityAlgorithms.HmacSha256),
            };

            var token = tokenHandler.CreateToken(descriptor);
            return await Task.FromResult(tokenHandler.WriteToken(token));
        }
    }
}
