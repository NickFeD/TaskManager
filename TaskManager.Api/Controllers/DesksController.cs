using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Api.Controllers.Abstracted;
using TaskManager.Api.Data;
using TaskManager.Api.Services;
using TaskManager.Command.Models;

namespace TaskManager.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class DesksController : ControllerBase, ICRUDController<DeskModel>
    {
        private readonly DeskService _service;
        public DesksController(ApplicationContext context)
        {
            _service = new(context);
        }
        /// <summary>
        /// Create a desk
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(Response<DeskModel>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response<DeskModel>), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] DeskModel model)
        {
            if (model is null)
                return BadRequest(new Response<DeskModel> { IsSuccess = false, Reason = "You didn't send anything" });
            var modelToCreate = await _service.CreateAsync(model);
            if (!modelToCreate.IsSuccess)
                return BadRequest(modelToCreate);
            return CreatedAtAction(nameof(GetById), new { id = modelToCreate.Model.Id }, modelToCreate);
        }

        /// <summary>
        /// Delete a desk
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            var deskToDelete = await _service.GetByIdAsync(id);
            if (!deskToDelete.IsSuccess)
                return BadRequest(new Response { IsSuccess = false, Reason = deskToDelete.Reason });
            var response = _service.Delete(id);
            return Ok(response);
        }

        /// <summary>
        /// Get all desks
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(Response<List<DeskModel>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<List<DeskModel>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAll()
        {
            var desks = await _service.GetAllAsync();
            if (!desks.IsSuccess)
                return BadRequest(desks);
            return Ok(desks);
        }

        /// <summary>
        /// Get a desk by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Response<DeskModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<DeskModel>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetById(int id)
        {
            var desk = await _service.GetByIdAsync(id);
            if (!desk.IsSuccess)
                return BadRequest(desk);
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
        [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, DeskModel model)
        {
            var response = await _service.UpdateAsync(model);
            if (!response.IsSuccess)
                return BadRequest(response);
            return Ok(response);
        }
    }
}
