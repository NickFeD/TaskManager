using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Core.Contracts.Services
{
    public interface ICRUDService<TModel,TId> where TModel : class
    {
        Task<IEnumerable<TModel>> GetAllAsync();
        Task<TModel> GetByIdAsync(TId id);
        Task<TModel> CreateAsync(TModel model);
        Task UpdateAsync(TModel model);
        Task DeleteAsync(TId id);

    }
}
