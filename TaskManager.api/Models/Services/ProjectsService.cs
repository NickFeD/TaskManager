using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Api.Models.Abstractions;
using TaskManager.Api.Models.Data;
using TaskManager.Command.Models;

namespace TaskManager.Api.Models.Services
{
    public class ProjectsService : AbstractionService, ICommandService<ProjectModel>
    {
        private readonly ApplicationContext _db;

        public ProjectsService(ApplicationContext context)
        {
            _db = context;
        }

        public ProjectModel Get(int id)
        {
            return _db.Projects.FirstOrDefault(p => p.Id == id).ToDto();
        }
        public IEnumerable<ProjectModel> GetByUserId(int userId)
        {
            List<ProjectModel> projectModels = new();
            var admin = _db.ProjectAdmins.FirstOrDefault(a => a.UserId == userId);
            if (admin != null)
            {
                var projectsForAdmin = _db.Projects.Where(p => p.AdminId ==  userId).Select(p=> p.ToDto());
                projectModels.AddRange(projectsForAdmin);
            }
            var projectsForUser = _db.Projects.Include(p=>p.AllUsers).Where(p=>p.AllUsers.Any(u=> u.Id ==userId)).Select(p => p.ToDto());
            projectModels.AddRange(projectsForUser);
            return projectModels;
        }
        public async Task<IEnumerable<ProjectModel>> GetAll()
        {
            return await _db.Projects.Select(p => p.ToDto()).ToListAsync();
        }

        public bool Create(ProjectModel model)
        {
            return DoAction(() =>
            {
                _db.Projects.Add(new Project(model));
                _db.SaveChanges();
            });
        }

        public bool Delete(int index)
        {
            return DoAction(() =>
            {
                Project project = _db.Projects.FirstOrDefault(p => p.Id == index);
                _db.Projects.Remove(project);
                _db.SaveChanges();
            });
        }

        public bool Update(int index, ProjectModel model)
        {
            return DoAction(() =>
            {
                Project project = _db.Projects.FirstOrDefault(p => p.Id == index);
                project.Id = model.Id;
                project.Name = model.Name;
                project.Description = model.Description;
                project.Photo = model.Photo;
                project.Status = model.Status;
                project.AdminId = model.AdminId;
                _db.Projects.Update(project);
                _db.SaveChanges();
            });
        }
    }
}
