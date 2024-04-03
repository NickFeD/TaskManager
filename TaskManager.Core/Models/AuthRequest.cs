using System.ComponentModel.DataAnnotations;

namespace TaskManager.Core.Models
{
    public class AuthRequest
    {
        [EmailAddress]
        public required string Email { get; set; }

        public required string Password { get; set; }
    }
}
