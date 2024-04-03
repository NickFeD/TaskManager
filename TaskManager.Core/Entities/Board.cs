using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.Core.Entities;


[Table("boards")]
public class Board : Entity<Guid>
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public DateTime CreationData { get; set; } = DateTime.Now;
    public Guid ProjectId { get; set; }
    [ForeignKey(nameof(ProjectId))]
    public Project? Project { get; set; }

    public List<TaskEntity> Tasks { get; set; } = [];
}
