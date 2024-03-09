using Microsoft.EntityFrameworkCore;
using TaskManager.Api.Data;
using TaskManager.Api.Entity;
using TaskManager.Api.Services.Abstracted;
using TaskManager.Command.Models;
using Task = System.Threading.Tasks.Task;

namespace TaskManager.Api.Services
{
    public class DeskService : ICRUDService<DeskModel>, ICRUDServiceAsync<DeskModel>
    {
        private readonly ApplicationContext _context;
        public DeskService(ApplicationContext context){ _context = context; }

        public Response<DeskModel> Create(DeskModel model)
        {
            var desk = new Desk()
            {
                CreationData = DateTime.UtcNow,
                Description = model.Description,
                Name = model.Name,
                ProjectId = model.ProjectId,
            };
            _context.Desks.Add(desk);
            _context.SaveChanges();
            model.Id = desk.Id;
            return new() { IsSuccess = true, Model = model };
        }

        public Task<Response<DeskModel>> CreateAsync(DeskModel model) 
            => Task.FromResult(Create(model));

        public Response Delete(int id)
        {
            var deskToDelete = _context.Desks.Find(id);
            if (deskToDelete is null)
                return new() { IsSuccess = false, Reason = "No desk" };
            _context.Desks.Remove(deskToDelete);
            _context.SaveChanges();
            return new() { IsSuccess = true };
        }

        public Task<Response> DeleteAsync(int id)
            => Task.FromResult(Delete(id));

        public Response< List<DeskModel>> GetAll()
        {
            var desks = _context.Desks.AsNoTracking().Select(d => d.ToDto()).ToList();
            if (desks is null)
                return new() { IsSuccess = false, Reason = "No desks" };
            return new() { IsSuccess = true, Model = desks };
        }

        public Task<Response<List<DeskModel>>> GetAllAsync() 
            => Task.FromResult(GetAll());

        public Response<DeskModel> GetById(int id)
        {
            var desk = _context.Desks.AsNoTracking().FirstOrDefault(p => p.Id.Equals(id));
            if (desk is null)
                return new() { IsSuccess = false, Reason = "No desk" };
            return new() { IsSuccess = true, Model = desk.ToDto()};
        }

        public Task<Response<DeskModel>> GetByIdAsync(int id)
            =>Task.FromResult(GetById(id));

        public Response Update(DeskModel model)
        {
            var deskToUpdate = _context.Desks.Find(model.Id);
            if (deskToUpdate is null)
                return new() { IsSuccess = false, Reason = "No desk" };
            deskToUpdate.Description = model.Description;
            deskToUpdate.Name = model.Name;
            deskToUpdate.ProjectId = model.ProjectId;
            _context.SaveChanges();
            return new() { IsSuccess = true };
        }

        public Task<Response> UpdateAsync(DeskModel model)
            => Task.FromResult(Update(model));
    }
}
