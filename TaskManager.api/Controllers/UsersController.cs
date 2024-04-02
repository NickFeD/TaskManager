using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Core.Contracts.Services;

namespace TaskManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(IUserService userService) : ControllerBase
    {
        private readonly IUserService _userService = userService;

        /// <summary>
        /// Get a user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var userToGet = await _userService.GetByIdAsync(id);
            return Ok(userToGet);
        }


        /// <summary>
        /// Get a project by user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("{userId}/projects")]
        public async Task<IActionResult> GetProjectByUserId(Guid userId)
        {
            var projects = await _userService.GetProjectsByUserIdAsync(userId);
            return Ok(projects);
        }
    }
}
