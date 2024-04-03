using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.Core.Entities;

[Table("user_refresh_tokens")]
public class UserRefreshToken : Entity<Guid>
{
    public string IpAddress { get; set; } = string.Empty;

    public string Token { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; }
    public DateTime ExpirationDate { get; set; }
    public bool IsInvalidated { get; set; }

    public Guid UserId { get; set; }
    [ForeignKey(nameof(UserId))]
    public virtual User? User { get; set; }

    [NotMapped]
    public bool IsActive => ExpirationDate > DateTime.UtcNow;
}
