using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Api.Controllers.Abstracted;
using TaskManager.Core.Contracts.Services;
using TaskManager.Core.Exceptions;
using TaskManager.Core.Extentions;
using TaskManager.Core.Models.User;
using Microsoft.AspNetCore.Http;

namespace TaskManager.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class MyController(IUserService userService) : BaseController
    {
        private readonly IUserService _userService = userService;

        /// <summary>
        /// Get information about the user who sent the request
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(UserModel), StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public IActionResult GetMy()
            => Ok(AuthUser.ToModel());

        /// <summary>
        /// Update your personal information
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Update(UserUpdateModel userModel)
        {
            await _userService.UpdateAsync(AuthUser.Id, userModel);
            return Ok();
        }

        /// <summary>
        /// Deleting a user
        /// </summary>
        /// <param name="isConfirmed"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Delete(bool isConfirmed)
        {
            if (!isConfirmed)
                throw new BadRequestException("Not confirmed");
            await _userService.DeleteAsync(AuthUser.Id);
            return NoContent();
        }

        /// <summary>
        /// Get a project by user id
        /// </summary>
        /// <returns></returns>
        [HttpGet("projects")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMyProject()
        {
            var projects = await _userService.GetProjectsByUserIdAsync(AuthUser.Id);
            return Ok(projects);
        }
    }
}
