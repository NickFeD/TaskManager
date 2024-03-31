using TaskManager.Core.Contracts.Repository;
using TaskManager.Core.Entities;

namespace TaskManager.Infrastructure.Persistence.Repository;

public class RoleRepository(TaskManagerDbContext context) : BaseRepository<Role, Guid>(context), IRoleRepository
{
}
