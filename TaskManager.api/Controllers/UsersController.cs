using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using TaskManager.Api.Data;
using TaskManager.Api.Models;
using TaskManager.Api.Models.Abstracted;
using TaskManager.Api.Services;
using TaskManager.Command.Models;

namespace TaskManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController: ControllerBase
    {
        private readonly UserService _userService;
        public UsersController(ApplicationContext context)
        { 
            _userService = new UserService(context);
        }

        [HttpGet("test")]
        public IActionResult Test() 
        {
            return Ok("OK Top");
        }

        [HttpGet]
        public ActionResult<IEnumerable<UserModel>> GetAll()
        {
            return Ok(_userService.GetAll().Select(u => (UserModel)u));
        }

        [HttpGet("{id}")]
        public ActionResult<UserModel> Get(int id)
        {
            var model =(UserModel)_userService.GetById(id);
            return model is null ? NotFound() : model;
        }

        [HttpPost]
        public IActionResult Create([FromBody] UserModel model)
        {
            _userService.Create((User)model);
            return CreatedAtAction(nameof(Get), new { id = model.Id }, model);
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, UserModel model)
        {
            var existingModel = _userService.GetById(id);
            if (existingModel == null)
                return NotFound();
            _userService.Update((User)model);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var modelToDelete = _userService.GetById(id);
            if (modelToDelete == null)
                return NotFound();
            _userService.Delete(id);
            return NoContent();
        }

    }
}
