using Microsoft.EntityFrameworkCore;
using TaskManager.Api.Data;
using TaskManager.Api.Entity;
using TaskManager.Command.Models;

namespace TaskManager.Api.Services
{
    public class ProjectService: ICRUDService<ProjectModel>
    {
        private readonly ApplicationContext _context;
        public ProjectService(ApplicationContext context)
        {
            _context = context;
        }
        // CRUT
        public List<ProjectModel> GetAll()
        {
            return _context.Projects.Select(p=> (ProjectModel)p).ToList();
        }
        public ProjectModel? GetById(int id)
        {
            var project = _context.Projects.Find(id);
            return project is null? null : (ProjectModel) project;
        }
        
        public ProjectModel Create(ProjectModel project)
        {
            //project.Participants = new List<ProjectParticipant>() 
            //{
            //    new ProjectParticipant()
            //    {
            //        Project = project,
            //        UserId = project.CreatorId?? throw new ArgumentNullException(),
            //    }
            //};
            _context.Projects.Add(project);
            _context.SaveChanges();
            return project;
        }
        
        public void Update(ProjectModel projectModel)
        {
            _context.Projects.Update(projectModel);
            _context.SaveChanges();
        }
        
        public void Delete(int id)
        {
            var projectToDelete = _context.Projects.Find(id);
            if (projectToDelete is null)
                return;

            _context.Projects.Remove(projectToDelete);
            _context.SaveChanges();
        }

        
    }
}
