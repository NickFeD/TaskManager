using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Api.Controllers.Abstracted;
using TaskManager.Core.Contracts.Services;
using TaskManager.Core.Models;
using TaskManager.Infrastructure.Services;

namespace TaskManager.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class BoardController(IBoardService boardService, IPermissionService permissionService) : BaseController //ICRUDController<BoardModel, Guid>
    {
        private readonly IBoardService _boardService = boardService;
        private readonly IPermissionService _permissionService = permissionService;

        /// <summary>
        /// Create a desk
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(BoardModel), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(BoardCreateModel model)
        { 
            await _permissionService.Board(AuthUser.Id,model.ProjectId,Core.Enums.AllowedBoard.Add);
            var modelToCreate = await _boardService.CreateAsync(model);
            return CreatedAtAction(nameof(GetById), new { id = modelToCreate.Id }, modelToCreate);
        }

        /// <summary>
        /// Delete a desk
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _permissionService.Board(AuthUser.Id, id, Core.Enums.AllowedBoard.Delete);
            await _boardService.DeleteAsync(id);
            return Ok();
        }

        /// <summary>
        /// Get all desks
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<BoardModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _boardService.GetAllAsync());
        }

        /// <summary>
        /// Get a desk by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BoardModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var desk = await _boardService.GetByIdAsync(id);
            return Ok(desk);
        }


        /// <summary>
        /// Update the desk
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Update(Guid id, BoardUpdateModel model)
        {
            await _boardService.UpdateAsync(id, model);
            return Ok();
        }


        [HttpGet("/api/Projects/{id}/Boards")]
        public IAsyncEnumerable<BoardModel> GetBoards(Guid id)
        {
            return _boardService.GetByProjectIdAsync(id);
        }
    }
}
