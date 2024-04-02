using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Core.Models;
using TaskManager.Core.Models.Project;

namespace TaskManager.Core.Contracts.Services
{
    public interface IProjectService
    {
        Task<Response> AddUsers(Guid projectId, Guid roleId, params string[] usersId);
        Task<ProjectModel> CreateAsync(ProjectModel model);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<ProjectModel>> GetAllAsync();
        Task<ProjectModel> GetByIdAsync(Guid id);
        Task<ProjectModel> UpdateAsync(Guid id, ProjectUpdateModel model);
    }
}
