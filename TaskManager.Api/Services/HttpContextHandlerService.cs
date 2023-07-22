﻿using System.Security.Claims;
using TaskManager.Api.Data;
using TaskManager.Api.Entity;

namespace TaskManager.Api.Services
{
    public class HttpContextHandlerService
    {
        private readonly ApplicationContext _context;

        public HttpContextHandlerService(ApplicationContext context)
        {
            _context = context;
        }

        public User? GetUser(HttpContext httpContext)
        {
            var identity = httpContext.User.Identity as ClaimsIdentity;
            if (identity is null)
                return null;
            var emailUser = identity.FindFirst(ClaimTypes.Email)?.Value;
            if (emailUser is null)
                return null;
            return _context.Users.FirstOrDefault(u => u.Email.Equals(emailUser));
        }

        public Task<User?> GetUserAsync(HttpContext httpContext)
            => System.Threading.Tasks.Task.FromResult(GetUser(httpContext));
    }
}