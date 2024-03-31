using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using TaskManager.Command.Models;
using TaskManager.DataAccess.Configurations;

namespace TaskManager.DataAccess.Models;

[Table("projects")]
[EntityTypeConfiguration(typeof(ProjectConfiguration))]
public class ProjectEntity
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public DateTime CreationData { get; set; } = DateTime.Now;
    public ProjectStatus Status { get; set; }
    public int? CreatorId { get; set; }
    [ForeignKey(nameof(CreatorId))]
    public UserEntity? Creator { get; set; }

    public List<ProjectParticipantEntity> Participants { get; set; } = [];
    public List<RoleEntity>? Roles { get; set; } = [];
    public List<BoardEntity>? Boards { get; set; } = [];
}
