using System.Linq;
using TaskManager.Api.Models.Abstractions;
using TaskManager.Api.Models.Data;
using TaskManager.Command.Models;

namespace TaskManager.Api.Models.Services
{
    public class ProjectsService : AbstractionService,ICommandService<ProjectModel>
    {
        private readonly ApplicationContext _db;

        public ProjectsService(ApplicationContext context)
        {
            _db = context;
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
