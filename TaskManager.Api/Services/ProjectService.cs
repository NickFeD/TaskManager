using Microsoft.EntityFrameworkCore;
using TaskManager.Api.Data;
using TaskManager.Api.Entity;
using TaskManager.Api.Services.Abstracted;
using TaskManager.Command.Models;

namespace TaskManager.Api.Services
{
    public class ProjectService : ICRUDService<ProjectModel>
    {
        private readonly ApplicationContext _context;
        public ProjectService(ApplicationContext context) { _context = context; }

        public ProjectModel Create(ProjectModel model)
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
            return model;
        }

        public void Delete(int id)
        {
            var projectToDelete = _context.Projects.Find(id);
            if (projectToDelete is null)
                return;
            _context.Projects.Remove(projectToDelete);
            _context.SaveChanges();
        }

        public List<ProjectModel> GetAll()
        {
            return _context.Projects.AsNoTracking().Select(p => p.ToDto()).ToList();
        }

        public ProjectModel? GetById(int id)
        {
            var project = _context.Projects.AsNoTracking().FirstOrDefault(p => p.Id.Equals(id));
            if (project is null)
                return null;
            return project.ToDto();
        }

        public void Update(ProjectModel model)
        {
            var projectToUpdate = _context.Projects.Find(model.Id);
            if (projectToUpdate is null)
                return;
            projectToUpdate.Status = model.Status;
            projectToUpdate.Description = model.Description;
            projectToUpdate.Name = model.Name;
            _context.SaveChanges();

        }
    }
}
