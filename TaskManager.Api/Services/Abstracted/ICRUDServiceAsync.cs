using Microsoft.AspNetCore.Mvc;
using TaskManager.Api.Data;
using TaskManager.Command.Models;

namespace TaskManager.Api.Services.Abstracted
{
    interface ICRUDServiceAsync<TModel> where TModel : class 
    {
        Task<List<TModel>> GetAllAsync();
        Task<TModel> GetByIdAsync(int id);
        Task<TModel> CreateAsync(TModel model);
        Task UpdateAsync(TModel model);
        Task DeleteAsync(int id);

    }
}
