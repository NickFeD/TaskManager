using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Api.Controllers.Abstracted;
using TaskManager.Core.Contracts.Services;
using TaskManager.Core.Models.Project;

namespace TaskManager.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/Projects")]
    public class ProjectController(IProjectService projectService, IPermissionService permissionService, IUserService userService) : BaseController
    {
        private readonly IProjectService _projectService = projectService;
        private readonly IPermissionService _permissionService = permissionService;
        private readonly IUserService _userService = userService;

        /// <summary>
        /// Create a project
        /// </summary>
        /// <param name="createModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Create([FromBody] ProjectCreateModel createModel)
        {
            var model = new ProjectModel()
            {
                Name = createModel.Name,
                Description = createModel.Description,
                CreatorId = AuthUser.Id,
            };
            var modelToCreate = await _projectService.CreateAsync(model);
            return CreatedAtAction(nameof(GetById), new { id = modelToCreate.Id }, modelToCreate);
        }

        /// <summary>
        /// Delete a project
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _projectService.DeleteAsync(id);
            return Ok();
        }

        /// <summary>
        /// Get a project by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await _projectService.GetByIdAsync(id);
            return Ok(response);
        }

        /// <summary>
        /// Edit the project
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(Guid id, ProjectUpdateModel model)
        {
            await _permissionService.Project(AuthUser.Id, id, Core.Enums.AllowedProject.Edit);
            await _projectService.UpdateAsync(id, model);
            return Ok();
        }


        [HttpPost("{id}/Users")]
        public async Task<IActionResult> AddUsers(Guid id, ProjectAddUsers addUsers)
        {
            await _permissionService.Project(AuthUser.Id, id, Core.Enums.AllowedProject.Edit);
            await _projectService.AddUsers(id, addUsers.RoleId, addUsers.UserId);
            return Ok();
        }


        [HttpGet("{id}/Users")]
        public async Task<IActionResult> GetUsers(Guid id)
        {
            var response = await _userService.GetByProjectId(id);
            return Ok(response);
        }


        [HttpGet("{id}/Boards")]
        public async Task<IActionResult> GetBoard(Guid id)
        {
            var response = await _projectService.GetByIdBoard(id);
            return Ok(response);
        }
    }
}
