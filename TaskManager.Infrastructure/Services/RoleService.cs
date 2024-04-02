using TaskManager.Core.Contracts.Repository;
using TaskManager.Core.Contracts.Services;
using TaskManager.Core.Extentions;
using TaskManager.Core.Models;

namespace TaskManager.Infrastructure.Services
{
    public class RoleService(IRoleRepository roleRepository) : IRoleService
    {
        private readonly IRoleRepository _roleRepository = roleRepository;

        public async Task<RoleModel> CreateAsync(RoleModel model)
        {
            model.Id = (await _roleRepository.AddAsync(model.ToEntity())).Id;
            return model;
        }

        public Task DeleteAsync(Guid id)
            => _roleRepository.DeleteAsync(id);

        public async Task<IEnumerable<RoleModel>> GetAllAsync()
        {
            var roles = await _roleRepository.GetAllAsync();
            return roles.Select(r => r.ToModel());
        }

        public async Task<RoleModel> GetByIdAsync(Guid id)
        {
            var role = await _roleRepository.GetByIdAsync(id);
            return role.ToModel();
        }

        public Task UpdateAsync(RoleModel model)
            => _roleRepository.UpdateAsync(model.ToEntity());


    }
}
