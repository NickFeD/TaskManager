using TaskManager.Core.Enums;

namespace TaskManager.Core.Contracts.Services
{
    public interface IPermissionService
    {
        Task Board(Guid userId, Guid projectId, AllowedBoard allowedBoard);
        Task Project(Guid userId, Guid boardId, AllowedProject allowedProject);
        Task Task(Guid userId, Guid taskId, AllowedTask allowedBoard);
    }
}
