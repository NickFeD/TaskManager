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
        public UsersController(ApplicationContext context)
        {
            _service = new(context);
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
    }
}
