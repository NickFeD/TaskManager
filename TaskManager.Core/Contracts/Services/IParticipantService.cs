using TaskManager.Core.Models;

namespace TaskManager.Core.Contracts.Services
{
    public interface IParticipantService //: ICRUDService<ParticipantModel, Guid>
    {
        Task<ParticipantModel> CreateAsync(ParticipantCreateModel model);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<ParticipantModel>> GetAllAsync();
        Task<ParticipantModel> GetByIdAsync(Guid id);
        Task UpdateAsync(Guid id, ParticipantUpdateModel model);
    }
}
