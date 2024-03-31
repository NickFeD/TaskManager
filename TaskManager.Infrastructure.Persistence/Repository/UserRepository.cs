using Microsoft.EntityFrameworkCore;
using TaskManager.Core.Contracts.Repository;
using TaskManager.Core.Entities;
using TaskManager.Core.Exceptions;

namespace TaskManager.Infrastructure.Persistence.Repository;

public class UserRepository(TaskManagerDbContext context) : BaseRepository<User, Guid>(context), IUserRepository
{
    public async Task<User> GetUserByEmail(string email)
    {
        var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Email == email)
            ?? throw new NotFoundException("user not registration");
        return user;
    }
}