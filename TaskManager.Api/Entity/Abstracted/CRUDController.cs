﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.Api.Data;
using TaskManager.Api.Services;
using TaskManager.Command.Models.Abstracted;

namespace TaskManager.Api.Models.Abstracted
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class CRUDController<TModel, TService> : ControllerBase, ICRUDControllers<TModel>
        where TModel : Model 
        where TService : ICRUDService<TModel>
    {
        protected readonly TService _service;

        protected CRUDController(TService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public ActionResult<TModel> GetById(int id)
        {
            var model = _service.GetById(id);
            return model is null ? NotFound() :model;
        }

        [HttpPost]
        public IActionResult Create([FromBody] TModel model)
        {
            _service.Create(model);
            return CreatedAtAction(nameof(GetById), new { id = model.Id }, model);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, TModel model)
        {
            var existingModel = _service.GetById(id);
            if (existingModel == null)
                return NotFound();
            _service.Update(model);
            return NoContent();
        }
        
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var modelToDelete = _service.GetById(id);
            if (modelToDelete != null)
                return NotFound();
            _service.Delete(id);
            return NoContent();
        }
    }
}