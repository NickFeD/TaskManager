using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Api.Controllers.Abstracted;
using TaskManager.Api.Data;
using TaskManager.Api.Services;
using TaskManager.Command.Models;
using Task = System.Threading.Tasks.Task;

namespace TaskManager.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase, ICRUDControllers<ProjectModel>
    {
        private readonly ProjectService _service;

        public ProjectsController(ApplicationContext context) { _service = new(context); }

        /// <summary>
        /// Create a project
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<IActionResult> Create([FromBody] ProjectModel model)
        {
            if (model is null)
                return Task.FromResult((IActionResult)BadRequest());
            var modelToCreate = _service.Create(model);
            if (modelToCreate is null)
                return Task.FromResult((IActionResult)BadRequest());
            return Task.FromResult((IActionResult)CreatedAtAction(nameof(GetById), new { id = modelToCreate.Id }, modelToCreate));
        }

        /// <summary>
        /// Delete a project
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public Task<IActionResult> Delete(int id)
        {
            var project = _service.GetById(id);
            if (project is null)
                return Task.FromResult((IActionResult)BadRequest());
            _service.Delete(id);
            return Task.FromResult((IActionResult)NoContent());
        }

        /// <summary>
        /// Get all projects
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public Task<IActionResult> GetAll()
        {
            return Task.FromResult((IActionResult)Ok(_service.GetAll()));
        }

        /// <summary>
        /// Get a project by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public Task<IActionResult> GetById(int id)
        {
            return Task.FromResult((IActionResult)Ok(_service.GetById(id)));
        }

        /// <summary>
        /// Update the project
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        public Task<IActionResult> Update(int id, ProjectModel model)
        {
            _service.Update(model);
            return Task.FromResult((IActionResult)Ok());
        }


        //[HttpPost("/{projectId}/Users/{userId}")]
        //public IActionResult AddUser(int projectId,int userId)
        //{
        //    ProjectParticipant participant = new()
        //    {
        //        UserId = userId,
        //        ProjectId = projectId,
        //    };
        //    _participantService.Create(participant);
        //    return Ok();
        //}
    }
}
