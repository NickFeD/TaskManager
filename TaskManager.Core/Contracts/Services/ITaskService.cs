using TaskManager.Core.Models;

namespace TaskManager.Core.Contracts.Services
{
    public interface ITaskService : ICRUDService<TaskModel, Guid>
    {
    }
}
