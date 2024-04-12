using TaskManager.Core.Entities;
using TaskManager.Core.Models;

namespace TaskManager.Core.Contracts.Services
{
    public interface ITaskService
    {
        Task<TaskModel> CreateAsync(TaskEntity model);
        Task DeleteAsync(Guid id);
        IAsyncEnumerable<TaskModel> GetAllAsync();
        IAsyncEnumerable<TaskModel> GetByBoardIdAsync(Guid id);
        Task<TaskModel> GetByIdAsync(Guid id);
        Task UpdateAsync(Guid id, TaskUpdateModel model);
    }
}
