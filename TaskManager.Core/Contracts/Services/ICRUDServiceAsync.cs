using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Core.Contracts.Services
{
    public interface ICRUDServiceAsync<TModel,TId> where TModel : class
    {
        Task<List<TModel>> GetAllAsync();
        Task<TModel> GetByIdAsync(TId id);
        Task<TModel> CreateAsync(TModel model);
        Task UpdateAsync(TModel model);
        Task DeleteAsync(TId id);

    }
}
