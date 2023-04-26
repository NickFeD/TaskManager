using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Api.Models;
using TaskManager.Api.Models.Data;
using TaskManager.Api.Models.Services;
using TaskManager.Command.Models;

namespace TaskManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Admin")]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationContext _db;
        private readonly UsersService _userServices;
        public UsersController(ApplicationContext db)
        {
            _db = db;
            _userServices = new(_db);
        }

        [HttpGet("test")]
        [AllowAnonymous]
        public IActionResult TestApi()
        {
            return Ok($"{true}");
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] UserModel userModel)
        {
            if (userModel is null)
                return BadRequest();
           return _userServices.Create(userModel)? Ok() : NotFound();
        }

        [HttpPatch("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] UserModel userModel)
        {
            if (userModel is null)
                return BadRequest();
            return _userServices.Update(id,userModel)? Ok() : NotFound();

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            return _userServices.Delete(id)? Ok(): NotFound();
        }

        [HttpGet]
        public async Task<IEnumerable<UserModel>> GetUsers() 
        {
           return await _db.Users.Select(x => x.ToDto()).ToListAsync();
        }

        [HttpPost("all")]
        public  IActionResult CreateMultipleUsers([FromBody] List<UserModel> userModels)
        {
            if (userModels is null && userModels.Count < 0)
                return BadRequest();
            return _userServices.CreateMultipleUsers(userModels)? Ok(): BadRequest();
        }
    }
}
