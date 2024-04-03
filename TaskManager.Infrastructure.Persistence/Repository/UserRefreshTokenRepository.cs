using Microsoft.EntityFrameworkCore;
using TaskManager.Core.Contracts.Repository;
using TaskManager.Core.Entities;
using TaskManager.Core.Exceptions;

namespace TaskManager.Infrastructure.Persistence.Repository;

public class UserRefreshTokenRepository(TaskManagerDbContext context) : BaseRepository<UserRefreshToken, Guid>(context), IUserRefreshTokenRepository
{
    public async Task<UserRefreshToken> Get(bool isInvalidated, string expiredToken, string refreshToken, string ipAddress)
        => await _context.UserRefreshToken.FirstOrDefaultAsync(
                x => x.IsInvalidated == isInvalidated && x.Token == expiredToken
                && x.RefreshToken == refreshToken
                && x.IpAddress == ipAddress) ?? throw new BadRequestException("ERROR");
}
