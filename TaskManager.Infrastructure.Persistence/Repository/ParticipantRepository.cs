using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TaskManager.Core.Contracts.Repository;
using TaskManager.Core.Entities;

namespace TaskManager.Infrastructure.Persistence.Repository;

public class ParticipantRepository(TaskManagerDbContext context) : BaseRepository<ProjectParticipant, Guid>(context), IParticipantRepository
{
    public async Task<List<Project>> GetProjectsByConditionAsync(Expression<Func<ProjectParticipant, bool>> filter)
    {
        var projects = await _context.Participant.AsNoTracking().Where(filter).Include(p=>p.Project).Select(p=>p.Project).ToListAsync();
        if(projects is null)
            throw new ArgumentNullException(nameof(projects));
        return projects;
    }
}
