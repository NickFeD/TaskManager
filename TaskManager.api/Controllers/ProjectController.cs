using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Api.Data;
using TaskManager.Api.Entity;
using TaskManager.Api.Models.Abstracted;
using TaskManager.Api.Services;
using TaskManager.Command.Models;

namespace TaskManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController :CRUDController<ProjectModel,ProjectService>
    {
        //private readonly ParticipantService _participantService;

        public ProjectController(ApplicationContext context) : base(new(context))
        {
            //_participantService = new(context);
        }

        [HttpGet("{userId}")]
        public ActionResult<List<ProjectModel>> GetByUserId(int userId)
        {
            var projects = _service.GetProjectsByUserId(userId).ToList();
            var models = projects.Select(u => (ProjectModel)u).ToList();
            return models is null ? NotFound() : models;
        }


        //[HttpPost("/{projectId}/Users/{userId}")]
        //public IActionResult AddUsers(int projectId,int userId)
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
