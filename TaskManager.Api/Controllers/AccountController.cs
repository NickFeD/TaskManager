using TaskManager.Api.Services.Abstracted;
using System.IdentityModel.Tokens.Jwt;
using TaskManager.Command.Models;
using TaskManager.Api.Services;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Api.Entity;
using TaskManager.Api.Data;
using TaskManager.Api.Exceptions;
using System.Net;

namespace TaskManager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController(IJwtServices jwtServices, ApplicationContext context) : ControllerBase
    {
        private readonly IJwtServices _jwtServices = jwtServices;
        private readonly UserService _userService = new(context);
        private readonly ApplicationContext _context = context;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="authRequest"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType(typeof(string))]
        public async Task<IActionResult> AuthToken([FromBody] AuthRequest authRequest)
        {
            var authResponse = await _jwtServices.GetTokenAsync(authRequest, ipAddress: HttpContext.Connection.RemoteIpAddress!.ToString());
            if (authResponse is null)
                return BadRequest("The email or password is incorrect");
            return Ok(authResponse.Model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("RefreshToken")]
        [ProducesResponseType(typeof(AuthResponse), StatusCodes.Status200OK)]
        [ProducesDefaultResponseType(typeof(string))]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            JwtSecurityToken? token;
            try
            {
                token = GetJwtToken(request.ExpiredToken);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
            if (ipAddress is null)
                return BadRequest("Your ip could not be determined");
            var userRefreshToken = _context.UserRefreshTokens.FirstOrDefault(
                x => x.IsInvalidated == true && x.Token == request.ExpiredToken
                && x.RefreshToken == request.RefreshToken
                && x.IpAddress == ipAddress);
            if (userRefreshToken is null)
                return BadRequest("ERROR");

            Response<AuthResponse> response = ValidateDetails(token, userRefreshToken);
            if (!response.IsSuccess)
                return BadRequest(response.Reason);
            userRefreshToken.IsInvalidated = true;
            _context.UserRefreshTokens.Update(userRefreshToken);
            await _context.SaveChangesAsync();

            var email = token.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Email)?.Value;
            if (email is null)
                return BadRequest("The token is damaged");

            var authResponse = await _jwtServices.GetRefreshTokenAsync(ipAddress, userRefreshToken.UserId, email);
            if (!authResponse.IsSuccess)
                return BadRequest(authResponse.Reason);
            return Ok(authResponse.Model);
        }

        [HttpPost("[action]")]
        [ProducesResponseType(typeof(AuthResponse), StatusCodes.Status200OK)]
        [ProducesDefaultResponseType(typeof(string))]
        public async Task<IActionResult> Registration(RegistrationModel model)
        {
            var user = new User()
            { 
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Phone = model.Phone,
                RegistrationDate = DateTime.UtcNow,
                LastLoginData = DateTime.UtcNow,
                Password = model.Password,
            };
            var modelToCreate = await _userService.Registration(user);
            return await AuthToken(new() { Email = model.Email, Password = model.Password });
        }

        private static Response<AuthResponse> ValidateDetails(JwtSecurityToken token, UserRefreshToken? userRefreshToken)
        {
            if (userRefreshToken is null)
                return new Response<AuthResponse> { IsSuccess = false, Reason = "Invalid token details." };
            if (token.ValidTo > DateTime.UtcNow)
                return new Response<AuthResponse> { IsSuccess = false, Reason = "Token not expired." };
            if (!userRefreshToken.IsActive)
                return new Response<AuthResponse> { IsSuccess = false, Reason = "Refresh Token expired." };
            return new Response<AuthResponse> { IsSuccess = true, };
        }

        private static JwtSecurityToken GetJwtToken(string expiredToken)
        {
            JwtSecurityTokenHandler tokenHandler = new ();
            return tokenHandler.ReadJwtToken(expiredToken);
        }
    }
}
