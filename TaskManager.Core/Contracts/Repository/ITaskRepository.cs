using TaskManager.Core.Entities;

namespace TaskManager.Core.Contracts.Repository;

public interface ITaskRepository : IRepository<TaskEntity, Guid>
{
}