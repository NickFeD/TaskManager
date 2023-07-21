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
    public class ParticipantsController : ControllerBase, ICRUDController<ProjectParticipantModel>
    {
        private readonly ParticipantService _service;
        public ParticipantsController(ApplicationContext context) { _service = new(context); }

        /// <summary>
        /// Create a participant
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(Response<ProjectParticipantModel>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Response<ProjectParticipantModel>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] ProjectParticipantModel model)
        {
            if (model is null)
                return BadRequest(new Response<ProjectModel> { IsSuccess = false, Reason = "Request null" });
            var modelToCreate = await _service.CreateAsync(model);
            if (!modelToCreate.IsSuccess)
                return BadRequest(modelToCreate);
            return CreatedAtAction(nameof(GetById), new { id = modelToCreate.Model.Id }, modelToCreate);
        }

        /// <summary>
        /// Delete a participant
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesDefaultResponseType]
        [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            var participant = await _service.GetByIdAsync(id);
            if (!participant.IsSuccess)
                return BadRequest(participant);
            _service.Delete(id);
            return Ok(participant);
        }

        /// <summary>
        /// Get all participants
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(Response<List<ProjectParticipantModel>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<List<ProjectParticipantModel>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAll()
        {
            var response = await _service.GetAllAsync();
            if (!response.IsSuccess)
                return BadRequest(response);
            return Ok(response);
        }

        /// <summary>
        /// Get a participant by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Response<ProjectModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<ProjectModel>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _service.GetByIdAsync(id);
            if (!response.IsSuccess)
                return BadRequest(response);
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
            var response = await _service.UpdateAsync(model);
            if (!response.IsSuccess)
                return BadRequest(response);
            return Ok(response);
        }
    }
}
