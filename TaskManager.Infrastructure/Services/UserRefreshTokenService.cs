using Azure.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Core.Contracts.Repository;
using TaskManager.Core.Contracts.Services;
using TaskManager.Core.Entities;
using TaskManager.Core.Exceptions;

namespace TaskManager.Infrastructure.Services;

internal class UserRefreshTokenService(IUserRefreshTokenRepository refreshTokenRepository) : IUserRefreshTokenService
{
    private readonly IUserRefreshTokenRepository _refreshTokenRepository = refreshTokenRepository;

    public Task<UserRefreshToken> CreateAsync(UserRefreshToken model)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<UserRefreshToken>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<UserRefreshToken> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(UserRefreshToken model)
    {
        throw new NotImplementedException(); 
    }

    public async Task<UserRefreshToken> GetFirstIsValidate(bool isInvalidated, string token, string refreshToken, string ipAddress)
    {
        var userRefreshToken = await _refreshTokenRepository.Get(isInvalidated, token,refreshToken, ipAddress);
        if (userRefreshToken is null)
            throw new BadRequestException("ERROR");
        return userRefreshToken;
    }
}