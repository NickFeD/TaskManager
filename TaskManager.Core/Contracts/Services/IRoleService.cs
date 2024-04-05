using TaskManager.Core.Models;

namespace TaskManager.Core.Contracts.Services
{
    public interface IRoleService //: ICRUDService<RoleModel, Guid>
    {
        Task<RoleModel> CreateAsync(RoleCreateModel model);
        Task DeleteAsync(Guid id);
        Task<List<RoleModel>> GetAllAsync();
        Task<RoleModel> GetByIdAsync(Guid id);
        Task UpdateAsync(Guid id, RoleUpdateModel model);
    }
}
