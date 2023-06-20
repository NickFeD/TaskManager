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

        public UsersController(ApplicationContext context):base(new(context))
        { 
        }


        [HttpGet]
        public ActionResult<IEnumerable<UserModel>> GetAll()
        {
            return Ok(_service.GetAll().Select(u => (UserModel)u));
        }
    }
}
