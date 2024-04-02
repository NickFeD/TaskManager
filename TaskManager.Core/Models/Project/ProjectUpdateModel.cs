using TaskManager.Core.Enums;

namespace TaskManager.Core.Models.Project
{
    public class ProjectUpdateModel
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public ProjectStatus Status { get; set; }
    }
}
