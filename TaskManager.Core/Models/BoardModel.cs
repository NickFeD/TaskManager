using TaskManager.Core.Models.Abstracted;

namespace TaskManager.Core.Models
{
    public class BoardModel : CommandModel
    {
        //public bool IsPrivate { get; init; }
        //public string[]? Columns { get; init; }
        //public int AdminId { get; init; }
        public Guid ProjectId { get; init; }
        //public List<TaskModel> Tasks { get; set; } = new();
    }
}
