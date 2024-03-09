using TaskManager.Command.Models;

namespace TaskManager.Api.Entity
{
    public class User
    {
#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
        public User() { }
#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string? Phone { get; set; }

        public DateTime RegistrationDate { get; set; } = DateTime.Now;

        public DateTime LastLoginData { get; set; } = DateTime.Now;

        public List<ProjectParticipant>? Participants { get; set; } = new();
        public List<UserRefreshToken> RefreshTokens { get; set; } = new();


        public UserModel ToDto() => new()
        {
            Id = Id,
            Email = Email,
            FirstName = FirstName,
            LastLoginData = LastLoginData,
            LastName = LastName,
            Phone = Phone,
            RegistrationDate = RegistrationDate,
        };

    }
}
