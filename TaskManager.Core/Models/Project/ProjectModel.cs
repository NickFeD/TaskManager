using TaskManager.Core.Enums;
using TaskManager.Core.Models.Abstracted;

namespace TaskManager.Core.Models.Project
{
    public class ProjectModel : CommandModel
    {
        public Guid? CreatorId { get; set; }
        public ProjectStatus Status { get; set; }
        //public List<UserModel> AllUsers { get; set; } = new();
        //public List<BoardModel> AllDesks { get; set; } = new();
    }
}
