using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Api.Data;
using TaskManager.Api.Models;
using TaskManager.Api.Services;
using TaskManager.Command.Models;

namespace TaskManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly ProjectService _projectService;

        public ProjectController(ApplicationContext context)
        {
            _projectService = new(context);
        }

        [HttpGet("{userId}")]
        public ActionResult<ProjectModel> GetByUserId(int userId)
        {
            Project[] model = _projectService.GetProjectsByUserId(userId).ToArray();
            return model is null ? NotFound() : (ProjectModel)model[0];
        }

        [HttpPost("{userId}")]
        public IActionResult Create(int userId ,ProjectModel project) 
        {
            _projectService.Create(userId,(Project)project);
            return CreatedAtAction(nameof(GetByUserId), new { id = project.CreatorId }, project);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id,ProjectModel projectModel) 
        {
            var existingModel = _projectService.GetById(id);
            if (existingModel == null)
                return NotFound();
            _projectService.Update(projectModel);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var modelToDelete = _projectService.GetById(id);
            if (modelToDelete == null)
                return NotFound();
            _projectService.Delete(id);
            return NoContent();
        }
    }
}
