using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using TaskManager.Api.Models;
using TaskManager.Api.Models.Data;
using TaskManager.Api.Models.Services;

namespace TaskManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ApplicationContext _db;
        private readonly UsersService _userServices;

        public AccountController(ApplicationContext db)
        {
            _db = db;
            _userServices = new(_db);
        }

        [HttpGet("info")]
        public ActionResult GetCurreutUserInfo()
        {
            string username = HttpContext.User.Identity.Name;
            var user = _db.Users.FirstOrDefault(u => u.Email == username);
            if (user is null)
                return NotFound();
            return Ok(user.ToDto());
        }
        [HttpPost("token")]
        public IActionResult GetToken()
        {
            (string name,string pass) = _userServices.GetUserLoginPassFromBasicAuth(Request);

            var identity = _userServices.GetClaimsIdentity(name, pass);
            if (identity.Claims is null)
                return NotFound();
            var now = DateTime.Now;
            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                notBefore: now,
                claims: identity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                signingCredentials: new(AuthOptions.GetSymmetricSecurityKey(),SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            var response = new
            {
                access_token = encodedJwt,
                username = identity.Name,
            };
            return Ok(response);
        }
    }
}
