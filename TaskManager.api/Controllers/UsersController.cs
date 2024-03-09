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
    public class UsersController(ApplicationContext context) : ControllerBase
    {
        private readonly UserService _service = new(context);

        /// <summary>
        /// Get a user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var userToGet = await _service.GetByIdAsync(id);
            return Ok(userToGet);
        }


        /// <summary>
        /// Get a project by user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("{userId}/projects")]
        public async Task<IActionResult> GetProjectByUserId(int userId)
        {
            var projects = await _service.GetProjectsByUserIdAsync(userId);
            return Ok(projects);
        }
    }
}
