using TaskManager.Core.Contracts.Repository;
using TaskManager.Core.Entities;

namespace TaskManager.Infrastructure.Persistence.Repository;

public class UserRefreshTokenRepository(TaskManagerDbContext context) : BaseRepository<UserRefreshToken, Guid>(context), IUserRefreshTokenRepository
{
}
