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
    public class TaskController : ControllerBase, ICRUDController<TaskModel>
    {
        private readonly TaskService _service;
        public TaskController(ApplicationContext context) { _service = new(context); }

        /// <summary>
        /// Create a task
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(Response<TaskModel>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Response<TaskModel>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] TaskModel model)
        {
            if (model is null)
                return BadRequest(new Response<ProjectModel> { IsSuccess = false, Reason = "Request null" });
            var modelToCreate = await _service.CreateAsync(model);
            if (!modelToCreate.IsSuccess)
                return BadRequest(modelToCreate);
            return CreatedAtAction(nameof(GetById), new { id = modelToCreate.Model.Id }, modelToCreate);
        }

        /// <summary>
        /// Delete a task
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            var taskToDelete = await _service.GetByIdAsync(id);
            if (!taskToDelete.IsSuccess)
                return BadRequest(taskToDelete);
            _service.Delete(id);
            return Ok(taskToDelete);
        }

        /// <summary>
        /// Get all tasks
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(Response<List<TaskModel>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<List<TaskModel>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAll()
        {
            var response = await _service.GetAllAsync();
            if (!response.IsSuccess)
                return BadRequest(response);
            return Ok(response);
        }

        /// <summary>
        /// Get a task by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Response<List<TaskModel>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<List<TaskModel>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _service.GetByIdAsync(id);
            if (!response.IsSuccess)
                return BadRequest(response);
            return Ok(response);
        }

        /// <summary>
        /// Update the task
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, TaskModel model)
        {
            var response = await _service.UpdateAsync(model);
            if (!response.IsSuccess)
                return BadRequest(response);
            return Ok(response);
        }
    }
}
