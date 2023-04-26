using System;
using System.Collections.Generic;
using TaskManager.Command.Models;

namespace TaskManager.Api.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public DateTime RegistrationData { get; set; }
        public DateTime LastLoginData { get; set; }
        public byte[] Photo { get; set; }
        public List<Project> Projects { get; set; } = new();
        public List<Desk> Desks { get; set; } = new();
        public List<Task> Tasks { get; set; } = new();
        public UserStatus Status { get; set; }
        public User()
        {
            RegistrationData = DateTime.Now;
        }
        public User(UserModel model)
        {
            FirstName = model.FirstName;
            LastName = model.LastName;
            Email = model.Email;
            Password = model.Password;
            Phone = model.Phone;
            RegistrationData = DateTime.Now;
            LastLoginData = model.LastLoginData;
            Photo = model.Photo;
            Status = model.Status;
        }

        public UserModel ToDto() => new() 
        {
            Id = Id,
            FirstName = FirstName,
            LastName = LastName,
            Email = Email,
            Password = Password,
            Phone = Phone,
            RegistrationData = RegistrationData,
            LastLoginData = LastLoginData,
            Photo = Photo,
            Status = Status,
            
        };
    }
}
