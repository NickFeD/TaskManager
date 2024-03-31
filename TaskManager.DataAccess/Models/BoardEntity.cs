using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using TaskManager.DataAccess.Configurations;

namespace TaskManager.DataAccess.Models;


[Table("boards")]
[EntityTypeConfiguration(typeof(BoardConfiguration))]
public class BoardEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string Name { get; set; }
    public string? Description { get; set; }
    public DateTime CreationData { get; set; } = DateTime.Now;
    public int ProjectId { get; set; }
    [ForeignKey(nameof(ProjectId))]
    public ProjectEntity? Project { get; set; }

    public List<TaskEntity> Tasks { get; set; } = [];
}
