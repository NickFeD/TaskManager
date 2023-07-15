using Microsoft.AspNetCore.Mvc;

namespace TaskManager.Api.Controllers.Abstracted
{
    public interface ICRUDControllers<TModel>
    {
        [HttpGet]
        public Task<IActionResult> GetAll();

        [HttpGet("{id}")]
        public Task<IActionResult> GetById(int id);

        [HttpPost]
        public Task<IActionResult> Create([FromBody] TModel model);

        [HttpPut("{id}")]
        public Task<IActionResult> Update(int id, TModel model);

        [HttpDelete("{id}")]
        public Task<IActionResult> Delete(int id);
    }
}
