using TaskManager.Core.Contracts.Repository;
using TaskManager.Core.Entities;
using TaskManager.Core.Exceptions;

namespace TaskManager.Infrastructure.Persistence.Repository;

public class ProjectRepository(TaskManagerDbContext context) : BaseRepository<Project, Guid>(context), IProjectRepository
{
    public async Task AddRangeAsync(Guid id, ProjectParticipant[] participants)
    {
        var project = await _context.Projects.FindAsync(id);
        if (project is null)
            throw new NotFoundException();
        project.Participants.AddRange(participants);
    }
}
