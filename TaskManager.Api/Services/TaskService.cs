using Microsoft.EntityFrameworkCore;
using TaskManager.Api.Data;
using TaskManager.Api.Entity;
using TaskManager.Api.Services.Abstracted;
using TaskManager.Command.Models;
using Task = TaskManager.Api.Entity.Task;

namespace TaskManager.Api.Services
{
    public class TaskService : ICRUDService<TaskModel>,ICRUDServiceAsync<TaskModel>
    {
        private readonly ApplicationContext _context;

        public TaskService(ApplicationContext context) { _context = context;  }

        public Response<TaskModel> Create(TaskModel model)
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
            _context.SaveChanges();
            model.Id = task.Id;
            return new() { IsSuccess = true, Model = model };
        }

        public Task<Response<TaskModel>> CreateAsync(TaskModel model)
            => System.Threading.Tasks.Task.FromResult(Create(model));

        public Response Delete(int id)
        {
            var taskToDelete = _context.Tasks.Find(id);
            if (taskToDelete is null)
                return new() { IsSuccess = false, Reason = "Task not found" };
            _context.Tasks.Remove(taskToDelete);
            _context.SaveChanges();
            return new() { IsSuccess = true };
        }

        public Task<Response> DeleteAsync(int id)
            => System.Threading.Tasks.Task.FromResult(Delete(id));

        public Response< List<TaskModel>> GetAll()
        {
            var tasks = _context.Tasks.AsNoTracking().Select(t => t.ToDto()).ToList();
            if (tasks is null)
                return new() { IsSuccess = false, Reason = "No tasks" };
            return new() { IsSuccess = true, Model = tasks };
        }

        public Task<Response<List<TaskModel>>> GetAllAsync()
            => System.Threading.Tasks.Task.FromResult(GetAll());

        public Response<TaskModel> GetById(int id)
        {
            var task = _context.Tasks.AsNoTracking().FirstOrDefault(p => p.Id.Equals(id));
            if (task is null)
                return new() { IsSuccess = false, Reason = "No user" };
            return new() { IsSuccess = true, Model = task.ToDto() };
        }

        public Task<Response<TaskModel>> GetByIdAsync(int id)
            => System.Threading.Tasks.Task.FromResult(GetById(id));

        public Response Update(TaskModel model)
        {
            var taskToUpdate = _context.Tasks.Find(model.Id);
            if (taskToUpdate is null)
                return new() { IsSuccess = false, Reason = "There is no project" };
            taskToUpdate.StartDate = model.StartDate;
            taskToUpdate.EndDate = model.EndDate;
            taskToUpdate.Description = model.Description;
            taskToUpdate.Name = model.Name;
            _context.SaveChanges();
            return new() { IsSuccess = true };
        }

        public Task<Response> UpdateAsync(TaskModel model)
        => System.Threading.Tasks.Task.FromResult(Update(model));
    }
}
