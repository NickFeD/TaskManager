namespace TaskManager.Core.Models
{
    public class RefreshTokenRequest
    {
#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
        public RefreshTokenRequest()
#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
        {
        }

        public required string ExpiredToken { get; set; }
        public required string RefreshToken { get; set; }
    }
}
