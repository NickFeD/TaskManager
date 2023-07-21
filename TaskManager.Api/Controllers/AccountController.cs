using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using TaskManager.Api.Data;
using TaskManager.Api.Entity;
using TaskManager.Api.Services.Abstracted;
using TaskManager.Command.Models;
using RouteAttribute = Microsoft.AspNetCore.Components.RouteAttribute;

namespace TaskManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IJwtServices _jwtServices;
        private readonly ApplicationContext _context;

        public AccountController(IJwtServices jwtServices, ApplicationContext context)
        {
            _jwtServices = jwtServices;
            _context = context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="authRequest"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AuthResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(AuthResponse), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> AuthToken([FromBody] AuthRequest authRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(new AuthResponse { IsSuccess = false, Reason = "Email and password most by provided." });
            var authResponse = await _jwtServices.GetTokenAsync(authRequest, HttpContext.Connection.RemoteIpAddress.ToString());
            if (authResponse is null)
                return Unauthorized(new AuthResponse { IsSuccess = false, Reason = "The email or password is incorrect" });
            return Ok(authResponse);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AuthResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(new AuthResponse { IsSuccess = false, Reason = "Tokens must by provided." });
            var token = GetJwtToken(request.ExpiredToken);
            var ipAddress = HttpContext.Connection.RemoteIpAddress.ToString();
            var userRefreshToken = _context.UserRefreshTokens.FirstOrDefault(
                x => x.IsInvalidated == false && x.Token == request.ExpiredToken
                && x.RefreshToken == request.RefreshToken
                && x.IpAddress == ipAddress);
            AuthResponse response = ValidateDetails(token, userRefreshToken);
            if (!response.IsSuccess)
                return BadRequest(response);

            userRefreshToken.IsInvalidated = true;
            _context.UserRefreshTokens.Update(userRefreshToken);
            await _context.SaveChangesAsync();

            var email = token.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Email).Value;
            var authResponse = await _jwtServices.GetRefreshTokenAsync(ipAddress, userRefreshToken.UserId, email);
            return Ok(authResponse);
        }

        private AuthResponse ValidateDetails(JwtSecurityToken token, UserRefreshToken? userRefreshToken)
        {
            if (userRefreshToken is null)
                return new AuthResponse { IsSuccess = false, Reason = "Invalid token details." };
            if (token.ValidTo > DateTime.UtcNow)
                return new AuthResponse { IsSuccess = false, Reason = "Token not expired." };
            if (!userRefreshToken.IsActive)
                return new AuthResponse { IsSuccess = false, Reason = "Refresh Token expired." };
            return new AuthResponse { IsSuccess = true, };
        }

        private JwtSecurityToken GetJwtToken(string expiredToken)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.ReadJwtToken(expiredToken);
        }
    }
}
