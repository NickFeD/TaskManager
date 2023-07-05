using TaskManager.Command.Models;

namespace TaskManager.Api.Services.Abstracted
{
    public interface IJwtServices
    {
        public Task<string> GetTokenAsync(AuthRequest authRequest);
    }
}
