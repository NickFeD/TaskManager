using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.Core.Entities;

[Table("tasks")]
public class TaskEntity : Entity<Guid>
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public DateTime CreationData { get; set; } = DateTime.Now;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public Guid? ExecutorId { get; set; }

    [ForeignKey(nameof(ExecutorId))]
    public User? Executor { get; set; }

    public Guid? СreatorId { get; set; }

    [ForeignKey(nameof(СreatorId))]
    public User? Сreator { get; set; }

    public Guid BoardId { get; set; }

    [ForeignKey(nameof(BoardId))]
    public Board? Board { get; set; }
}
