using TaskManager.Core.Models.Abstracted;

namespace TaskManager.Core.Models;

public class RoleCreateModel : RoleAllowed
{
    public string Name { get; set; } = string.Empty;
    public Guid ProjectId { get; set; }
}
