using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Api.Controllers.Abstracted;
using TaskManager.Api.Data;
using TaskManager.Api.Services;
using TaskManager.Command.Models;

namespace TaskManager.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController : ControllerBase, ICRUDController<ProjectModel>
    {
        private readonly ProjectService _service;

        public ProjectsController(ApplicationContext context) { _service = new(context); }

        /// <summary>
        /// Create a project
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(Response<ProjectModel>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Response<ProjectModel>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] ProjectModel model)
        {
            if (model is null)
                return BadRequest(new Response<ProjectModel> { IsSuccess = false, Reason = "Request null" });
            var modelToCreate = await _service.CreateAsync(model);
            if (!modelToCreate.IsSuccess)
                return BadRequest(modelToCreate);
            return CreatedAtAction(nameof(GetById), new { id = modelToCreate.Model.Id }, modelToCreate);
        }

        /// <summary>
        /// Delete a project
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            var project = await _service.GetByIdAsync(id);
            if (!project.IsSuccess)
                return BadRequest(project);
            _service.Delete(id);
            return Ok(project);
        }

        /// <summary>
        /// Get all projects
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(Response<List<ProjectModel>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<List<ProjectModel>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAll()
        {
            var response = await _service.GetAllAsync();
            if (!response.IsSuccess)
                return BadRequest(response);
            return Ok(response);
        }

        /// <summary>
        /// Get a project by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Response<ProjectModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<ProjectModel>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _service.GetByIdAsync(id);
            if (!response.IsSuccess)
                return BadRequest(response);
            return Ok(response);
        }

        /// <summary>
        /// Update the project
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, ProjectModel model)
        {
            var response = await _service.UpdateAsync(model);
            if (!response.IsSuccess)
                return BadRequest(response);
            return Ok(response);
        }


        //[HttpPost("/{projectId}/Users/{userId}")]
        //public Task<IActionResult> AddUser(int projectId, int userId)
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
