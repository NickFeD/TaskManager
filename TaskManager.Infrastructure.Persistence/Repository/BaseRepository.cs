﻿using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TaskManager.Core.Contracts.Repository;
using TaskManager.Core.Entities;
using TaskManager.Core.Exceptions;

namespace TaskManager.Infrastructure.Persistence.Repository;

public class BaseRepository<T, TId> : IRepository<T, TId> where T : class, IEntity<TId>
{
    protected readonly TaskManagerDbContext _context;
    private readonly DbSet<T> table;

    public BaseRepository(TaskManagerDbContext context)
    {
        _context = context;
        table = _context.Set<T>();
    }
    public async Task DeleteAsync(TId id)
    {
        var count = await table.Where(e => e.Id!.Equals(id)).ExecuteDeleteAsync();
        if (count < 1)
            throw new NotFoundException($"{typeof(T).ToString().ToLower()} not found");
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await table.AsNoTracking().ToListAsync();
    }

    public async Task<IEnumerable<T>> GetByConditionAsync(Expression<Func<T, bool>> filter)
        => await table.AsNoTracking().Where(filter).ToListAsync();


    public async Task<T?> GetByIdAsync(TId id)
    {
        return await table.AsNoTracking().FirstOrDefaultAsync(e => e.Id!.Equals(id))
            ?? throw new NotFoundException($"{typeof(T).ToString().ToLower()} not found");
    }

    public async Task<TId> InsertAsync(T entity)
    {
        await table.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity.Id;
    }

    public async Task<T> AddAsync(T entity)
    {
        await table.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<T> UpdateAsync(T entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return entity;
    }

    public Task<T?> GetFirstByConditionAsync(Expression<Func<T, bool>> filter)
        => table.AsNoTracking().FirstOrDefaultAsync(filter);

    public Task<bool> ContainsdAsync(TId id)
        => ContainsdByConditionAsync(e => e.Id!.Equals(id));


    public async Task<bool> ContainsdByConditionAsync(Expression<Func<T, bool>> filter)
    {
        return await table.AnyAsync(filter);
    }
}
