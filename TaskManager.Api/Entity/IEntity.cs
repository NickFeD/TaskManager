namespace TaskManager.Api.Entity
{
    public interface IEntity<TModel>
    {
        TModel ToDto();
    }
}
