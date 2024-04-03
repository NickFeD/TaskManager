using TaskManager.Core.Contracts.Repository;
using TaskManager.Core.Contracts.Services;
using TaskManager.Core.Entities;
using TaskManager.Core.Exceptions;

namespace TaskManager.Infrastructure.Services;

public class UserRefreshTokenService(IUserRefreshTokenRepository refreshTokenRepository) : IUserRefreshTokenService
{
    private readonly IUserRefreshTokenRepository _refreshTokenRepository = refreshTokenRepository;

    public Task<UserRefreshToken> CreateAsync(UserRefreshToken model)
        => _refreshTokenRepository.AddAsync(model);

    public Task DeleteAsync(Guid id)
        => _refreshTokenRepository.DeleteAsync(id);

    public Task<IEnumerable<UserRefreshToken>> GetAllAsync()
        => _refreshTokenRepository.GetAllAsync();

    public Task<UserRefreshToken> GetByIdAsync(Guid id)
        => _refreshTokenRepository.GetByIdAsync(id);

    public Task UpdateAsync(UserRefreshToken model)
        => _refreshTokenRepository.UpdateAsync(model);

    public async Task<UserRefreshToken> GetFirstIsValidate(bool isInvalidated, string token, string refreshToken, string ipAddress)
    {
        var userRefreshToken = await _refreshTokenRepository.Get(isInvalidated, token, refreshToken, ipAddress);
        if (userRefreshToken is null)
            throw new BadRequestException("ERROR");
        return userRefreshToken;
    }
}