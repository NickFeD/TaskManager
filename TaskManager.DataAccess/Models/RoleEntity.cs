using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using TaskManager.DataAccess.Configurations;

namespace TaskManager.DataAccess.Models;

[Table("user_roles")]
[EntityTypeConfiguration(typeof(RoleConfiguration))]
public class RoleEntity
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public int ProjectId { get; set; }
    [ForeignKey(nameof(ProjectId))]
    public ProjectEntity? Project { get; set; }

    public List<ProjectParticipantEntity>? Participants { get; set; } = [];
}

