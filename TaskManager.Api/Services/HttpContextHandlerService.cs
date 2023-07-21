using System.Security.Claims;
using TaskManager.Api.Data;
using TaskManager.Api.Entity;

namespace TaskManager.Api.Services
{
    public class HttpContextHandlerService
    {
        private readonly ApplicationContext _context;
        private readonly HttpContext _httpContext;

        public HttpContextHandlerService(ApplicationContext context, HttpContext httpContext)
        {
            _context = context;
            _httpContext = httpContext;
        }

        public User? GetUser()
        {
            var identity = _httpContext.User.Identity as ClaimsIdentity;
            if (identity is null)
                return null;
            var emailUser = identity.FindFirst(ClaimTypes.Email)?.Value;
            if (emailUser is null)
                return null;
            return _context.Users.FirstOrDefault(u => u.Email.Equals(emailUser));
        }
    }
}