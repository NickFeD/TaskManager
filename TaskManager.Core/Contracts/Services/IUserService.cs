using TaskManager.Core.Models;
using TaskManager.Core.Models.Project;
using TaskManager.Core.Models.User;

namespace TaskManager.Core.Contracts.Services;

public interface IUserService
{
    Task DeleteAsync(Guid id);
    Task<List<UserModel>> GetAllAsync();
    Task<UserModel> GetByIdAsync(Guid id);
    Task<IEnumerable<UserRoleModel>> GetByProjectId(Guid projectId);
    Task<List<ProjectModel>> GetProjectsByUserIdAsync(Guid userId);
    Task<UserModel> Registration(RegistrationModel user);
    Task UpdateAsync(Guid Id, UserUpdateModel model);
}