using TaskManager.Core.Entities;

namespace TaskManager.Core.Contracts.Repository;

public interface IBoardRepository : IRepository<Board, Guid>
{
}