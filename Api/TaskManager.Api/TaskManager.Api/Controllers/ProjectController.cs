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
    [Route("api/My/Project")]
    public class ProjectController(ApplicationContext context) : ControllerBase
    {
        private readonly ProjectService _service = new(context);
        private readonly RoleService _roleService = new(context);
        private readonly HttpContextHandlerService _httpHandler = new(context);

        /// <summary>
        /// Create a project
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Create([FromBody] ProjectModel model)
        {
            var user = _httpHandler.GetUserAsNoTracking(HttpContext);
            model.CreatorId = user.Id;
            var modelToCreate = await _service.CreateAsync(model);
            return CreatedAtAction(nameof(GetById), new { id = modelToCreate.Id }, modelToCreate);
        }

        /// <summary>
        /// Delete a project
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return Ok();
        }

        /// <summary>
        /// Get a project by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _service.GetByIdAsync(id);
            return Ok(response);
        }

        /// <summary>
        /// Edit the project
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Edit(ProjectModel model)
        {
            await _service.UpdateAsync(model);
            return Ok();
        }


        [HttpPost("{id}/Users")]
        public async Task<IActionResult> AddUsers(int id, int[] usersId)
        {
            var response = await _service.AddUsers(id, usersId);
            return Ok(response);
        }

        [HttpGet("{id}/Users")]
        public async Task<IActionResult> GetUsers(int id)
        {
            var response = await _service.GetUsers(id);
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
