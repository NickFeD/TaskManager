using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskManager.Api.Controllers.Abstracted;
using TaskManager.Api.Data;
using TaskManager.Api.Entity;
using TaskManager.Api.Services;
using TaskManager.Command.Models;
using Task = System.Threading.Tasks.Task;

namespace TaskManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase, ICRUDControllers<UserModel>
    {
        private readonly UserService _service;
        public UsersController(ApplicationContext context) { _service = new(context); }

        /// <summary>
        /// Create a user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(UserModel), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public Task<IActionResult> Create([FromBody] UserModel model)
        {
            if (model is null)
                return Task.FromResult((IActionResult)BadRequest());
            var modelToCreate = _service.Create(model);
            if (modelToCreate is null)
                return Task.FromResult((IActionResult)BadRequest());
            return Task.FromResult((IActionResult)CreatedAtAction(nameof(GetById), new { id = modelToCreate.Id }, modelToCreate));
        }

        /// <summary>
        /// Deleting a user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public Task<IActionResult> Delete(int id)
        {
            var userToDelete = _service.GetById(id);
            if (userToDelete is null)
                return Task.FromResult((IActionResult)NoContent());
            _service.Delete(id);
            return Task.FromResult((IActionResult)Ok());
        }

        /// <summary>
        /// All users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(List<UserModel>), StatusCodes.Status200OK)]
        public Task<IActionResult> GetAll()
        {
            return Task.FromResult<IActionResult>(Ok(_service.GetAll()));
        }

        /// <summary>
        /// Get a user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(UserModel), StatusCodes.Status200OK)]
        public Task<IActionResult> GetById(int id)
        {
            var userToGet = _service.GetById(id);
            if (userToGet is null)
                return Task.FromResult((IActionResult)NotFound());
            return Task.FromResult((IActionResult)Ok(userToGet));
        }

        /// <summary>
        /// Update user
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public Task<IActionResult> Update(int id, UserModel model)
        {
            var userAuthorize = GetUser();
            if (userAuthorize is null)
                return Task.FromResult((IActionResult)BadRequest());
            if (userAuthorize.Id != id)
                if (userAuthorize is null)
                    return Task.FromResult((IActionResult)BadRequest());
            var user = _service.GetById(id);
            if (user is null)
                return Task.FromResult((IActionResult)NotFound());
            model.Id = user.Id;
            _service.Update(model);
            return Task.FromResult((IActionResult)Ok());
        }

        /// <summary>
        /// Get a project by user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("{userId}/projects")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(List<ProjectModel>), StatusCodes.Status200OK)]
        public Task<IActionResult> GetProjectByUserId(int userId)
        {
            var projects = _service.GetProjectsByUserId(userId).ToList();
            var models = projects.Select(u => u.ToDto()).ToList();
            return models is null ? Task.FromResult((IActionResult)NotFound()) : Task.FromResult((IActionResult)Ok(models));
        }


        private User? GetUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity is null)
                return null;
            var emailUser = identity.FindFirst(ClaimTypes.Email)?.Value;
            if (emailUser is null)
                return null;
            return _service.GetByEmail(emailUser);
        }
    }
}
