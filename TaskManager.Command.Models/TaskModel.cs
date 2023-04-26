namespace TaskManager.Command.Models
{
    public record TaskModel: CommandModel
    {
        public DateTime StartDate { get; init; }
        public DateTime EndDate { get; init; }
        public byte[] File { get; init; }
        public int DeskId { get; init; }
        public string Column { get; init; }
        public int? СreatorId { get; init; }
        public int? ExecutorId { get; init; }
    }
}
