using Mapster;
using Microsoft.EntityFrameworkCore;
using TaskManager.Core.Contracts.Services;
using TaskManager.Core.Entities;
using TaskManager.Core.Exceptions;
using TaskManager.Infrastructure.Persistence;

namespace TaskManager.Infrastructure.Services;

public class UserRefreshTokenService(TaskManagerDbContext context) : IUserRefreshTokenService
{
    private readonly TaskManagerDbContext _context = context;

    public async Task<UserRefreshToken> CreateAsync(UserRefreshToken model)
    {
        await _context.UserRefreshToken.AddAsync(model);
        await _context.SaveChangesAsync();
        return model;
    }

    public async Task DeleteAsync(Guid id)
    {
        var count = await _context.UserRefreshToken.Where(p => p.Id == id).ExecuteDeleteAsync();

        if (count < 1)
            throw new NotFoundException("Invalid refresh token uuid");
    }

    public Task<List<UserRefreshToken>> GetAllAsync()
    {
        return _context.UserRefreshToken.AsNoTracking().ProjectToType<UserRefreshToken>().ToListAsync();
    }

    public async Task<UserRefreshToken> GetByIdAsync(Guid id)
    {
        var userRefreshToken = await _context.UserRefreshToken.AsNoTracking().ProjectToType<UserRefreshToken>().SingleOrDefaultAsync(p => p.Id == id);

        if (userRefreshToken is null)
            throw new NotFoundException("Invalid refresh token uuid");

        return userRefreshToken;
    }

    public async Task UpdateAsync(UserRefreshToken model)
    {
        var count = await _context.UserRefreshToken.Where(p => p.Id == model.Id)
                .ExecuteUpdateAsync(setters => setters
                .SetProperty(p => p.IpAddress, model.IpAddress)
                .SetProperty(p => p.UserId, model.UserId)
                .SetProperty(p => p.RefreshToken, model.RefreshToken)
                .SetProperty(p => p.IsInvalidated, model.IsInvalidated)
                .SetProperty(p => p.CreatedDate, model.CreatedDate)
                .SetProperty(p => p.ExpirationDate, model.ExpirationDate));

        if (count < 1)
            throw new NotFoundException("Invalid project uuid");
    }

    public async Task<UserRefreshToken> GetFirstIsValidate(bool isInvalidated, string token, string refreshToken, string ipAddress)
    {
        var userRefreshToken = await _context.UserRefreshToken.FirstOrDefaultAsync(
                x => x.IsInvalidated == isInvalidated && x.Token == token
                && x.RefreshToken == refreshToken
                && x.IpAddress == ipAddress) ?? throw new BadRequestException("ERROR");
        return userRefreshToken;
    }
}