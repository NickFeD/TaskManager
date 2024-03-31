using Microsoft.EntityFrameworkCore;
using TaskManager.Api.Data;
using TaskManager.Api.Entity;
using TaskManager.Api.Exceptions;
using TaskManager.Infrastructure.Services.Abstracted;
using TaskManager.Command.Models;
using Task = System.Threading.Tasks.Task;

namespace TaskManager.Infrastructure.Services
{
    public class ParticipantService : ICRUDServiceAsync<ProjectParticipantModel>
    {
        private readonly ApplicationContext _context;

        public ParticipantService(ApplicationContext context) { _context = context; }

        public async Task<ProjectParticipantModel> CreateAsync(ProjectParticipantModel model)
        {
            var participant = new ProjectParticipant()
            {
                ProjectId = model.Id,
                UserId = model.UserId,
                UserRoleId = model.UserRoleId,
            };
            _context.Participants.Add(participant);
            await _context.SaveChangesAsync();
            model.Id = participant.Id;
            return model;
        }

        public async Task DeleteAsync(int id)
        {
            var countDelete = await _context.Participants.Where(d => d.Id == id).ExecuteDeleteAsync();

            if (countDelete < 1)
                throw new NotFoundException("Not found participant");
        }

        public async Task<List<ProjectParticipantModel>> GetAllAsync()
        {
            var participant = await _context.Participants.AsNoTracking().Select(d => d.ToDto()).ToListAsync();

            if (participant.Count < 1)
                throw new NotFoundException("Not found participants");

            return participant;
        }

        public async Task<ProjectParticipantModel> GetByIdAsync(int id)
        {
            var participant = await _context.Participants.AsNoTracking().FirstOrDefaultAsync(p => p.Id.Equals(id));

            if (participant is null)
                throw new NotFoundException("Not found participant");

            return participant.ToDto();
        }

        public async Task UpdateAsync(ProjectParticipantModel model)
        {
            var countUpdate = await _context.Participants.Where(d => d.Id == model.Id)
                .ExecuteUpdateAsync(setter => setter
                .SetProperty(o => o.ProjectId, model.ProjectId)
                .SetProperty(o => o.UserId, model.UserId)
                .SetProperty(o => o.UserRoleId, model.UserRoleId));

            if (countUpdate < 1)
                throw new NotFoundException("Not found participant");
        }
    }
}
