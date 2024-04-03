using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.Core.Entities;

[Table("participants")]
public class ProjectParticipant : Entity<Guid>
{
    public Guid UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public User User { get; set; }

    public Guid ProjectId { get; set; }

    [ForeignKey(nameof(ProjectId))]
    public Project Project { get; set; }

    public Guid? RoleId { get; set; }

    [ForeignKey(nameof(RoleId))]
    public Role? Role { get; set; }

    public override bool Equals(object? obj)
    {
        return obj is ProjectParticipant participant &&
               UserId == participant.UserId &&
               ProjectId == participant.ProjectId;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(UserId, ProjectId);
    }
}
