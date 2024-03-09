using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Api.Controllers.Abstracted;
using TaskManager.Api.Data;
using TaskManager.Api.Entity;
using TaskManager.Api.Services;
using TaskManager.Command.Models;
using Microsoft.AspNetCore.Http;
using System.Data;

namespace TaskManager.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly ProjectService _service;
        private readonly RoleService _roleService;
        private readonly HttpContextHandlerService _httpHandler;

        public ProjectController(ApplicationContext context) 
        { 
            _service = new(context);
            _httpHandler = new(context);
            _roleService = new(context);
        }

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
            var user = _httpHandler.GetUserAsNoTracking(HttpContext);
            if (user == null)
                return BadRequest();
            model.CreatorId = user.Id;
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
            if (!IsValidate(_httpHandler.GetUserRoleAsNoTracking(HttpContext, id)?.AllowedDeleteProject))
                return BadRequest(new Response { IsSuccess = false, Reason = "no access" });

            var project = await _service.GetByIdAsync(id);
            if (!project.IsSuccess)
                return BadRequest(project);

            _service.Delete(id);
            return Ok(project);
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
        /// Edit the project
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Edit(int id, ProjectModel model)
        {
            if (!IsValidate(_httpHandler.GetUserRoleAsNoTracking(HttpContext, id)?.AllowedEditProject))
                return BadRequest(new Response { IsSuccess = false, Reason = "no access" });
            var response = await _service.UpdateAsync(model);
            if (!response.IsSuccess)
                return BadRequest(response);
            return Ok(response);
        }


        [HttpPost("{id}/Users")]
        [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddUsers(int id, int[] usersId)
        {
            var response = await _service.AddUsers(id, usersId);
            if (!response.IsSuccess)
                return BadRequest(response);
            return Ok(response);
        }

        [HttpGet("{id}/Users")]
        [ProducesResponseType(typeof(Response<List<UserRoleModel>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<List<UserRoleModel>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetUsers(int id)
        {
            var role = _httpHandler.GetUserRoleAsNoTracking(HttpContext, id);
            if (role is null)
                return BadRequest(new Response { IsSuccess = false, Reason = "no access" });
            var response = await _service.GetUsers(id);
            if (!response.IsSuccess)
                return BadRequest(response);
            return Ok(response);
        }

        private bool IsValidate(bool? isValidate)
        {
            if (isValidate is null)
                return false;
            return (bool)isValidate;
        }
    }
}
