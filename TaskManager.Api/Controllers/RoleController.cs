using Microsoft.AspNetCore.Mvc;
using TaskManager.Api.Controllers.Abstracted;
using TaskManager.Core.Contracts.Services;
using TaskManager.Core.Models;

namespace TaskManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController(IRoleService roleService) : BaseController
    {
        private readonly IRoleService _roleService = roleService;

        /// <summary>
        /// Create a role
        /// </summary>
        /// <param name="createModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Create([FromBody] RoleCreateModel createModel)
        {
            var model = new RoleModel()
            {
               Id = Guid.NewGuid(),
               Name = createModel.Name,
               ProjectId =createModel.ProjectId,
               AllowedAddUsersProject = createModel.AllowedAddUsersProject,
               AllowedDeleteProject = createModel.AllowedDeleteProject,
               AllowedEditProject = createModel.AllowedEditProject,
            };
            var modelToCreate = await _roleService.CreateAsync(model);
            return CreatedAtAction(nameof(GetById), new { id = modelToCreate.Id }, modelToCreate);
        }

        /// <summary>
        /// Delete a role
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _roleService.DeleteAsync(id);
            return Ok();
        }

        /// <summary>
        /// Get all roles
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _roleService.GetAllAsync();
            return Ok(response);
        }

        /// <summary>
        /// Get a role by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await _roleService.GetByIdAsync(id);
            return Ok(response);
        }

        /// <summary>
        /// Update the role
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, RoleUpdateModel model)
        {
            var role = new RoleModel()
            {
                Id = id,
                Name = model.Name,
                AllowedAddUsersProject = model.AllowedAddUsersProject,
                AllowedDeleteProject = model.AllowedDeleteProject,
                AllowedEditProject = model.AllowedEditProject,
            };
            await _roleService.UpdateAsync(role);
            return Ok();
        }
    }
}
