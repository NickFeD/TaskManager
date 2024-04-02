using TaskManager.Core.Entities;

namespace TaskManager.Core.Contracts.Services;

public interface IUserRefreshTokenService : ICRUDService<UserRefreshToken, Guid>
{
    Task<UserRefreshToken> GetFirstIsValidate(bool isInvalidated, string token, string refreshToken, string ipAddress);
}
