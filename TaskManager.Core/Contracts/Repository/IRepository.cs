using System.Linq.Expressions;
using TaskManager.Core.Entities;

namespace TaskManager.Core.Contracts.Repository;

public interface IRepository<T, TId> where T : IEntity<TId>
{
    Task<TId> InsertAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task DeleteAsync(TId id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> GetByConditionAsync(Expression<Func<T, bool>> filter);
    Task<T?> GetFirstByConditionAsync(Expression<Func<T, bool>> filter);
    Task<T?> GetByIdAsync(TId id);
    Task<T> AddAsync(T entity);
    Task<bool> ContainsdAsync(TId id);
    Task<bool> ContainsdByConditionAsync(Expression<Func<T, bool>> filter);
}
