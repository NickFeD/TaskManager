using TaskManager.Core.Models;

namespace TaskManager.Core.Contracts.Services
{
    public interface IBoardService //ICRUDService<BoardModel, Guid>
    {
        Task<BoardModel> CreateAsync(BoardCreateModel model);
        Task DeleteAsync(Guid id);
        Task<List<BoardModel>> GetAllAsync();
        Task<BoardModel> GetByIdAsync(Guid id);
        Task<List<TaskModel>> GetByIdTaskAsync(Guid id);
        Task UpdateAsync(Guid id, BoardUpdateModel model);
    }
}
