using Microsoft.EntityFrameworkCore;
using TaskManager.Core.Contracts.Services;
using TaskManager.Core.Enums;
using TaskManager.Core.Exceptions;
using TaskManager.Infrastructure.Persistence;

namespace TaskManager.Infrastructure.Services;

public class PermissionService(TaskManagerDbContext context) : IPermissionService
{
    private readonly TaskManagerDbContext _context = context;

    public async Task Project(Guid userId, Guid projectId, AllowedProject allowedProject)
    {
        var projects = _context.Participant.Where(p => p.UserId == userId && p.ProjectId == projectId);

        if (projects is null)
        {
            throw new BadRequestException("Invalid project uuid");
        }

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
            case AllowedProject.DeleteUsers:
                isPermission = await projects.AnyAsync(p => p.Role!.ProjectDeleteUsers);
                break;
            default:
                break;
        }
        if (!isPermission)
            throw new ForbiddenException("You don't have enough rights");
    }

    public async Task Board(Guid userId, Guid boardId, AllowedBoard allowedBoard)
    {
        var projects = _context.Boards
            .Where(b => b.Id == boardId)
            .Select(p => p.Project)
            .Select(p =>p!.Participants.SingleOrDefault(p=>p.UserId == userId)) ?? throw new BadRequestException("Invalid project uuid");

        var isPermission = false;
        switch (allowedBoard)
        {
            case AllowedBoard.Add:
                isPermission = await projects.AnyAsync(p => p.Role!.BoardAdd);
                break;
            case AllowedBoard.Edit:
                isPermission = await projects.AnyAsync(p => p.Role!.BoardEdit);
                break;
            case AllowedBoard.Delete:
                isPermission = await projects.AnyAsync(p => p.Role!.BoardDelete);
                break;
            case AllowedBoard.AddTask:
                isPermission = await projects.AnyAsync(p => p.Role!.BoardAddTasks);
                break;
            default:
                break;
        }
        if (!isPermission)
            throw new ForbiddenException("You don't have enough rights");
    }

    public async Task Task(Guid userId, Guid taskId, AllowedTask allowedTask)
    {
        var projects = _context.Tasks
            .Where(b => b.Id == taskId)
            .Select(p => p.Board)
            .Select(p => p.Project)
            .Select(p => p.Participants.SingleOrDefault(p => p.UserId == userId))?? throw new BadRequestException("Invalid project uuid");
        var isPermission = false;
        switch (allowedTask)
        {
            case AllowedTask.Add:
                isPermission = await projects.AnyAsync(p => p.Role!.TaskAdd);
                break;
            case AllowedTask.Edit:
                isPermission = await projects.AnyAsync(p => p.Role!.TaskEdit);
                break;
            case AllowedTask.Delete:
                isPermission = await projects.AnyAsync(p => p.Role!.TaskDelete);
                break;
            default:
                break;
        }
        if (!isPermission)
            throw new ForbiddenException("You don't have enough rights");
    }

    public async Task Role(Guid userId, Guid projectId, AllowedRole allowedRole)
    {
        var projects = _context.Projects
            .Where(b => b.Id == projectId)
            .Select(p => p.Participants.SingleOrDefault(p => p.UserId == userId)) ?? throw new BadRequestException("Invalid project uuid");
        var isPermission = false;
        switch (allowedRole)
        {
            case AllowedRole.Add:
                isPermission = await projects.AnyAsync(p => p.Role!.RoleAdd);
                break;
            case AllowedRole.Edit:
                isPermission = await projects.AnyAsync(p => p.Role!.RoleEdit);
                break;
            case AllowedRole.Delete:
                isPermission = await projects.AnyAsync(p => p.Role!.RoleDelete);
                break;
            default:
                break;
        }
        if (!isPermission)
            throw new ForbiddenException("You don't have enough rights");
    }
}
