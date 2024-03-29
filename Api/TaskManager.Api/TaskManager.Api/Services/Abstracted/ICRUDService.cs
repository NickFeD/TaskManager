using Microsoft.AspNetCore.Mvc;
using TaskManager.Api.Data;
using TaskManager.Command.Models;

namespace TaskManager.Api.Services.Abstracted
{
    public interface ICRUDService<TModel> where TModel : class 
    {
        Response<List<TModel>> GetAll();
        Response<TModel> GetById(int id);
        Response<TModel> Create(TModel model);
        Response Update(TModel model);
        Response Delete(int id);
    }
}
