using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Api.Controllers.Abstracted;
using TaskManager.Api.Data;
using TaskManager.Api.Services;
using TaskManager.Command.Models;
using Task = System.Threading.Tasks.Task;
using Microsoft.AspNetCore.Http;

namespace TaskManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserService _service;
        private readonly HttpContextHandlerService _httpHandler;
        public UsersController(ApplicationContext context)
        {
            _service = new(context);
            _httpHandler = new(context, HttpContext);
        }

        /// <summary>
        /// Create a user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(Response<UserModel>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Response<UserModel>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] UserModel model)
        {
            if (model is null)
                return BadRequest(new Response<UserModel> { IsSuccess = false, Reason = "You didn't send anything" });
            var modelToCreate = await _service.CreateAsync(model);
            if (!modelToCreate.IsSuccess)
                return BadRequest(modelToCreate);
            return CreatedAtAction(nameof(GetById), new { id = modelToCreate.Model.Id }, modelToCreate);
        }

        /// <summary>
        /// Deleting a user
        /// </summary>
        /// <param name="isConfirmed"></param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete]
        [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(bool isConfirmed)
        {
            if (!isConfirmed)
                return BadRequest(new Response { IsSuccess = false, Reason = "Not confirmed" });
            var userToDelete = await _httpHandler.GetUserAsync(HttpContext);
            if (userToDelete is null)
                return BadRequest(new Response { IsSuccess = false, Reason = "Not found" });
            var response = await _service.DeleteAsync(userToDelete);
            return Ok(response);
        }

        /// <summary>
        /// Get a user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Response<UserModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<UserModel>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var userToGet = await _service.GetByIdAsync(id);
            if (!userToGet.IsSuccess)
                return NotFound(userToGet);
            return Ok(userToGet);
        }

        /// <summary>
        /// Update user
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize]
        [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(UserModel model)
        {
            var user = await _httpHandler.GetUserAsync(HttpContext);
            if (user is null)
                return NotFound(new Response { IsSuccess = false, Reason = "Not Found" });
            model.Id = user.Id;
            var response = _service.Update(model);
            return Ok(response);
        }

        /// <summary>
        /// Get a project by user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("{userId}/projects")]
        [ProducesResponseType(typeof(Response<List<ProjectModel>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<List<ProjectModel>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetProjectByUserId(int userId)
        {
            var projects = await _service.GetProjectsByUserIdAsync(userId);
            if (!projects.IsSuccess)
                return BadRequest(projects);
            var models = projects.Model.Select(u => u.ToDto()).ToList();
            return Ok(models);
        }

        /// <summary>
        /// Get information about the user who sent the request
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("my")]
        [ProducesResponseType(typeof(Response<UserModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<UserModel>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetMy()
        {
            var user = await _httpHandler.GetUserAsync(HttpContext);
            if (user is null)
                return BadRequest(new Response<UserModel> { IsSuccess = false, Reason = "Сould not identify the user" });
            return Ok(new Response<UserModel> { IsSuccess = true, Model = user.ToDto() });
        }
    }
}
