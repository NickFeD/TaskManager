using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Core.Entities;
using TaskManager.Core.Models;

namespace TaskManager.Core.Contracts.Services
{
    public interface IUserService
    {
        Task DeleteAsync(Guid id);
        Task<List<UserModel>> GetAllAsync();
        Task<UserModel> GetByIdAsync(Guid id);
        Task<List<ProjectModel>> GetProjectsByUserIdAsync(Guid userId);
        Task<UserModel> Registration(User user);
        Task UpdateAsync(UserUpdateModel model);
    }
}
