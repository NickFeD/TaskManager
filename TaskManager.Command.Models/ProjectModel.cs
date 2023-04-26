namespace TaskManager.Command.Models
{
    public record ProjectModel : CommandModel
    {
        public int? AdminId { get; init; }
        public ProjectStatus Status { get; init; }
        public List<UserModel> AllUsers { get; set; } = new();
        public List<DeskModel> AllDesks { get; set; } = new();
    }
}
