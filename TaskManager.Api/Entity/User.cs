using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskManager.Api.Controllers.Abstracted;
using TaskManager.Command.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TaskManager.Api.Entity
{
    [Table("users")]
    public class User
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Column("first_name")]
        public string FirstName { get; set; }

        [Column("last_name")]
        public string LastName { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("password")]
        public string Password { get; set; }

        [Column("phone")]
        public string? Phone { get; set; }

        [Column("registration_date")]
        public DateTime RegistrationDate { get; set; }

        [Column("last_login_data")]
        public DateTime LastLoginData { get; set; }
        public List<ProjectParticipant>? Participants { get; set; } = new List<ProjectParticipant>();

#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
        public User() 
        {
            RegistrationDate = DateTime.Now;
            LastLoginData = DateTime.Now;
        }
#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.

        public User(UserModel model)
        {
            FirstName = model.FirstName;
            LastName = model.LastName;
            Email = model.Email;
            Password = model.Password;
            Phone = model.Phone;
            RegistrationDate = model.RegistrationDate;

        }

        public static implicit operator User(UserModel model)
        {
            return new User(model);
        }
        public static implicit operator UserModel(User model)
        {
            return new UserModel()
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Password = model.Password,
                Phone = model.Phone,
                RegistrationDate = model.RegistrationDate,
                LastLoginData = model.LastLoginData
            };

        }
    }
}
