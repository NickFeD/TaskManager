using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Core.Enums;

namespace TaskManager.Core.Contracts.Services
{
    public interface IPermissionService
    {
        Task Project(Guid userId, Guid projectId, AllowedProject allowedProject);
    }
}
