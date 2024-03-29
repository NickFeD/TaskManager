using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TaskManager.Api.Data;
using TaskManager.Api.Entity;
using TaskManager.Api.Exceptions;
using Task = System.Threading.Tasks.Task;

namespace TaskManager.Api.Services
{
    public class HttpContextHandlerService
    {
        private readonly ApplicationContext _context;

        public HttpContextHandlerService(ApplicationContext context)
        {
            _context = context;
        }

        public User GetUserAsNoTracking(HttpContext httpContext)
        {
            var emailUser = GetEmail(httpContext);
            if (emailUser is null)
                throw new UnauthorizedException();
            return _context.Users.AsNoTracking().FirstOrDefault(u => u.Email.Equals(emailUser))?? throw new UnauthorizedException();
        }
        
        public Task<User> GetUserAsNoTrackingAsync(HttpContext httpContext)
            => Task.FromResult(GetUserAsNoTracking(httpContext));

        public User GetUser(HttpContext httpContext)
        {
            var emailUser = GetEmail(httpContext);
            if (emailUser is null)
                throw new UnauthorizedException();
            return _context.Users.FirstOrDefault(u => u.Email.Equals(emailUser)) ?? throw new UnauthorizedException();
        }

        public Task<User> GetUserAsync(HttpContext httpContext)
            => System.Threading.Tasks.Task.FromResult(GetUser(httpContext));

        public Role? GetUserRole(HttpContext httpContext, int projectId)
        {
            var emailUser = GetEmail(httpContext);
            if (emailUser is null)
                return null;
            return _context.Participants.Include(p => p.Role).Include(p=>p.User).FirstOrDefault(p => p.ProjectId.Equals(projectId)&& p.User.Email.Equals(emailUser))?.Role;
        }
        public Role? GetUserRoleAsNoTracking(HttpContext httpContext, int projectId)
        {
            var emailUser = GetEmail(httpContext);
            if (emailUser is null)
                return null;
            return _context.Participants.AsNoTracking().Include(p => p.Role).Include(p => p.User).FirstOrDefault(p => p.ProjectId.Equals(projectId) && p.User.Email.Equals(emailUser))?.Role;
        }


        public bool? IsParticipant(HttpContext httpContext, int projectId)
        {
            var emailUser = GetEmail(httpContext);
            if (emailUser is null)
                return null;
            return _context.Participants.Include(p=> p.User).Any(p=>p.ProjectId.Equals(projectId)&& p.User.Email.Equals(emailUser));
        }
        private string? GetEmail(HttpContext httpContext)
        {
            var identity = httpContext.User.Identity as ClaimsIdentity;
            if (identity is null)
                return null;
            return identity.FindFirst(ClaimTypes.Email)?.Value;
        }
    }
}