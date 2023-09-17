using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using TaskManager.Api.Data;
using TaskManager.Api.Services;
using TaskManager.Command.Models;



namespace TaskManager.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class MyController : ControllerBase
    {
        private readonly HttpContextHandlerService _httpHandler;
        private readonly UserService _userService;

        public MyController(ApplicationContext context)
        {
            _httpHandler = new(context);
            _userService = new(context);
        }

        /// <summary>
        /// Get information about the user who sent the request
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(UserModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetMy()
        {
            var user = await _httpHandler.GetUserAsync(HttpContext);
            if (user is null)
                return BadRequest("Сould not identify the user" );
            return Ok(user.ToDto());
        }

        /// <summary>
        /// Update your personal information
        /// </summary>
        /// <param name="patchUser"></param>
        /// <returns></returns>
        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> UpdateMy([FromBody] JsonPatchDocument<UserModel> patchUser)
        {
            var user = await _httpHandler.GetUserAsync(HttpContext);
            if (user is null)
                return NotFound("Not Found User");
            var model = user.ToDto();
            patchUser.ApplyTo(model);
            var response = _userService.Update(model);
            return Ok(response);
        }

        /// <summary>
        /// Deleting a user
        /// </summary>
        /// <param name="isConfirmed"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteMy(bool isConfirmed)
        {
            if (!isConfirmed)
                return BadRequest(new Response { IsSuccess = false, Reason = "Not confirmed" });
            var userToDelete = await _httpHandler.GetUserAsync(HttpContext);
            if (userToDelete is null)
                return BadRequest(new Response { IsSuccess = false, Reason = "Not found" });
            var response = await _userService.DeleteAsync(userToDelete);
            return Ok(response);
        }

        /// <summary>
        /// Get a project by user id
        /// </summary>
        /// <returns></returns>
        [HttpGet("projects")]

        [ProducesResponseType(typeof(Response<List<ProjectModel>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<List<ProjectModel>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetMyProject()
        {
            var user = _httpHandler.GetUser(HttpContext);
            if (user is null)
                return BadRequest(new Response<List<ProjectModel>> { IsSuccess = false, Reason= "FATAL ERROR" });
            var projects = await _userService.GetProjectsByUserIdAsync(user.Id);
            if (!projects.IsSuccess)
                return BadRequest(projects);
            var models = projects.Model.Select(u => u.ToDto()).ToList();
            return Ok(models);
        }
    }
}
