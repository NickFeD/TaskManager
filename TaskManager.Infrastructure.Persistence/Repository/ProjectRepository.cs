using TaskManager.Core.Contracts.Repository;
using TaskManager.Core.Entities;

namespace TaskManager.Infrastructure.Persistence.Repository;

public class ProjectRepository(TaskManagerDbContext context) : BaseRepository<Project, Guid>(context), IProjectRepository
{
}
