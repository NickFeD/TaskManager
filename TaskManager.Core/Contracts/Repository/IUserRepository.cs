using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Core.Entities;

namespace TaskManager.Core.Contracts.Repository;

public interface IUserRepository:IRepository<User,Guid>
{
    Task<User> GetUserByEmail(string email);
}