using TaskManager.Command.Models.Abstracted;

namespace TaskManager.Command.Models
{
    public class DeskModel : CommandModel
    {
        //public bool IsPrivate { get; init; }
        //public string[]? Columns { get; init; }
        //public int AdminId { get; init; }
        public int ProjectId { get; init; }
        //public List<TaskModel> Tasks { get; set; } = new();
    }
}
