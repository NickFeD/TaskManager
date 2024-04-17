using Mapster;
using Microsoft.EntityFrameworkCore;
using TaskManager.Core.Contracts.Services;
using TaskManager.Core.Entities;
using TaskManager.Core.Exceptions;
using TaskManager.Core.Models.Project;
using TaskManager.Infrastructure.Persistence;

namespace TaskManager.Infrastructure.Services
{
    public class ProjectService(TaskManagerDbContext context) : IProjectService
    {
        private readonly TaskManagerDbContext _context = context;

        public async Task UpdateAsync(Guid id, ProjectUpdateModel model)
        {
            var count = await _context.Projects.Where(p => p.Id == id)
                .ExecuteUpdateAsync(setters => setters
                .SetProperty(p => p.Name, model.Name)
                .SetProperty(p => p.Status, model.Status)
                .SetProperty(p => p.Description, model.Description));

            if (count < 1)
                throw new NotFoundException("Invalid project uuid");
        }

        public async Task<IEnumerable<ProjectModel>> GetAllAsync()
        {
            var project = await _context.Projects.AsNoTracking().ProjectToType<ProjectModel>().ToListAsync();

            return project;
        }

        public async Task<ProjectModel> GetByIdAsync(Guid id)
        {
            var project = await _context.Projects.AsNoTracking().ProjectToType<ProjectModel>().FirstOrDefaultAsync(p => p.Id == id);

            if (project is null)
                throw new NotFoundException("Invalid project uuid");

            return project;
        }

        public async Task<ProjectModel> CreateAsync(ProjectModel model)
        {

            var role = new Role()
            {
                Name = "Admin",
                ProjectDeleteUsers = true,
                ProjectAddUsers = true,
                ProjectDelete = true,
                ProjectEdit = true,
                BoardAddTasks = true,
                BoardDelete = true,
                BoardEdit = true,
                BoardAdd = true,
                RoleDelete = true,
                RoleEdit = true,
                RoleAdd = true,
                TaskDelete = true,
                TaskEdit = true,
                TaskAdd = true,
            };
            var participant = new ProjectParticipant()
            {
                Role = role,
                UserId = model.CreatorId!.Value,
            };
            var project = model.Adapt<Project>();
            project.Status = Core.Enums.ProjectStatus.InProgress;
            project.CreationData = DateTime.UtcNow;
            project.Participants.Add(participant);
            project.Roles.Add(role);

            role.Project = project;
            role.Participants.Add(participant);

            participant.Project = project;
            participant.Role = role;



            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();

            return project.Adapt<ProjectModel>();
        }

        public async Task DeleteAsync(Guid id)
        {
            var count = await _context.Projects.Where(p => p.Id == id).ExecuteDeleteAsync();

            if (count < 1)
                throw new NotFoundException("Invalid project uuid");
        }
    }
}
