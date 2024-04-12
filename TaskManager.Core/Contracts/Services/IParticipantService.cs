using TaskManager.Core.Models;

namespace TaskManager.Core.Contracts.Services
{
    public interface IParticipantService //: ICRUDService<ParticipantModel, Guid>
    {
        Task AddUsersInProject(Guid id, Guid roleId, Guid[] userId);
        Task<ParticipantModel> CreateAsync(ParticipantCreateModel model);
        Task DeleteAsync(Guid id);
        Task DeleteParticipantInProjectAsync(Guid projectId, Guid userId);
        Task<List<ParticipantModel>> GetAllAsync();
        Task<ParticipantModel> GetByIdAsync(Guid id);
        Task UpdateAsync(Guid id, ParticipantUpdateModel model);
    }
}
