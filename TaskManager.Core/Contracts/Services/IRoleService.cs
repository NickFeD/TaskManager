using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Core.Models;

namespace TaskManager.Core.Contracts.Services
{
    public interface IRoleService:ICRUDService<RoleModel,Guid>
    {
    }
}
