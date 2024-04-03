using TaskManager.Core.Contracts.Repository;
using TaskManager.Core.Contracts.Services;
using TaskManager.Core.Entities;
using TaskManager.Core.Exceptions;
using TaskManager.Core.Extentions;
using TaskManager.Core.Models.Project;

namespace TaskManager.Infrastructure.Services
{
    public class ProjectService(IProjectRepository projectRepository) : IProjectService
    {
        private readonly IProjectRepository _projectRepository = projectRepository;

        public async Task<ProjectModel> UpdateAsync(Guid id, ProjectUpdateModel model)
        {
            if (await _projectRepository.ContainsdAsync(id))
                throw new NotFoundException("progect not found");

            Project project = new()
            {
                Id = id,
                Name = model.Name,
                Description = model.Description,
                Status = model.Status,
            };
            return (await _projectRepository.UpdateAsync(project)).ToModel();
        }

        public async Task AddUsers(Guid projectId, Guid roleId, params Guid[] usersId)
        {
            var participants = new ProjectParticipant[usersId.Length];
            for (int i = 0; i < usersId.Length; i++)
            {
                var participant = new ProjectParticipant()
                {
                    UserId = usersId[i],
                    ProjectId = projectId,
                    RoleId = roleId,
                };
                participants[i] = participant;
            }
            await _projectRepository.AddRangeAsync(projectId, participants);
        }

        public async Task<IEnumerable<ProjectModel>> GetAllAsync()
        {
            var project = await _projectRepository.GetAllAsync();

            return project.Select(p => p.ToModel());
        }

        public async Task<ProjectModel> GetByIdAsync(Guid id)
        {
            var project = await _projectRepository.GetByIdAsync(id);

            return project.ToModel();
        }

        public async Task<ProjectModel> CreateAsync(ProjectModel model)
        {

            var role = new Role()
            {
                Name = "Admin",
                AllowedAddUsersProject = true,
                AllowedDeleteProject = true,
                AllowedEditProject = true,

            };
            var participant = new ProjectParticipant()
            {
                Role = role,
                UserId = model.CreatorId!.Value,
            };
            var project = new Project()
            {
                Name = model.Name,
                Status = Core.Enums.ProjectStatus.InProgress,
                CreatorId = model.CreatorId,
                CreationData = DateTime.UtcNow,
                Description = model.Description,
            };
            role.Project = project;
            role.Participants.Add(participant);

            participant.Project = project;
            participant.Role = role;

            project.Participants.Add(participant);
            project.Roles.Add(role);

            project = await _projectRepository.AddAsync(project);
            model.Id = project.Id;
            return model;
        }

        public Task DeleteAsync(Guid id)
            => _projectRepository.DeleteAsync(id);
    }
}
