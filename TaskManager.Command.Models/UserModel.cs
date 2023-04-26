using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Command.Models
{
    public record UserModel
    {
        public int Id { get; init; }
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string Password { get; init; }
        public string Email { get; init; }
        public string Phone { get; init; }
        public DateTime RegistrationData { get; init; }
        public DateTime LastLoginData { get; init; }
        public byte[]? Photo { get; init; }
        public UserStatus Status { get; init; }
    }
    
}
