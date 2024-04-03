using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.Core.Entities;

[Table("users")]
public class User : Entity<Guid>
{
    //public Guid Id { get; set; }
    [Required]
    public string Username { get; set; } = "";

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    //[Phone]
    public required string Phone { get; set; }
    [EmailAddress]
    public required string Email { get; set; }

    public byte[] Password { get; set; } = [];

    public byte[] Salt { get; set; } = [];

    public DateTime RegistrationDate { get; set; }

    public DateTime LastLoginData { get; set; } = DateTime.Now;

    public List<ProjectParticipant> Participants { get; set; } = [];
    public List<UserRefreshToken> RefreshTokens { get; set; } = [];
}
