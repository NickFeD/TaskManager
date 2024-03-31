using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.Core.Entities;

[Table("user_roles")]
public class Role : Entity<Guid>
{
    public required string Name { get; set; }
    public int ProjectId { get; set; }
    [ForeignKey(nameof(ProjectId))]
    public Project? Project { get; set; }

    public List<ProjectParticipant>? Participants { get; set; } = [];
}

