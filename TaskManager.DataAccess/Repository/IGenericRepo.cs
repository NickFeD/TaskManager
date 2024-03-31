using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.DataAccess.Repository;

interface IGenericRepo<T>
{
    Task<IEnumerable<T>> GetAll();
    Task<T> Get(Expression<Func<T, bool>> func);
    Task<T> Add(T obj);
    Task Update(Expression<Func<T, bool>> func, Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> setPropertyCalls);
    Task Delete(Expression<Func<T, bool>> func);
}
