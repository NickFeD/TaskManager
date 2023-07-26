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
                users.Add(new() { Role = participants[i].Role.ToDto(), User = participants[i].User.ToDto() });
            }
            return Task.FromResult( new Response<List<UserRoleModel>>() { IsSuccess = true, Model = users });
        }
    }
}
