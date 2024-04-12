using Mapster;
using Microsoft.EntityFrameworkCore;
using TaskManager.Core.Contracts.Services;
using TaskManager.Core.Entities;
using TaskManager.Core.Exceptions;
using TaskManager.Core.Models;
using TaskManager.Infrastructure.Persistence;

namespace TaskManager.Infrastructure.Services
{
    public class ParticipantService(TaskManagerDbContext context) : IParticipantService
    {
        private readonly TaskManagerDbContext _context = context;

        public async Task AddUsersInProject(Guid projectId, Guid roleId, Guid[] usersId)
        {
            var taskProject = _context.Projects.FindAsync(projectId);

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
            var project = await taskProject;

            if (project is null)
                throw new NotFoundException("Invalid project uuid");
            project.Participants.AddRange(participants);
            await _context.SaveChangesAsync();
        }

        public async Task<ParticipantModel> CreateAsync(ParticipantCreateModel model)
        {
            await ValidateDetails(model.ProjectId, model.UserId, model.RoleId);

            var participant = model.Adapt<ProjectParticipant>();
            participant.Id = Guid.NewGuid();

            await _context.Participant.AddAsync(participant);
            return participant.Adapt<ParticipantModel>();
        }

        public Task DeleteAsync(Guid id)
            => _context.Participant.Where(p => p.Id == id).ExecuteDeleteAsync();

        public async Task DeleteParticipantInProjectAsync(Guid projectId, Guid userId)
        {
            var count = await _context.Participant
                .Where(p => p.ProjectId == projectId && p.UserId == userId)
                .ExecuteDeleteAsync();
            if (count < 1)
                throw new NotFoundException("Invalid user uuid");
        }

        public Task<List<ParticipantModel>> GetAllAsync()
        {
            return _context.Participant.AsNoTracking().ProjectToType<ParticipantModel>().ToListAsync();
        }

        public async Task<ParticipantModel> GetByIdAsync(Guid id)
        {
            var participant = await _context.Participant.AsNoTracking().ProjectToType<ParticipantModel>().SingleOrDefaultAsync(p => p.Id == id);

            if (participant is null)
                throw new NotFoundException("Invalid participant uuid");

            return participant;
        }

        public async Task UpdateAsync(Guid id, ParticipantUpdateModel model)
        {
            await ValidateDetails(model.ProjectId, model.UserId, model.RoleId);

            var count = await _context.Participant.Where(p => p.Id == id)
                .ExecuteUpdateAsync(setters => setters
                .SetProperty(p => p.UserId, model.UserId)
                .SetProperty(p => p.ProjectId, model.ProjectId)
                .SetProperty(p => p.RoleId, model.RoleId));

            if (count < 1)
                throw new NotFoundException("Invalid participant uuid");
        }

        private async Task ValidateDetails(Guid projectId, Guid userId, Guid roleId)
        {

            var roleContaind = _context.Roles.AnyAsync(r => r.Id == roleId);
            var userContaind = _context.Users.AnyAsync(u => u.Id == userId);
            var projectContaind = _context.Projects.AnyAsync(u => u.Id == projectId);

            if (await roleContaind)
                throw new BadRequestException("Invalid role uuid");

            if (await userContaind)
                throw new BadRequestException("Invalid user uuid");

            if (await projectContaind)
                throw new BadRequestException("Invalid project uuid");
        }
    }
}
