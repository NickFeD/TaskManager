using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using TaskManager.Core.Exceptions;

namespace TaskManager.DataAccess.Repository;

public class GenericRepo<T> : IGenericRepo<T> where T : class
{
    //The following variable is going to hold the EmployeeDBContext instance
    private readonly TaskManagerDbContext _context;

    private readonly DbSet<T> table;


    public GenericRepo(TaskManagerDbContext _context)
    {
        this._context = _context;
        table = _context.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAll()
    {
        return await table.AsNoTracking().ToListAsync();
    }

    public async Task<T> Get(Expression<Func<T,bool>> func)
    {
        var obj = await table.AsNoTracking().FirstOrDefaultAsync(func);
        if (obj is null)
            throw new NotFoundException($"{typeof(T).ToString().ToLower()} not found");

        return obj;
    }

    public async Task<T> Add(T obj)
    {
        table.Add(obj);
        await _context.SaveChangesAsync();
        return obj;
    }

    public async Task Update(Expression<Func<T, bool>> func, Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> setPropertyCalls)
    {
        var count = await table.Where(func).ExecuteUpdateAsync(setPropertyCalls);

        if (count < 1)
            throw new NotFoundException($"{typeof(T).ToString().ToLower()} not found");
    }

    public async Task Delete(Expression<Func<T, bool>> func)
    {
        var count = await table.Where(func).ExecuteDeleteAsync();

        if (count < 1)
            throw new NotFoundException($"{typeof(T).ToString().ToLower()} not found");
    }
}
