using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using TaskManager.DataAccess.Configurations;

namespace TaskManager.DataAccess.Models;

[Table("participants")]
[EntityTypeConfiguration(typeof(ParticipantConfiguration))]
public class ProjectParticipantEntity
{
    public Guid Id { get; set; }

    public int UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public UserEntity? User { get; set; }

    public int ProjectId { get; set; }

    [ForeignKey(nameof(ProjectId))]
    public ProjectEntity? Project { get; set; }

    public int? UserRoleId { get; set; }

    [ForeignKey(nameof(UserRoleId))]
    public RoleEntity? Role { get; set; }

    public override bool Equals(object? obj)
    {
        return obj is ProjectParticipantEntity participant &&
               UserId == participant.UserId &&
               ProjectId == participant.ProjectId;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(UserId, ProjectId);
    }
}
