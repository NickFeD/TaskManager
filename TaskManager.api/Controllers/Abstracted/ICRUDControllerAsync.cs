using Microsoft.AspNetCore.Mvc;
using TaskManager.Command.Models.Abstracted;

namespace TaskManager.Api.Controllers.Abstracted
{
    public interface ICRUDControllerAsync<TModel> where TModel : Model
    {
        [HttpGet]
        public Task<IActionResult> GetAllAsync();

        [HttpGet("{id}")]
        public Task<IActionResult> GetByIdAsync(int id);

        [HttpPost]
        public Task<IActionResult> CreateAsync(TModel model);

        [HttpPut("{id}")]
        public Task<IActionResult> UpdateAsync(int id, TModel model);

        [HttpDelete("{id}")]
        public Task<IActionResult> DeleteAsync(int id);
    }
}
