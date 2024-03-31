using System.Linq.Expressions;
using TaskManager.Core.Entities;

namespace TaskManager.Core.Contracts.Repository;

public interface IParticipantRepository : IRepository<ProjectParticipant, Guid>
{

    Task<List<Project>> GetProjectsByConditionAsync(Expression<Func<ProjectParticipant, bool>> filter);
}
