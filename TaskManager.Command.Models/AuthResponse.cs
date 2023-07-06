namespace TaskManager.Command.Models
{
    public class AuthResponse
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }

        public bool IsSuccess { get; set; }
        public string Reason { get; set; }

        public DateTime ExpiresToken { get; set; }
        public DateTime ExpiresRefreshToken { get; set; }
    }
}
