using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using TaskManager.Api.Data;
using TaskManager.Api.Entity;
using TaskManager.Api.Controllers.Abstracted;
using TaskManager.Api.Services;
using TaskManager.Command.Models;
using System.Diagnostics;

namespace TaskManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController: CRUDController<UserModel,UserService>
    {

        public UsersController(ApplicationContext context):base(new(context))
        { 
        }

        public override IActionResult Delete(int id)
        {
            var modelToDelete = _service.GetById(id);
            if (modelToDelete == null)
                return NotFound();
            _service.Delete(id);
            return NoContent();
        }
        /// <summary>
        /// Get a project by user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("{userId}/projects")]
        public ActionResult<List<ProjectModel>> GetProjectByUserId(int userId)
        {
            var projects = _service.GetProjectsByUserId(userId).ToList();
            var models = projects.Select(u => (ProjectModel)u).ToList();
            return models is null ? NotFound() : models;
        }
    }
}
