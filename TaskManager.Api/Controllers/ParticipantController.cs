using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Api.Controllers.Abstracted;
using TaskManager.Core.Contracts.Services;
using TaskManager.Core.Models.Project;
using TaskManager.Core.Models.User;

namespace TaskManager.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ParticipantController(IParticipantService participantService, IUserService userService, IPermissionService permissionService) : BaseController //ICRUDController<ParticipantModel, Guid>
    {
        private readonly IParticipantService _participantService = participantService;
        private readonly IUserService _userService = userService;
        private readonly IPermissionService _permissionService = permissionService;


        [HttpPost("/api/projects/{id}/users")]
        public async Task<IActionResult> AddUsers(Guid id, ProjectAddUsers addUsers)
        {
            await _permissionService.Project(AuthUser.Id, id, Core.Enums.AllowedProject.AddUsers);
            await _participantService.AddUsersInProject(id, addUsers.RoleId, addUsers.UserId);
            return Ok();
        }


        [HttpGet("/api/projects/{id}/users")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IAsyncEnumerable<UserRoleModel> GetUsers(Guid id)
        {
            return _userService.GetByProjectId(id);
        }

        [HttpDelete("/api/projects/{projectId}/users/{userId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> DeleteUsers(Guid projectId, Guid userId)
        {
            await _permissionService.Project(AuthUser.Id, projectId, Core.Enums.AllowedProject.DeleteUsers);
            await _participantService.DeleteParticipantInProjectAsync(projectId, userId);
            return NoContent();
        }
    }
}
