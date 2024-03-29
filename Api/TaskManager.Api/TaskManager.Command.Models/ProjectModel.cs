using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskManager.Command.Models.Abstracted;

namespace TaskManager.Command.Models
{
    public class ProjectModel : CommandModel
    {
        public int? CreatorId { get; set; }
        public ProjectStatus Status { get; set; }
        //public List<UserModel> AllUsers { get; set; } = new();
        //public List<DeskModel> AllDesks { get; set; } = new();
    }
}
