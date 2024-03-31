using Microsoft.EntityFrameworkCore;
using TaskManager.Api.Data;
using TaskManager.Api.Entity;
using TaskManager.Api.Exceptions;
using TaskManager.Infrastructure.Services.Abstracted;
using TaskManager.Command.Models;
using Task = System.Threading.Tasks.Task;

namespace TaskManager.Infrastructure.Services
{
    public class ProjectService(ApplicationContext context) : ICRUDServiceAsync<ProjectModel>
    {
        private readonly ApplicationContext _context = context;

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

        public async Task<Response> AddUsers(int projectId, int[] usersId)
        {
            var participants = new ProjectParticipant[usersId.Length];
            for (int i = 0; i < usersId.Length; i++)
            {
                var participant = new ProjectParticipant()
                {
                    UserId = usersId[i],
                    ProjectId = projectId,
                };
                participants[i] = participant;
            }
            _context.AddRange(participants);
            await _context.SaveChangesAsync();
            return new() { IsSuccess = true };
        }

        public Task<Response<List<UserRoleModel>>> GetUsers(int projectId)
        {
            var participants = _context.Participants.AsNoTracking().Include(p => p.Role).Include(p => p.User).Where(p=>p.ProjectId.Equals(projectId)).ToList();
            if (participants is null)
                return Task.FromResult( new Response<List<UserRoleModel>>() { IsSuccess= false, Reason="Not Found" });
            List<UserRoleModel> users = new();
            for (int i = 0; i < participants.Count; i++)
            {
#pragma warning disable CS8602 // Разыменование вероятной пустой ссылки.
                users.Add(new() { Role = participants[i].Role.ToDto(), User = participants[i].User.ToDto() });
#pragma warning restore CS8602 // Разыменование вероятной пустой ссылки.
            }
            return Task.FromResult( new Response<List<UserRoleModel>>() { IsSuccess = true, Model = users });
        }

        public async Task<List<ProjectModel>> GetAllAsync()
        {
            var project = await _context.Projects.AsNoTracking().Select(d => d.ToDto()).ToListAsync();

            if (project.Count < 1)
                throw new NotFoundException("Not found projects");

            return project;
        }

        public async Task<ProjectModel> GetByIdAsync(int id)
        {
            var project = await _context.Projects.AsNoTracking().FirstOrDefaultAsync(p => p.Id.Equals(id));

            if (project is null)
                throw new NotFoundException("Not found project");

            return project.ToDto();
        }

        public async Task<ProjectModel> CreateAsync(ProjectModel model)
        {
            var project = new Project()
            {
                Name = model.Name,
                Status = model.Status,
                CreatorId = model.CreatorId,
                CreationData = DateTime.UtcNow,
                Description = model.Description,
            };
            _context.Projects.Add(project);


            var role = new Role()
            {
                Name = "Admin",
                Project = project,
                AllowedAddUsersProject = true,
                AllowedDeleteProject = true,
                AllowedEditProject = true,

            };
            _context.Roles.Add(role);

            var participant = new ProjectParticipant()
            {
                Role = role,
                Project = project,
                UserId = project.CreatorId ?? 0,
            };
            _context.Participants.Add(participant);

            await _context.SaveChangesAsync();
            model.Id = project.Id;
            return model;
        }

        public async Task UpdateAsync(ProjectModel model)
        {
            var countUpdate = await _context.Projects.Where(d => d.Id == model.Id)
                .ExecuteUpdateAsync(setter => setter
                .SetProperty(o => o.Status, model.Status)
                .SetProperty(o => o.Description, model.Description)
                .SetProperty(o => o.Name, model.Name));

            if (countUpdate < 1)
                throw new NotFoundException("Not found project");
        }

        public async Task DeleteAsync(int id)
        {
            var countDelete = await _context.Projects.Where(d => d.Id == id).ExecuteDeleteAsync();

            if (countDelete < 1)
                throw new NotFoundException("Not found desk");
        }
    }
}
