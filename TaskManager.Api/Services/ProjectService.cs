using Microsoft.EntityFrameworkCore;
using TaskManager.Api.Data;
using TaskManager.Api.Models;
using TaskManager.Command.Models;

namespace TaskManager.Api.Services
{
    public class ProjectService
    {
        private readonly ApplicationContext _context;
        public ProjectService(ApplicationContext context)
        {
            _context = context;
        }
        

        public Project GetById(int id)
        {
            return _context.Projects.Find(id);
        }
        public IEnumerable<Project> GetProjectsByUserId(int userId)
        {
           var projectParticipants = _context.Users.AsNoTracking().Include(u => u.Participants).ThenInclude(p => p.Project).FirstOrDefault(u => u.Id == userId).Participants.ToArray();
            return projectParticipants.Select(p => p.Project).ToList();
        }

        public Project Create(int userId,Project project)
        {
            project.CreatorId = userId;
            project.Participants = new List<ProjectParticipant>() 
            {
                new ProjectParticipant()
                {
                    Project = project,
                    UserId = userId,
                }
            };
            _context.Projects.Add(project);
            _context.SaveChanges();
            return project;
        }
        public void Update(ProjectModel projectModel)
        {
            _context.Projects.Update((Project)projectModel);
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
