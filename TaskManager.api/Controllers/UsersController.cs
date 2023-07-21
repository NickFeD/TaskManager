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
    public class UsersController : ControllerBase, ICRUDController<UserModel>
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
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete]
        [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            var userToDelete = await _service.GetByIdAsync(id);
            if (!userToDelete.IsSuccess)
                return BadRequest(new Response { IsSuccess = false, Reason = userToDelete.Reason });
            var response = _service.Delete(id);
            return Ok(response);
        }

        /// <summary>
        /// All users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(Response<List<UserModel>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<List<UserModel>>), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public  async Task<IActionResult> GetAll()
        {
            var users = await _service.GetAllAsync();
            if (!users.IsSuccess)
                return BadRequest(users);
            return Ok(users);
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
        [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, UserModel model)
        {
            var userAuthorize = _httpHandler.GetUser();
            if (userAuthorize is null)
                return BadRequest(new Response { IsSuccess = false, Reason ="Not Found User"});
            if (userAuthorize.Id != id)
                return BadRequest(new Response { IsSuccess = false, Reason = "Не прикидывайся" });
            var user = await _service.GetByIdAsync(id);
            if (!user.IsSuccess)
                return BadRequest(new Response { IsSuccess = user .IsSuccess, Reason= user.Reason });
            model.Id = user.Model.Id;
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
    }
}
