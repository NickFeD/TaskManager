using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Api.Controllers.Abstracted;
using TaskManager.Api.Data;
using TaskManager.Api.Services;
using TaskManager.Command.Models;
using Microsoft.AspNetCore.Http;

namespace TaskManager.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ParticipantController(ApplicationContext context) : ControllerBase, ICRUDController<ProjectParticipantModel>
    {
        private readonly ParticipantService _service = new(context);

        /// <summary>
        /// Create a participant
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Create([FromBody] ProjectParticipantModel model)
        {
            var modelToCreate = await _service.CreateAsync(model);
            return CreatedAtAction(nameof(GetById), new { id = modelToCreate.Id }, modelToCreate);
        }

        /// <summary>
        /// Delete a participant
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return Ok();
        }

        /// <summary>
        /// Get all participants
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _service.GetAllAsync();
            return Ok(response);
        }

        /// <summary>
        /// Get a participant by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _service.GetByIdAsync(id);
            return Ok(response);
        }

        /// <summary>
        /// Update the participant
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, ProjectParticipantModel model)
        {
            await _service.UpdateAsync(model);
            return Ok();
        }
    }
}
