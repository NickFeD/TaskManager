using TaskManager.Core.Models.Project;

namespace TaskManager.Core.Contracts.Services
{
    public interface IProjectService
    {
        Task<ProjectModel> CreateAsync(ProjectModel model);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<ProjectModel>> GetAllAsync();
        Task<ProjectModel> GetByIdAsync(Guid id);
        Task UpdateAsync(Guid id, ProjectUpdateModel model);
    }
}
