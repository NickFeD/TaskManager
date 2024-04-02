using Microsoft.AspNetCore.Mvc;
using TaskManager.Core.Models.Abstracted;

namespace TaskManager.Api.Controllers.Abstracted
{
    public interface ICRUDController<TModel,TId> where TModel : Model
    {
        [HttpGet]
        public Task<IActionResult> GetAll();

        [HttpGet("{id}")]
        public Task<IActionResult> GetById(TId id);

        [HttpPost]
        public Task<IActionResult> Create(TModel model);

        [HttpPut("{id}")]
        public Task<IActionResult> Update(TId id, TModel model);

        [HttpDelete("{id}")]
        public Task<IActionResult> Delete(TId id);
    }
}
