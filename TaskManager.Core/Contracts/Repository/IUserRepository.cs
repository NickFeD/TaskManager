using TaskManager.Core.Entities;

namespace TaskManager.Core.Contracts.Repository;

public interface IUserRepository : IRepository<User, Guid>
{
    Task<User> GetUserByEmail(string email);
}