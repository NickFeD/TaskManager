using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using TaskManager.Api.Data;
using TaskManager.Api.Entity;
using TaskManager.Api.Models.Abstracted;
using TaskManager.Api.Services;
using TaskManager.Command.Models;

namespace TaskManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController: CRUDController<UserModel,UserService>
    {
        Initialization initialization;
        public UsersController(ApplicationContext context):base(new(context))
        { 
            initialization = new Initialization(context);
        }

        [HttpGet("test")]
        public IActionResult Test() 
        {
            return Ok("OK Top");
        }

        [HttpGet("dbFull/{num}")]
        public IActionResult DbFull(int num)
        {
            initialization.InitializationDb(num);
            return Ok("DbFull");
        }

        [HttpGet]
        public ActionResult<IEnumerable<UserModel>> GetAll()
        {
            return Ok(_service.GetAll().Select(u => (UserModel)u));
        }
    }
}
