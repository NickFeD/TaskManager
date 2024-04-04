namespace TaskManager.Core.Models;

public class BoardCreateModel
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public required Guid ProjectId { get; set; }
}
