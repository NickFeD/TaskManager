namespace TaskManager.Core.Models.Project;

public class ProjectAddUsers
{
    public Guid RoleId { get; set; }

    public Guid[] UserId { get; set; } = [];
}