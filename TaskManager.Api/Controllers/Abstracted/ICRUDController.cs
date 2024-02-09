using Microsoft.AspNetCore.Mvc;
using TaskManager.Command.Models.Abstracted;

namespace TaskManager.Api.Controllers.Abstracted
{
    interface ICRUDController<TModel> where TModel : Model
    {
        [HttpGet]
        Task<IActionResult> GetAll();

        [HttpGet("{id}")]
        Task<IActionResult> GetById(int id);

        [HttpPost]
        Task<IActionResult> Create(TModel model);

        [HttpPut("{id}")]
        Task<IActionResult> Update(int id, TModel model);

        [HttpDelete("{id}")]
        Task<IActionResult> Delete(int id);
    }
}
