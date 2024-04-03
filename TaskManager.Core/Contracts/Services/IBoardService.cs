using TaskManager.Core.Models;

namespace TaskManager.Core.Contracts.Services
{
    public interface IBoardService : ICRUDService<BoardModel, Guid>
    {
    }
}
