namespace TaskManager.Core.Models
{
    public class AuthResponse
    {
        public string Token { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;

        public DateTime ExpiresToken { get; set; }
        public DateTime ExpiresRefreshToken { get; set; }
    }
}
