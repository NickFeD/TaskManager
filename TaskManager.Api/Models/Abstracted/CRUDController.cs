using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.Api.Data;
using TaskManager.Api.Services;

namespace TaskManager.Api.Models.Abstracted
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class CRUDController<T, TService> : ControllerBase, ICRUDControllers<T> where T : IModel where TService : CRUTService<T>
    {
        protected TService _service;

        protected CRUDController(TService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public ActionResult<T> Get(int id)
        {
            var model = _service.GetModel(id);
            return model is null ? NotFound() : model;
        }

        [HttpPost]
        public IActionResult Create([FromBody] T model)
        {
            _service.CreateModel(model);
            return CreatedAtAction(nameof(Get),new { id = model.Id }, model);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var modelToDelete = _service.Get(id);
            if (modelToDelete != null)
                return NotFound();
            _service.DeleteModel(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, T model)
        {
            var existingModel = _service.Get(id);
            if (existingModel == null)
                return NotFound();
            _service.UpdateModel(model);
            return NoContent();
        }
    }
}
