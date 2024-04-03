using TaskManager.Core.Models.Abstracted;

namespace TaskManager.Core.Models
{
    public class TaskModel : CommandModel
    {
        public DateTime StartDate { get; init; }
        public DateTime EndDate { get; init; }
        //public byte[] File { get; init; }
        public Guid BoardId { get; init; }
        //public string Column { get; init; }
        public Guid? СreatorId { get; init; }
        //public int? ExecutorId { get; init; }
    }
}
