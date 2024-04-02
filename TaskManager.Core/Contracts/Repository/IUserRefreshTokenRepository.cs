using System.Net;
using TaskManager.Core.Entities;

namespace TaskManager.Core.Contracts.Repository;

public interface IUserRefreshTokenRepository : IRepository<UserRefreshToken, Guid>
{
    Task<UserRefreshToken> Get(bool isInvalidated, string expiredToken, string refreshToken, string ipAddress);
}