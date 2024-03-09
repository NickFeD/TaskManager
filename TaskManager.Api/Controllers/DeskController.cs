using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Api.Controllers.Abstracted;
using TaskManager.Api.Data;
using TaskManager.Api.Exceptions;
using TaskManager.Api.Services;
using TaskManager.Command.Models;

namespace TaskManager.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class DeskController(ApplicationContext context) : ControllerBase, ICRUDControllerAsync<DeskModel>
    {
        private readonly DeskService _service = new(context);

        /// <summary>
        /// Create a desk
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(DeskModel), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateAsync(DeskModel model)
        {
            var modelToCreate = await _service.CreateAsync(model);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = modelToCreate.Id }, modelToCreate);
        }

        /// <summary>
        /// Delete a desk
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _service.DeleteAsync(id);
            return Ok();
        }

        /// <summary>
        /// Get all desks
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<DeskModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _service.GetAllAsync());
        }

        /// <summary>
        /// Get a desk by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(DeskModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var desk = await _service.GetByIdAsync(id);
            return Ok(desk);
        }

        /// <summary>
        /// Update the desk
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateAsync(int id, DeskModel model)
        {
            await _service.UpdateAsync(model);
            return Ok();
        }
    }
}
