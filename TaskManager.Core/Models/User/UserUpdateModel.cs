using System.ComponentModel.DataAnnotations;

namespace TaskManager.Core.Models.User
{
    public class UserUpdateModel
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        //[Phone]
        public required string Phone { get; set; }
        [EmailAddress]
        public required string Email { get; set; }
    }
}
