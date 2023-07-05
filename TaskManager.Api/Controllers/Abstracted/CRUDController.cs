using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.Api.Data;
using TaskManager.Api.Services.Abstracted;
using TaskManager.Command.Models.Abstracted;

namespace TaskManager.Api.Controllers.Abstracted
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

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public virtual ActionResult<List<TModel>> GetAll()
        {
            var model = _service.GetAll();
            return model is null ? NotFound() : model;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public virtual IActionResult GetById(int id)
        {
            var model = _service.GetById(id);
            return model is null ? NotFound() :Ok(model);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public virtual IActionResult Create([FromBody] TModel model)
        {
           var modelToCreate = _service.Create(model);
            if (modelToCreate is null)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(GetById), new { id = modelToCreate.Id }, modelToCreate);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public virtual IActionResult Update(int id, TModel model)
        {
            var existingModel = _service.GetById(id);
            if (existingModel == null)
                return NotFound();
            model.Id = id;
            _service.Update(model);
            return NoContent();
        }
        
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public virtual IActionResult Delete(int id)
        {
            var modelToDelete = _service.GetById(id);
            if (modelToDelete == null)
                return NotFound();
            _service.Delete(id);
            return NoContent();
        }
    }
}
