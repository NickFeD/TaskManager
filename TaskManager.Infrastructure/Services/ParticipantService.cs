using TaskManager.Core.Contracts.Repository;
using TaskManager.Core.Contracts.Services;
using TaskManager.Core.Entities;
using TaskManager.Core.Extentions;
using TaskManager.Core.Models;

namespace TaskManager.Infrastructure.Services
{
    public class ParticipantService(IParticipantRepository participantRepository) : IParticipantService
    {
        private readonly IParticipantRepository _participantRepository = participantRepository;

        public async Task<ParticipantModel> CreateAsync(ParticipantModel model)
        {
            var participant = new ProjectParticipant()
            {
                ProjectId = model.Id,
                UserId = model.UserId,
                RoleId = model.RoleId,
            };
            participant = await _participantRepository.AddAsync(participant);
            model.Id = participant.Id;
            return model;
        }

        public async Task DeleteAsync(Guid id)
        {
            await _participantRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<ParticipantModel>> GetAllAsync()
        {

            var participant = (await _participantRepository.GetAllAsync()).Select(p => p.ToModel());
            return participant.ToList();
        }

        public async Task<ParticipantModel> GetByIdAsync(Guid id)
        {
            var participant = await _participantRepository.GetByIdAsync(id);
            return participant.ToModel();
        }

        public async Task UpdateAsync(ParticipantModel model)
        {
            await _participantRepository.UpdateAsync(model.ToEntity());
        }
    }
}
