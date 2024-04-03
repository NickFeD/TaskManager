using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using TaskManager.Core.Contracts.Services;
using TaskManager.Core.Entities;
using TaskManager.Core.Models;

namespace TaskManager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController(IJwtService jwtServices, IUserService userService, IUserRefreshTokenService userRefreshTokenService) : ControllerBase
    {
        private readonly IJwtService _jwtServices = jwtServices;
        private readonly IUserService _userService = userService;
        private readonly IUserRefreshTokenService _userRefreshTokenService = userRefreshTokenService;

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
            return Ok(authResponse);
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

            var userRefreshToken = await _userRefreshTokenService.GetFirstIsValidate(true, request.ExpiredToken, request.RefreshToken, ipAddress);

            Response<AuthResponse> response = ValidateDetails(token, userRefreshToken);
            if (!response.IsSuccess)
                return BadRequest(response.Reason);
            userRefreshToken.IsInvalidated = true;
            await _userRefreshTokenService.UpdateAsync(userRefreshToken);

            var email = token.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Email)?.Value;
            if (email is null)
                return BadRequest("The token is damaged");

            var authResponse = await _jwtServices.GetRefreshTokenAsync(ipAddress, userRefreshToken.UserId, email);
            return Ok(authResponse);
        }

        [HttpPost("[action]")]
        [ProducesResponseType(typeof(AuthResponse), StatusCodes.Status200OK)]
        [ProducesDefaultResponseType(typeof(string))]
        public async Task<IActionResult> Registration(RegistrationModel model)
        {
            var modelToCreate = await _userService.Registration(model);
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
            JwtSecurityTokenHandler tokenHandler = new();
            return tokenHandler.ReadJwtToken(expiredToken);
        }
    }
}
