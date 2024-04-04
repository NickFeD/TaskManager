using AutoMapper;
using TaskManager.Core.Contracts.Repository;
using TaskManager.Core.Contracts.Services;
using TaskManager.Core.Entities;
using TaskManager.Core.Exceptions;
using TaskManager.Core.Models;

namespace TaskManager.Infrastructure.Services
{
    public class BoardService(IBoardRepository boardRepository, IMapper mapper, IProjectRepository projectRepository) : IBoardService
    {
        private readonly IBoardRepository _boardRepository = boardRepository;
        private readonly IProjectRepository _projectRepository = projectRepository;
        private readonly IMapper _mapper = mapper;
        public async Task<BoardModel> CreateAsync(BoardCreateModel model)
        {
            var containd = _projectRepository.ContainsdAsync(model.ProjectId);
            var board = _mapper.Map<Board>(model);
            board.Id = Guid.NewGuid();
            board.CreationData = DateTime.UtcNow;
            if (!await containd)
                throw new BadRequestException("Invalid project uuid");

            board = await _boardRepository.AddAsync(board);

            return _mapper.Map<BoardModel>(board);
        }

        public Task DeleteAsync(Guid id)
            => _boardRepository.DeleteAsync(id);

        public async Task<IEnumerable<BoardModel>> GetAllAsync()
        {
            return (await _boardRepository.GetAllAsync())
                .Select(b => _mapper.Map<BoardModel>(b)).ToList();
        }

        public async Task<BoardModel> GetByIdAsync(Guid id)
        {
            var board = await _boardRepository.GetByIdAsync(id);
            return _mapper.Map<BoardModel>(board);
        }

        public async Task UpdateAsync(Guid id, BoardUpdateModel model)
        {
            var board = _mapper.Map<Board>(model);
            board.Id = id;

            await _boardRepository.UpdateAsync(board);
        }
    }
}
