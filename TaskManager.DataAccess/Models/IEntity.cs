namespace TaskManager.DataAccess.Models;

public interface IEntity<TModel>
{
    TModel ToDto();
}
