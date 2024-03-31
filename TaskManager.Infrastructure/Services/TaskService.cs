using Microsoft.EntityFrameworkCore;
using TaskManager.Api.Data;
using TaskManager.Api.Entity;
using TaskManager.Api.Exceptions;
using TaskManager.Infrastructure.Services.Abstracted;
using TaskManager.Command.Models;
using Task = TaskManager.Api.Entity.TaskEntity;

namespace TaskManager.Infrastructure.Services
{
    public class TaskService(ApplicationContext context) : ICRUDServiceAsync<TaskModel>
    {
        private readonly ApplicationContext _context = context;

        public async Task<TaskModel> CreateAsync(TaskModel model)
        {
            var task = new Task()
            {
                CreationData = DateTime.UtcNow,
                DeskId = model.Id,
                Description = model.Description,
                Name = model.Name,
                EndDate = model.EndDate,
                StartDate = model.StartDate,
                СreatorId = model.СreatorId,
            };
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            model.Id = task.Id;
            return model;
        }

        public async System.Threading.Tasks.Task DeleteAsync(int id)
        {
            var countDelete = await _context.Tasks.Where(d => d.Id == id).ExecuteDeleteAsync();

            if (countDelete < 1)
                throw new NotFoundException("Not found task");
        }

        public async Task<List<TaskModel>> GetAllAsync()
        {
            var desks = await _context.Tasks.AsNoTracking().Select(d => d.ToDto()).ToListAsync();

            if (desks.Count < 1)
                throw new NotFoundException("Not found tasks");

            return desks;
        }

        public async Task<TaskModel> GetByIdAsync(int id)
        {
            var task = await _context.Tasks.AsNoTracking().FirstOrDefaultAsync(p => p.Id.Equals(id));

            if (task is null)
                throw new NotFoundException("Not found task");

            return task.ToDto();
        }

        public async System.Threading.Tasks.Task UpdateAsync(TaskModel model)
        {
            var countUpdate = await _context.Tasks.Where(d => d.Id == model.Id)
                 .ExecuteUpdateAsync(setter => setter
                 .SetProperty(o => o.StartDate, model.StartDate)
                 .SetProperty(o => o.EndDate, model.EndDate)
                 .SetProperty(o => o.Name, model.Name)
                 .SetProperty(o => o.Description, model.Description));

            if (countUpdate < 1)
                throw new NotFoundException("Not found task");
        }
    }
}
