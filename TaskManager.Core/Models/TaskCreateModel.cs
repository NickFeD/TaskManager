namespace TaskManager.Core.Models;

public class TaskCreateModel
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public Guid BoardId { get; init; }
}
