using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskManager.Api.Models.Abstracted;
using TaskManager.Command.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TaskManager.Api.Models
{
    [Table("users")]
    public class User:Model
    {
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
        public DateTime RegistrationDate { get; set; } = DateTime.Now;

        [Column("last_login_data")]
        public DateTime LastLoginData { get; set; }
        public List<ProjectParticipant>? Participants { get; set; } = new List<ProjectParticipant>();

        public User()
        {
            RegistrationDate = DateTime.Now;
        }

        public User(UserModel model)
        {
            FirstName = model.FirstName;
            LastName = model.LastName;
            Email = model.Email;
            Password = model.Password;
            Phone = model.Phone;
            RegistrationDate = model.RegistrationDate;
            LastLoginData = model.LastLoginData;

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
