using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Command.Models.Abstracted;

namespace TaskManager.Command.Models
{
    public class UserModel: Model
    {

#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
        public UserModel()
        {
        }
#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.

        public UserModel(string firstName, string lastName, string email, string password, string? phone, DateTime registrationDate, DateTime lastLoginData)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
            Phone = phone;
            RegistrationDate = registrationDate;
            LastLoginData = lastLoginData;
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string? Phone { get; set; }

        public DateTime RegistrationDate { get; set; } = DateTime.Now;

        public DateTime LastLoginData { get; set; } = DateTime.Now;
        //public byte[]? Photo { get; init; }

    }

}
