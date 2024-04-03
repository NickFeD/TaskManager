using System.ComponentModel.DataAnnotations.Schema;
using TaskManager.Core.Enums;

namespace TaskManager.Core.Entities;

[Table("projects")]
public class Project : Entity<Guid>
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public DateTime CreationData { get; set; } = DateTime.UtcNow;
    public ProjectStatus Status { get; set; }
    public Guid? CreatorId { get; set; }
    [ForeignKey(nameof(CreatorId))]
    public User? Creator { get; set; }

    public List<ProjectParticipant> Participants { get; set; } = [];
    public List<Role> Roles { get; set; } = [];
    public List<Board>? Boards { get; set; } = [];
}
