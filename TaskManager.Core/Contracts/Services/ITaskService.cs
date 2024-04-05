using TaskManager.Core.Models;

namespace TaskManager.Core.Contracts.Services
{
    public interface ITaskService
    {
        Task<TaskModel> CreateAsync(TaskCreateModel model);
        Task DeleteAsync(Guid id);
        Task<List<TaskModel>> GetAllAsync();
        Task<TaskModel> GetByIdAsync(Guid id);
        Task UpdateAsync(Guid id, TaskUpdateModel model);
    }
}
