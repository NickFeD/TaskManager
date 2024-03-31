using Microsoft.EntityFrameworkCore;
using TaskManager.Api.Data;
using TaskManager.Api.Entity;
using TaskManager.Api.Exceptions;
using TaskManager.Infrastructure.Services.Abstracted;
using TaskManager.Command.Models;
using Task = System.Threading.Tasks.Task;

namespace TaskManager.Infrastructure.Services
{
    public class RoleService(ApplicationContext context) : ICRUDServiceAsync<RoleModel>
    {
        private readonly ApplicationContext _context = context;

        public async Task<RoleModel> CreateAsync(RoleModel model)
        {
            var userRole = new Role()
            {
                ProjectId = model.ProjectId,
                Name = model.Name,
            };
            _context.Roles.Add(userRole);
            await _context.SaveChangesAsync();
            model.Id = userRole.Id;
            return model;
        }

        public async Task DeleteAsync(int id)
        {
            var countDelete = await _context.Roles.Where(d => d.Id == id).ExecuteDeleteAsync();

            if (countDelete < 1)
                throw new NotFoundException("Not found role");
        }

        public async Task<List<RoleModel>> GetAllAsync()
        {
            var roles = await _context.Roles.AsNoTracking().Select(d => d.ToDto()).ToListAsync();

            if (roles.Count < 1)
                throw new NotFoundException("Not found roles");

            return roles;
        }

        public async Task<RoleModel> GetByIdAsync(int id)
        {
            var role = await _context.Roles.AsNoTracking().FirstOrDefaultAsync(p => p.Id.Equals(id));

            if (role is null)
                throw new NotFoundException("Not found desk");

            return role.ToDto();
        }

        public async Task UpdateAsync(RoleModel model)
        {
            var countUpdate = await _context.Roles.Where(d => d.Id == model.Id)
                .ExecuteUpdateAsync(setter => setter
                .SetProperty(o => o.Name, model.Name));

            if (countUpdate < 1)
                throw new NotFoundException("Not found role");
        }

        
    }
}
