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
        [ProducesResponseType(typeof(Response<UserModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<UserModel>), StatusCodes.Status400BadRequest)]
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
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateMy(UserModel model)
        {
            var user = await _httpHandler.GetUserAsync(HttpContext);
            if (user is null)
                return NotFound(new Response { IsSuccess = false, Reason = "Not Found" });
            model.Id = user.Id;
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

            
#pragma warning disable CS1572 // Комментарий XML содержит тег param, но параметр с таким именем не существует
/// <summary>
        /// Get a project by user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("projects")]
#pragma warning restore CS1572 // Комментарий XML содержит тег param, но параметр с таким именем не существует
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
