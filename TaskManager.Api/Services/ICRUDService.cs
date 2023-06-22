using Microsoft.AspNetCore.Mvc;
using TaskManager.Api.Data;

namespace TaskManager.Api.Services
{
    public interface ICRUDService<T>
    {
        public List<T> GetAll();
        public T? GetById(int id);
        public T Create(T model);
        public void Update(T model);
        public void Delete(int id);
    }
}
