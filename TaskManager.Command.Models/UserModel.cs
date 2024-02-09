using TaskManager.Command.Models.Abstracted;

namespace TaskManager.Command.Models
{
    public class UserModel : Model
    {
#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
        public UserModel() { }
#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
        public UserModel(int id,string firstName, string lastName, string email, string? phone)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Phone = phone;
        }

        public DateTime RegistrationDate { get; set; } = DateTime.Now;
        public DateTime LastLoginData { get; set; } = DateTime.Now;
        public string FirstName { get; set; } 
        public string LastName { get; set; }
        public string Email { get; set; }
        public string? Phone { get; set; }
        //public byte[]? Photo { get; init; }

    }

}
