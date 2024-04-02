using System.ComponentModel.DataAnnotations;
using TaskManager.Core.Models.Abstracted;

namespace TaskManager.Core.Models.User
{
    public class UserModel : Model
    {
#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
        public UserModel() { }
#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
        public UserModel(Guid id, string firstName, string lastName, string email, string phone)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Phone = phone;
        }
        public required string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string Phone { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime LastLoginData { get; set; } = DateTime.Now;

        //public byte[]? Photo { get; init; }

    }

}
