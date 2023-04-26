namespace TaskManager.Api.Models.Abstractions
{
    public interface ICommandService<T>
    {
        bool Create(T model);
        bool Update(int index, T model);
        bool Delete(int index);
    }
}
