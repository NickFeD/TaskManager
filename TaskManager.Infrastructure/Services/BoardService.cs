using Mapster;
using Microsoft.EntityFrameworkCore;
using TaskManager.Core.Contracts.Services;
using TaskManager.Core.Entities;
using TaskManager.Core.Exceptions;
using TaskManager.Core.Models;
using TaskManager.Infrastructure.Persistence;

namespace TaskManager.Infrastructure.Services;

public class BoardService(TaskManagerDbContext context) : IBoardService
{
    private readonly TaskManagerDbContext _context = context;

    public async Task<BoardModel> CreateAsync(BoardCreateModel model)
    {
        var containdProjects = _context.Projects.AnyAsync(p => p.Id == model.ProjectId);

        var board = model.Adapt<Board>();
        board.Id = Guid.NewGuid();
        board.CreationData = DateTime.UtcNow;

        if (!await containdProjects)
            throw new BadRequestException("Invalid project uuid");

        await _context.Boards.AddAsync(board);
        await _context.SaveChangesAsync();

        return board.Adapt<BoardModel>();
    }

    public Task DeleteAsync(Guid id)
        => _context.Boards.Where(p => p.Id == id).ExecuteDeleteAsync();

    public Task<List<BoardModel>> GetAllAsync()
    {
        return _context.Boards.ProjectToType<BoardModel>().ToListAsync();
    }

    public async Task<BoardModel> GetByIdAsync(Guid id)
    {
        var board = await _context.Boards.AsNoTracking().ProjectToType<BoardModel>().SingleOrDefaultAsync(p => p.Id == id);

        if (board is null)
            throw new NotFoundException("Invalid board uuid");

        return board;
    }

    public IAsyncEnumerable<BoardModel> GetByProjectIdAsync(Guid id)
    {
        return _context.Boards.Where(b => b.ProjectId.Equals(id)).ProjectToType<BoardModel>().AsAsyncEnumerable();
    }

    public async Task UpdateAsync(Guid id, BoardUpdateModel model)
    {
        var count = await _context.Boards
            .Where(p => p.Id == id)
            .ExecuteUpdateAsync(setters => setters
            .SetProperty(p => p.Name, model.Name)
            .SetProperty(p => p.Description, model.Description));

        if (count < 1)
            throw new NotFoundException("Invalid board uuid");
    }
}
