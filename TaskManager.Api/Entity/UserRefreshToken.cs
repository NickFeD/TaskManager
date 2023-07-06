using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.Api.Entity
{
    public class UserRefreshToken
    {
        public int Id { get; set; }
        public string IpAddress { get; set; } 
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsInvalidated { get; set; }

        public int UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }

        [NotMapped]
        public bool IsActive => ExpirationDate > DateTime.UtcNow;
    }
}
