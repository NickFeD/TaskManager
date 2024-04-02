using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Core.Entities;
using TaskManager.Core.Models;

namespace TaskManager.Core.Contracts.Services;

public interface IJwtService
{
    public Task<AuthResponse> GetTokenAsync(AuthRequest authRequest, string ipAddress);
    public Task<AuthResponse> GetRefreshTokenAsync(string idAddress, Guid userId, string email);
    public Task<User> IsTokenValid(string accessToken, string ipAddress);
}
