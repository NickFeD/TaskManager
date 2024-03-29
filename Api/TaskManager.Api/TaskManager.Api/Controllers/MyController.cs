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
    public class MyController(ApplicationContext context) : ControllerBase
    {
        private readonly HttpContextHandlerService _httpHandler = new(context);
        private readonly UserService _userService = new(context);

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
                return BadRequest("Сould not identify the user");
            return Ok(user.ToDto());
        }

        /// <summary>
        /// Update your personal information
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Update(UserModel userModel)
        {
            var user = await _httpHandler.GetUserAsync(HttpContext);
            if (user is null)
                return NotFound("Not Found User");
            userModel.Id = user.Id;
            var response = _userService.UpdateAsync(userModel);
            return Ok(response);
        }

        /// <summary>
        /// Deleting a user
        /// </summary>
        /// <param name="isConfirmed"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Delete(bool isConfirmed)
        {
            if (!isConfirmed)
                return BadRequest("Not confirmed");
            var userToDelete = await _httpHandler.GetUserAsync(HttpContext);
            if (userToDelete is null)
                return NotFound();
            await _userService.DeleteAsync(userToDelete.Id);
            return Ok();
        }

        /// <summary>
        /// Get a project by user id
        /// </summary>
        /// <returns></returns>
        [HttpGet("projects")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetMyProject()
        {
            var user = _httpHandler.GetUser(HttpContext);
            if (user is null)
                return BadRequest("FATAL ERROR");
            var projects = await _userService.GetProjectsByUserIdAsync(user.Id);
            return Ok(projects);
        }
    }
}
