using Mapster;
using Microsoft.EntityFrameworkCore;
using TaskManager.Core.Contracts.Services;
using TaskManager.Core.Entities;
using TaskManager.Core.Exceptions;
using TaskManager.Core.Models;
using TaskManager.Infrastructure.Persistence;

namespace TaskManager.Infrastructure.Services
{
    public class TaskService(TaskManagerDbContext context) : ITaskService
    {
        private readonly TaskManagerDbContext _context = context;

        public async Task<TaskModel> CreateAsync(TaskEntity model)
        {
            var isBoard = await _context.Boards.AnyAsync(b=>b.Id == model.BoardId);

            if (!isBoard)
                throw new BadRequestException("Invalid board uuid");

            await _context.Tasks.AddAsync(model);
            await _context.SaveChangesAsync();

            return model.Adapt<TaskModel>();
        }

        public async Task DeleteAsync(Guid id)
        {
            var count = await _context.Tasks.Where(p => p.Id == id).ExecuteDeleteAsync();

            if (count < 1)
                throw new BadRequestException("Invalid board uuid");
        }

        public async Task<List<TaskModel>> GetAllAsync()
        {
            var project = await _context.Tasks.AsNoTracking().ProjectToType<TaskModel>().ToListAsync();

            return project;
        }

        public async Task<TaskModel> GetByIdAsync(Guid id)
        {
            var taskModel = await _context.Tasks.AsNoTracking().ProjectToType<TaskModel>().SingleOrDefaultAsync(t => t.Id == id);

            if (taskModel is null)
                throw new BadRequestException("Invalid board uuid");

            return taskModel;
        }

        public async Task UpdateAsync(Guid id, TaskUpdateModel model)
        {
            var count = await _context.Tasks.Where(p => p.Id == id)
                .ExecuteUpdateAsync(setters => setters
                .SetProperty(p => p.Name, model.Name)
                .SetProperty(p => p.StartDate, model.StartDate)
                .SetProperty(p => p.EndDate, model.EndDate)
                .SetProperty(p => p.Description, model.Description));

            if (count < 1)
                throw new BadRequestException("Invalid board uuid");
        }
    }
}
