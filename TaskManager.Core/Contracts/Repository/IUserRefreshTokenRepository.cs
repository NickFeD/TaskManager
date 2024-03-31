using TaskManager.Core.Entities;

namespace TaskManager.Core.Contracts.Repository;

public interface IUserRefreshTokenRepository : IRepository<UserRefreshToken, Guid>
{
}