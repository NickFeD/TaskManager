using TaskManager.Core.Contracts.Repository;
using TaskManager.Core.Contracts.Services;
using TaskManager.Core.Extentions;
using TaskManager.Core.Models;

namespace TaskManager.Infrastructure.Services
{
    public class TaskService(ITaskRepository taskRepository) : ITaskService
    {
        private readonly ITaskRepository _taskRepository = taskRepository;

        public async Task<TaskModel> CreateAsync(TaskModel model)
        {
            model.Id = (await _taskRepository.AddAsync(model.ToEntity())).Id;
            return model;
        }

        public Task DeleteAsync(Guid id)
            => _taskRepository.DeleteAsync(id);

        public async Task<IEnumerable<TaskModel>> GetAllAsync()
        {
            var desks = await _taskRepository.GetAllAsync();
            return desks.Select(d => d.ToModel());
        }

        public async Task<TaskModel> GetByIdAsync(Guid id)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            return task.ToModel();
        }

        public Task UpdateAsync(TaskModel model)
            => _taskRepository.UpdateAsync(model.ToEntity());
    }
}
