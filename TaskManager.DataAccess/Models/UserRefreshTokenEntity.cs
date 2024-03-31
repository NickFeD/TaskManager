using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using TaskManager.DataAccess.Configurations;

namespace TaskManager.DataAccess.Models;

[Table("user_refresh_tokens")]
[EntityTypeConfiguration(typeof(UserRefreshTokenConfiguration))]
public class UserRefreshTokenEntity
{
    public Guid Id { get; set; }
    public string IpAddress { get; set; } = string.Empty;
    public required string Token { get; set; }
    public required string RefreshToken { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime ExpirationDate { get; set; }
    public bool IsInvalidated { get; set; }

    public int UserId { get; set; }
    [ForeignKey(nameof(UserId))]
    public virtual UserEntity? User { get; set; }

    [NotMapped]
    public bool IsActive => ExpirationDate > DateTime.UtcNow;
}
