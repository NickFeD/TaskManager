namespace TaskManager.Core.Contracts.Services
{
    public interface ICRUDService<TModel, TId> where TModel : class
    {
        Task<List<TModel>> GetAllAsync();
        Task<TModel> GetByIdAsync(TId id);
        Task<TModel> CreateAsync(TModel model);
        Task UpdateAsync(TModel model);
        Task DeleteAsync(TId id);

    }
}
