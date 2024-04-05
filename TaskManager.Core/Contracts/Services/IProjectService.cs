using TaskManager.Core.Models;
using TaskManager.Core.Models.Project;

namespace TaskManager.Core.Contracts.Services
{
    public interface IProjectService
    {
        Task AddUsers(Guid projectId, Guid roleId, params Guid[] usersId);
        Task<ProjectModel> CreateAsync(ProjectModel model);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<ProjectModel>> GetAllAsync();
        Task<ProjectModel> GetByIdAsync(Guid id);
        Task<List<BoardModel>> GetByIdBoard(Guid id);
        Task UpdateAsync(Guid id, ProjectUpdateModel model);
    }
}
