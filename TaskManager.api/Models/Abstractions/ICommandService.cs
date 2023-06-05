namespace TaskManager.Api.Models.Abstractions
{
    public interface ICommandService<T>
    {
        T Get(int id);
        bool Create(T model);
        bool Update(int index, T model);
        bool Delete(int index);
    }
}
