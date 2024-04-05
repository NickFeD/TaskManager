using Microsoft.AspNetCore.Mvc;
using TaskManager.Core.Contracts.Services;
using TaskManager.Core.Entities;
using TaskManager.Core.Exceptions;
using TaskManager.Infrastructure.Persistence;

namespace TaskManager.Api.Controllers.Abstracted;

public class BaseController : ControllerBase
{
    private User? _authUser;
    public User AuthUser
    {
        get
        {
            if (_authUser is not null)
                return _authUser;

            _authUser = HttpContext.Items["user"] as User 
                ?? throw new UnauthorizedException("Сould not identify the user");
            _authUser.LastLoginData = DateTime.UtcNow;
            return _authUser;
        }
    }
}
