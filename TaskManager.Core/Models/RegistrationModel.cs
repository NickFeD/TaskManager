using System.ComponentModel.DataAnnotations;

namespace TaskManager.Core.Models
{
    public class RegistrationModel
    {
        public required string Username { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public required string Password { get; set; }

        [EmailAddress]
        public required string Email { get; set; }

       // [Phone]
        public required string Phone { get; set; }
    }
}
