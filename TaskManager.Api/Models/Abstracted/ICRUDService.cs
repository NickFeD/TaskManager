using Microsoft.AspNetCore.Mvc;

namespace TaskManager.Api.Models.Abstracted
{
    public interface ICRUDService<T>
    {
        public abstract T Get(int id);
        public abstract T Create(T model);
        public abstract void Update(T model);
        public abstract void Delete(int id);
    }
}
