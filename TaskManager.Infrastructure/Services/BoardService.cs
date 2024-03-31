using TaskManager.Core.Contracts.Repository;
using TaskManager.Core.Contracts.Services;
using TaskManager.Core.Extentions;
using TaskManager.Core.Models;

namespace TaskManager.Infrastructure.Services
{
    public class BoardService(IBoardRepository boardRepository) :IBoardService
    {
        private readonly IBoardRepository _boardRepository = boardRepository;

        public async Task<BoardModel> CreateAsync(BoardModel model)
        {
            var board = model.ToEntity();
            board = await _boardRepository.AddAsync(board);
            model.Id = board.Id;
            return model;
        }

        public Task DeleteAsync(Guid id) 
            => _boardRepository.DeleteAsync(id);

        public async Task<List<BoardModel>> GetAllAsync()
        {
            return (await _boardRepository.GetAllAsync())
                .Select(m=>m.ToModel()).ToList();
        }

        public async Task<BoardModel> GetByIdAsync(Guid id)
        {
            var board = await _boardRepository.GetByIdAsync(id);
            return board.ToModel();
        }

        public async Task UpdateAsync(BoardModel model)
        {
             await _boardRepository.UpdateAsync(model.ToEntity());
        }

        
    }
}
