using TaskManager.Core.Contracts.Repository;
using TaskManager.Core.Entities;

namespace TaskManager.Infrastructure.Persistence.Repository;

public class ParticipantRepository(TaskManagerDbContext context) : BaseRepository<ProjectParticipant, Guid>(context), IParticipantRepository
{
}
