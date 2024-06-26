﻿using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Api.Controllers.Abstracted;
using TaskManager.Core.Contracts.Services;
using TaskManager.Core.Entities;
using TaskManager.Core.Models;

namespace TaskManager.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController(ITaskService taskService) : BaseController
    {
        private readonly ITaskService _taskService = taskService;

        /// <summary>
        /// Create a task
        /// </summary>
        /// <param name="createModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(TaskCreateModel), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] TaskCreateModel createModel)
        {
            var model = createModel.Adapt<TaskEntity>();
            model.Id = Guid.NewGuid();
            model.СreatorId = AuthUser.Id;
            model.CreationData = DateTime.UtcNow;


            var modelToCreate = await _taskService.CreateAsync(model);
            return CreatedAtAction(nameof(GetById), new { id = modelToCreate.Id }, modelToCreate);
        }

        /// <summary>
        /// Delete a task
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _taskService.DeleteAsync(id);
            return NoContent();
        }

        /// <summary>
        /// Get all tasks
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IAsyncEnumerable<TaskModel> GetAll()
        {
            return _taskService.GetAllAsync();
        }

        /// <summary>
        /// Get a task by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await _taskService.GetByIdAsync(id);
            return Ok(response);
        }

        /// <summary>
        /// Update the task
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateModel"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update(Guid id, TaskUpdateModel updateModel)
        {
            await _taskService.UpdateAsync(id, updateModel);
            return Ok();
        }

        [HttpGet("/api/board/{id}/task")]
        public IAsyncEnumerable<TaskModel> GetByIdTask(Guid id)
        {
            return _taskService.GetByBoardIdAsync(id);
        }
    }
}
