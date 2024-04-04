using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Api.Controllers.Abstracted;
using TaskManager.Core.Contracts.Services;
using TaskManager.Core.Models;

namespace TaskManager.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ParticipantController(IParticipantService participantService) : BaseController //ICRUDController<ParticipantModel, Guid>
    {
        private readonly IParticipantService _participantService = participantService;

        /// <summary>
        /// Create a participant
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Create([FromBody] ParticipantCreateModel model)
        {
            var modelToCreate = await _participantService.CreateAsync(model);
            return CreatedAtAction(nameof(GetById), new { id = modelToCreate.Id }, modelToCreate);
        }

        /// <summary>
        /// Delete a participant
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _participantService.DeleteAsync(id);
            return NoContent();
        }

        /// <summary>
        /// Get all participants
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _participantService.GetAllAsync();
            return Ok(response);
        }

        /// <summary>
        /// Get a participant by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await _participantService.GetByIdAsync(id);
            return Ok(response);
        }

        /// <summary>
        /// Update the participant
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Update(Guid id, ParticipantUpdateModel model)
        {
            await _participantService.UpdateAsync(id, model);
            return NoContent();
        }
    }
}
