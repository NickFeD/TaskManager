using System.ComponentModel.DataAnnotations.Schema;
using TaskManager.Core.Models.Abstracted;

namespace TaskManager.Core.Entities;

[Table("user_roles")]
public class Role : RoleAllowed, IEntity<Guid>
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public int ProjectId { get; set; }
    [ForeignKey(nameof(ProjectId))]
    public Project? Project { get; set; }

    public List<ProjectParticipant> Participants { get; set; } = [];
}

public interface IEntity
{
    public Guid Id { get; set; }
}

