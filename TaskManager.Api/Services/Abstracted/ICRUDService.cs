using Microsoft.AspNetCore.Mvc;
using TaskManager.Api.Data;

namespace TaskManager.Api.Services.Abstracted
{
    public interface ICRUDService<T> where T : class
    {
        public List<T> GetAll();
        public T? GetById(int id);
        public T Create(T model);
        public void Update(T model);
        public void Delete(int id);
    }
}
