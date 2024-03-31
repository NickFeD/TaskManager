using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Core.Models;

namespace TaskManager.Core.Contracts.Services;

public interface IJwtService
{
    public Task<Response<AuthResponse>?> GetTokenAsync(AuthRequest authRequest, string ipAddress);
    public Task<Response<AuthResponse>> GetRefreshTokenAsync(string idAddress, Guid userId, string email);
    public Task<bool> IsTokenValid(string accessToken, string ipAddress);
}
