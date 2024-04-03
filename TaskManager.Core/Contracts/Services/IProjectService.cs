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
        Task<ProjectModel> UpdateAsync(Guid id, ProjectUpdateModel model);
    }
}
