using Microsoft.EntityFrameworkCore;
using TaskManager.Core.Contracts.Repository;
using TaskManager.Core.Entities;

namespace TaskManager.Infrastructure.Persistence.Repository;

public class UserRepository(TaskManagerDbContext context) : BaseRepository<User, Guid>(context), IUserRepository
{
    public Task<User?> GetUserByEmail(string email)
        => _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Email == email);

}