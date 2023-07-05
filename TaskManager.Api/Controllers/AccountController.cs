using Microsoft.AspNetCore.Mvc;
using TaskManager.Api.Services.Abstracted;
using TaskManager.Command.Models;

namespace TaskManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IJwtServices _jwtServices;

        public AccountController(IJwtServices jwtServices)
        {
            _jwtServices = jwtServices;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AuthToken([FromBody] AuthRequest authRequest)
        {
            string? token = await _jwtServices.GetTokenAsync(authRequest);
            if (token is null)
                return Unauthorized();
            return Ok( new AuthResponse { Token = token });
        }
    }
}
