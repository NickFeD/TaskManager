using TaskManager.Core.Entities;

namespace TaskManager.Core.Contracts.Repository;

public interface IProjectRepository : IRepository<Project, Guid>
{
}
