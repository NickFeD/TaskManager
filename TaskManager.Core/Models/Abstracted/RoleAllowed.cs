namespace TaskManager.Core.Models.Abstracted
{
    public abstract class RoleAllowed
    {
        public bool ProjectDelete { get; set; } = false;
        public bool ProjectEdit { get; set; } = false;
        public bool ProjectAddUsers { get; set; } = false;
        public bool ProjectDeleteUsers { get; set; } = false;
        public bool BoardAdd { get; set; } = false;
        public bool BoardEdit { get; set; } = false;
        public bool BoardDelete { get; set; } = false;
        public bool BoardAddTasks { get; set; } = false;
        public bool TaskAdd { get; set; } = false;
        public bool TaskEdit { get; set; } = false;
        public bool TaskDelete { get; set; } = false;
        public bool RoleAdd { get; set; } = false;
        public bool RoleEdit { get; set; } = false;
        public bool RoleDelete { get; set; } = false;
    }
}
