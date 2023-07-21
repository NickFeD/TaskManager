using Microsoft.AspNetCore.Mvc;
using TaskManager.Api.Data;
using TaskManager.Command.Models;

namespace TaskManager.Api.Services.Abstracted
{
    public interface ICRUDServiceAsync<TModel> where TModel : class 
    {
        Task<Response<List<TModel>>> GetAllAsync();
        Task<Response<TModel>> GetByIdAsync(int id);
        Task<Response<TModel>> CreateAsync(TModel model);
        Task<Response> UpdateAsync(TModel model);
        Task<Response> DeleteAsync(int id);

    }
}
