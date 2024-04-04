
namespace TaskManager.Core.Models;

public class ParticipantUpdateModel
{
    public Guid UserId { get; set; }
    public Guid ProjectId { get; set; }
    public Guid RoleId { get; set; }
}
