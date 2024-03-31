using TaskManager.Core.Contracts.Repository;
using TaskManager.Core.Entities;

namespace TaskManager.Infrastructure.Persistence.Repository;

public class BoardRepository(TaskManagerDbContext context) : BaseRepository<Board, Guid>(context), IBoardRepository
{
}