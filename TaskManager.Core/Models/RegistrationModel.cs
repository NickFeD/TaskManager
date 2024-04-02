using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Core.Models.Abstracted;

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
        [Phone]
        public required string Phone { get; set; }
    }
}
