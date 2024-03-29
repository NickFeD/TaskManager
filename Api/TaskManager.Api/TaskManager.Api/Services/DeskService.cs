using Microsoft.EntityFrameworkCore;
using TaskManager.Api.Data;
using TaskManager.Api.Entity;
using TaskManager.Api.Exceptions;
using TaskManager.Api.Services.Abstracted;
using TaskManager.Command.Models;
using Task = System.Threading.Tasks.Task;

namespace TaskManager.Api.Services
{
    public class DeskService(ApplicationContext context) : ICRUDServiceAsync<DeskModel>
    {
        private readonly ApplicationContext _context = context;

        public async Task<DeskModel> CreateAsync(DeskModel model)
        {
            var desk = new Desk()
            {
                CreationData = DateTime.UtcNow,
                Description = model.Description,
                Name = model.Name,
                ProjectId = model.ProjectId,
            };
            _context.Desks.Add(desk);
            await _context.SaveChangesAsync();
            model.Id = desk.Id;
            return model;
        }

        public async Task DeleteAsync(int id)
        {
            var countDelete = await _context.Desks.Where(d => d.Id == id).ExecuteDeleteAsync();

            if (countDelete < 1)
                throw new NotFoundException("Not found desk");
        }

        public async Task<List<DeskModel>> GetAllAsync()
        {
            var desks = await _context.Desks.AsNoTracking().Select(d => d.ToDto()).ToListAsync();

            if (desks.Count < 1)
                throw new NotFoundException("Not found desks");

            return desks;
        }

        public async Task<DeskModel> GetByIdAsync(int id)
        {
            var desk = await _context.Desks.AsNoTracking().FirstOrDefaultAsync(p => p.Id.Equals(id));

            if (desk is null)
                throw new NotFoundException("Not found desk");

            return desk.ToDto();
        }

        public async Task UpdateAsync(DeskModel model)
        {
            var countUpdate = await _context.Desks.Where(d => d.Id == model.Id)
                .ExecuteUpdateAsync(setter => setter
                .SetProperty(o => o.Description, model.Description)
                .SetProperty(o => o.Name, model.Name)
                .SetProperty(o => o.ProjectId, model.ProjectId));

            if (countUpdate < 1)
                throw new NotFoundException("Not found desk");
        }
    }
}
