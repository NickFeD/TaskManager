using TaskManager.Core.Contracts.Repository;
using TaskManager.Core.Entities;

namespace TaskManager.Infrastructure.Persistence.Repository;

public class TaskRepository(TaskManagerDbContext context) : BaseRepository<TaskEntity, Guid>(context), ITaskRepository
{
}
