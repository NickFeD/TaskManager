using TaskManager.Command.Models;

namespace TaskManager.Api.Services.Abstracted
{
    public interface IJwtServices
    {
        public Task<AuthResponse> GetTokenAsync(AuthRequest authRequest, string ipAddress);
        public Task<AuthResponse> GetRefreshTokenAsync(string idAddress, int userId, string email);
        public Task<bool> IsTokenValid(string accessToken, string ipAddress);
    }
}
