using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Core.Models.Abstracted;

namespace TaskManager.Core.Models.User
{
    public class UserUpdateModel
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        [Phone]
        public required string Phone { get; set; }
        [EmailAddress]
        public required string Email { get; set; }
    }
}
