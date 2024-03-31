using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskManager.DataAccess.Configurations;

namespace TaskManager.DataAccess.Models;

[Table("users")]
[EntityTypeConfiguration(typeof(UserConfiguration))]
public class UserEntity
{
    public Guid Id { get; set; }
    public required string Username { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    [Phone]
    public required string Phone { get; set; }
    [EmailAddress]
    public required string Email { get; set; }

    public required byte[] Password { get; set; }

    public required byte[] Salt { get; set; }

    public DateTime RegistrationDate { get; set; } = DateTime.Now;

    public DateTime LastLoginData { get; set; } = DateTime.Now;

    public List<ProjectParticipantEntity> Participants { get; set; } = [];
    public List<UserRefreshTokenEntity> RefreshTokens { get; set; } = [];
}
