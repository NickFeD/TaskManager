using Mapster;
using Microsoft.EntityFrameworkCore;
using TaskManager.Core.Contracts.Services;
using TaskManager.Core.Entities;
using TaskManager.Core.Exceptions;
using TaskManager.Core.Models;
using TaskManager.Infrastructure.Persistence;

namespace TaskManager.Infrastructure.Services;

public class RoleService(TaskManagerDbContext context) : IRoleService
{
    private readonly TaskManagerDbContext _context = context;

    public async Task<RoleModel> CreateAsync(RoleCreateModel model)
    {
        var taskContains = _context.Projects.AnyAsync(p => p.Id == p.Id);
        var role = model.Adapt<Role>();
        role.Id = Guid.NewGuid();

        if (!await taskContains)
            throw new BadRequestException("Invalid role uuid");

        await _context.Roles.AddAsync(role);
        await _context.SaveChangesAsync();
        return role.Adapt<RoleModel>();
    }

    public async Task DeleteAsync(Guid id)
    {
        var count = await _context.Roles.Where(r => r.Id == id).ExecuteDeleteAsync();
        if (count < 1)
            throw new BadRequestException("Invalid role uuid");
    }

    public Task<List<RoleModel>> GetAllAsync()
        => _context.Roles.ProjectToType<RoleModel>().ToListAsync();

    public async Task<RoleModel> GetByIdAsync(Guid id)
    {
        var role = await _context.Roles.AsNoTracking().ProjectToType<RoleModel>().FirstOrDefaultAsync(r => r.Id == id);
        if (role is null)
            throw new BadRequestException("Invalid role uuid");
        return role;
    }

    public async Task UpdateAsync(Guid id, RoleUpdateModel model)
    {
        var taskRole = _context.Roles.AnyAsync(r => r.Id == id);

        var role = model.Adapt<Role>();
        role.Id = id;
        if (!await taskRole)
            throw new BadRequestException("Invalid role uuid");

        _context.Entry(role).State = EntityState.Modified;

        await _context.SaveChangesAsync();

    }
}
