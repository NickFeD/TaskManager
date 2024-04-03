using TaskManager.Core.Entities;
using TaskManager.Core.Models.Abstracted;

namespace TaskManager.Core.Models
{
    public class RoleModel : RoleAllowed, IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public Guid ProjectId { get; set; }
    }
}
