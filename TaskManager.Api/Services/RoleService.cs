using Microsoft.EntityFrameworkCore;
using TaskManager.Api.Data;
using TaskManager.Api.Entity;
using TaskManager.Api.Services.Abstracted;
using TaskManager.Command.Models;
using Task = System.Threading.Tasks.Task;

namespace TaskManager.Api.Services
{
    public class RoleService : ICRUDService<RoleModel>, ICRUDServiceAsync<RoleModel>
    {
        private readonly ApplicationContext _context;

        public RoleService(ApplicationContext context)
        {
            _context = context;
        }

        public Response<RoleModel> Create(RoleModel model)
        {
            var userRole = new Role()
            {
                ProjectId = model.ProjectId,
                Name = model.Name,
            };
            _context.Roles.Add(userRole);
            _context.SaveChanges();
            model.Id = userRole.Id;
            return new() { IsSuccess = true, Model = model };
        }

        public Task<Response<RoleModel>> CreateAsync(RoleModel model)
            => Task.FromResult(Create(model));

        public Response Delete(int id)
        {
            var userRoleToDelete = _context.Roles.Find(id);
            if (userRoleToDelete is null)
                return new() { IsSuccess = false, Reason = "Not found" };
            _context.Roles.Remove(userRoleToDelete);
            _context.SaveChanges();
            return new() { IsSuccess = true };
        }

        public Task<Response> DeleteAsync(int id)
            => Task.FromResult(Delete(id));

        public Response<List<RoleModel>> GetAll()
        {
            var userRoles = _context.Roles.AsNoTracking().Select(r => r.ToDto()).ToList();
            if (userRoles is null)
                return new() { IsSuccess = false, Reason = "No roles" };
            return new() { IsSuccess = true, Model = userRoles };
        }

        public Task<Response<List<RoleModel>>> GetAllAsync()
            => Task.FromResult(GetAll());

        public Response<RoleModel> GetById(int id)
        {
            var userRole = _context.Roles.AsNoTracking().FirstOrDefault(p => p.Id.Equals(id));
            if (userRole is null)
                return new() { IsSuccess = false, Reason = "No role" };
            return new() { IsSuccess = true, Model = userRole.ToDto() };
        }

        public Task<Response<RoleModel>> GetByIdAsync(int id)
            => Task.FromResult(GetById(id));

        public Response Update(RoleModel model)
        {
            var userRoleToUpdate = _context.Roles.Find(model.Id);
            if (userRoleToUpdate is null)
                return new() { IsSuccess = false, Reason = "There is no role" };
            userRoleToUpdate.Name = model.Name;
            _context.SaveChanges();
            return new() { IsSuccess = true };
        }

        public Task<Response> UpdateAsync(RoleModel model)
            => Task.FromResult(Update(model));
    }
}
