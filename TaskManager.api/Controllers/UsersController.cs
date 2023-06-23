using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using TaskManager.Api.Data;
using TaskManager.Api.Entity;
using TaskManager.Api.Controllers.Abstracted;
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

        [HttpGet("{userId}/projects")]
        public ActionResult<List<ProjectModel>> GetByUserId(int userId)
        {
            var projects = _service.GetProjectsByUserId(userId).ToList();
            var models = projects.Select(u => (ProjectModel)u).ToList();
            return models is null ? NotFound() : models;
        }
    }
}
