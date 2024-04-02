using TaskManager.Core.Entities;
using TaskManager.Core.Models;

namespace TaskManager.Core.Extentions;

public static class ParticipantExtension
{
    public static ParticipantModel ToModel(this ProjectParticipant participant)
    {
        return new()
        {
            Id = participant.Id,
            ProjectId = participant.ProjectId,
            UserId = participant.UserId,
            RoleId = participant.RoleId,
        };
    }

    public static ProjectParticipant ToEntity(this ParticipantModel participant)
    {
        return new()
        {
            Id = participant.Id,
            ProjectId = participant.ProjectId,
            UserId = participant.UserId,
            RoleId = participant.RoleId,
        };
    }
}
