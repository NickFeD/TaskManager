using Microsoft.EntityFrameworkCore;
using TaskManager.Api.Data;
using TaskManager.Api.Entity;
using TaskManager.Api.Services.Abstracted;
using TaskManager.Command.Models;
using Task = System.Threading.Tasks.Task;

namespace TaskManager.Api.Services
{
    public class ProjectService : ICRUDService<ProjectModel>, ICRUDServiceAsync<ProjectModel>
    {
        private readonly ApplicationContext _context;
        public ProjectService(ApplicationContext context) { _context = context; }

        public Response<ProjectModel> Create(ProjectModel model)
        {
            var project = new Project()
            {
                CreationData = DateTime.UtcNow,
                CreatorId = model.CreatorId,
                Description = model.Description,
                Name = model.Name,
                Status = model.Status,
            };
            _context.Projects.Add(project);
            _context.SaveChanges();
            model.Id = project.Id;
            return new() { IsSuccess = true, Model = model };
        }

        public Task<Response<ProjectModel>> CreateAsync(ProjectModel model)
            => Task.FromResult(Create(model));

        public Response Delete(int id)
        {
            var projectToDelete = _context.Projects.Find(id);
            if (projectToDelete is null)
                return new() { IsSuccess = false, Reason = "Project not found" };
            _context.Projects.Remove(projectToDelete);
            _context.SaveChanges();
            return new() { IsSuccess = true };
        }

        public Task<Response> DeleteAsync(int id)
            => Task.FromResult(Delete(id));

        public Response<List<ProjectModel>> GetAll()
        {
            var projects = _context.Projects.AsNoTracking().Select(p => p.ToDto()).ToList();
            if (projects is null)
                return new() { IsSuccess = false, Reason = "No projects" };
            return new() { IsSuccess = true, Model = projects };
        }

        public Task<Response<List<ProjectModel>>> GetAllAsync()
            => Task.FromResult(GetAll());

        public Response<ProjectModel> GetById(int id)
        {
            var project = _context.Projects.AsNoTracking().FirstOrDefault(p => p.Id.Equals(id));
            if (project is null)
                return new() { IsSuccess = false, Reason = "No project" };
            return new() { IsSuccess = true, Model = project.ToDto() };
        }

        public Task<Response<ProjectModel>> GetByIdAsync(int id)
            => Task.FromResult(GetById(id));

        public Response Update(ProjectModel model)
        {
            var projectToUpdate = _context.Projects.Find(model.Id);
            if (projectToUpdate is null)
                return new() { IsSuccess = false, Reason = "There is no project" };
            projectToUpdate.Status = model.Status;
            projectToUpdate.Description = model.Description;
            projectToUpdate.Name = model.Name;
            _context.SaveChanges();
            return new() { IsSuccess = true };
        }

        public Task<Response> UpdateAsync(ProjectModel model)
            => Task.FromResult(Update(model));
    }
}
