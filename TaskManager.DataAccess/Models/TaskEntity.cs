using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using TaskManager.DataAccess.Configurations;

namespace TaskManager.DataAccess.Models;

[Table("tasks")]
[EntityTypeConfiguration(typeof(TaskConfiguration))]
public class TaskEntity
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public DateTime CreationData { get; set; } = DateTime.Now;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int? СreatorId { get; set; }

    [ForeignKey(nameof(СreatorId))]
    public UserEntity? Сreator { get; set; }

    public int BoardId { get; set; }

    [ForeignKey(nameof(BoardId))]
    public BoardEntity? Board { get; set; }
}
