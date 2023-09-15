using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using TaskManager.Api.Data;
using TaskManager.Api.Entity;
using TaskManager.Api.Services;
using TaskManager.Api.Services.Abstracted;
using TaskManager.Command.Models;
using Microsoft.AspNetCore.Http;

namespace TaskManager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IJwtServices _jwtServices;
        private readonly UserService _userService;
        private readonly ApplicationContext _context;

        public AccountController(IJwtServices jwtServices, ApplicationContext context)
        {
            _jwtServices = jwtServices;
            _userService = new(context);
            _context = context;
        }

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
            if (!ModelState.IsValid)
                return BadRequest("Email and password most by provided.");
#pragma warning disable CS8602 // Разыменование вероятной пустой ссылки.
            var authResponse = await _jwtServices.GetTokenAsync(authRequest, HttpContext.Connection.RemoteIpAddress.ToString());
#pragma warning restore CS8602 // Разыменование вероятной пустой ссылки.
            if (authResponse is null)
                return Unauthorized("The email or password is incorrect");
            return Ok(authResponse.Model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        [ProducesResponseType(typeof(AuthResponse), StatusCodes.Status200OK)]
        [ProducesDefaultResponseType(typeof(string))]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest("Tokens must by provided.");
            var token = GetJwtToken(request.ExpiredToken);
            var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
            if (ipAddress is null)
                return BadRequest("Your ip could not be determined");
            var userRefreshToken = _context.UserRefreshTokens.FirstOrDefault(
                x => x.IsInvalidated == false && x.Token == request.ExpiredToken
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
        public async Task<IActionResult> Registration(UserModel model)
        {
            if (model is null)
                return BadRequest("You didn't send anything");
            var modelToCreate = await _userService.CreateAsync(model);
            if (!modelToCreate.IsSuccess)
                return BadRequest(modelToCreate.Reason);
            return await AuthToken(new() { Email = model.Email, Password = model.Password });
        }

        private Response<AuthResponse> ValidateDetails(JwtSecurityToken token, UserRefreshToken? userRefreshToken)
        {
            if (userRefreshToken is null)
                return new Response<AuthResponse> { IsSuccess = false, Reason = "Invalid token details." };
            if (token.ValidTo > DateTime.UtcNow)
                return new Response<AuthResponse> { IsSuccess = false, Reason = "Token not expired." };
            if (!userRefreshToken.IsActive)
                return new Response<AuthResponse> { IsSuccess = false, Reason = "Refresh Token expired." };
            return new Response<AuthResponse> { IsSuccess = true, };
        }

        private JwtSecurityToken GetJwtToken(string expiredToken)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.ReadJwtToken(expiredToken);
        }
    }
}
