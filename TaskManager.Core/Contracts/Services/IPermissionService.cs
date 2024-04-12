using TaskManager.Core.Enums;

namespace TaskManager.Core.Contracts.Services
{
    public interface IPermissionService
    {
        Task Project(Guid userId, Guid projectId, AllowedProject allowedProject);
    }
}
