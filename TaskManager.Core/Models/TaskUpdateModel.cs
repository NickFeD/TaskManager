namespace TaskManager.Core.Models
{
    public class TaskUpdateModel
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid BoardId { get; set; }
    }
}
