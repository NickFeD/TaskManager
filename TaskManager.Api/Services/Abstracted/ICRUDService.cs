using Microsoft.AspNetCore.Mvc;
using TaskManager.Api.Data;

namespace TaskManager.Api.Services.Abstracted
{
    public interface ICRUDService<TModel> where TModel : class 
    {
        List<TModel> GetAll();
        TModel? GetById(int id);
        TModel? Create(TModel model);
        void Update(TModel model);
        void Delete(int id);
    }
}
