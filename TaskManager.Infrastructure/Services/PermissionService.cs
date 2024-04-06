using Microsoft.EntityFrameworkCore;
using System.Security;
using TaskManager.Core.Contracts.Services;
using TaskManager.Core.Enums;
using TaskManager.Core.Exceptions;
using TaskManager.Infrastructure.Persistence;

namespace TaskManager.Infrastructure.Services;

public class PermissionService(TaskManagerDbContext context): IPermissionService
{
    private readonly TaskManagerDbContext _context = context;

    public async Task Project(Guid userId, Guid projectId, AllowedProject allowedProject)
    {
        var projects =  _context.Participant.Where(p=>p.UserId == userId && p.ProjectId == projectId).Include(p=>p.Role);
        var isPermission = false;
        switch (allowedProject)
        {
            case AllowedProject.Edit:
                isPermission = await projects.AnyAsync(p => p.Role!.ProjectEdit);
                break;
            case AllowedProject.Delete:
                isPermission = await projects.AnyAsync(p => p.Role!.ProjectDelete);
                break;
            case AllowedProject.AddUsers:
                isPermission = await projects.AnyAsync(p => p.Role!.ProjectAddUsers);
                break;
            default:
                break;
        }
        if (!isPermission)
            throw new ForbiddenException("You don't have enough rights");
    }
}
