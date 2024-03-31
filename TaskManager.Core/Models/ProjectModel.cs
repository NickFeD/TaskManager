using TaskManager.Core.Enums;
using TaskManager.Core.Models.Abstracted;

namespace TaskManager.Core.Models
{
    public class ProjectModel : CommandModel
    {
        public int? CreatorId { get; set; }
        public ProjectStatus Status { get; set; }
        //public List<UserModel> AllUsers { get; set; } = new();
        //public List<BoardModel> AllDesks { get; set; } = new();
    }
}
