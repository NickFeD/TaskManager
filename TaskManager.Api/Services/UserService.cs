using Microsoft.EntityFrameworkCore;
using TaskManager.Api.Data;
using TaskManager.Api.Entity;
using TaskManager.Api.Exceptions;
using TaskManager.Api.Services.Abstracted;
using TaskManager.Command.Models;
using Task = System.Threading.Tasks.Task;

namespace TaskManager.Api.Services
{
    public class UserService(ApplicationContext context)
    {
        private readonly ApplicationContext _context = context;

        public async Task<List<UserModel>> GetAllAsync()
        {
            var users = await _context.Users.AsNoTracking().Select(d => d.ToDto()).ToListAsync();

            if (users.Count < 1)
                throw new NotFoundException("Not found desks");

            return users;
        }

        public async Task<UserModel> GetByIdAsync(int id)
        {
            var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id.Equals(id));

            if (user is null)
                throw new NotFoundException("Not found user");

            return user.ToDto();
        }

        public async Task<UserModel> Registration(User user)
        {
            if (await _context.Users.AnyAsync(u => u.Email == user.Email))
                throw new BadRequestException($"By e-mail {user.Email} the user is already registered");
            _context.Users.Add(user);
            user.Id = await _context.SaveChangesAsync();
            return user.ToDto();
        }

        public async Task UpdateAsync(UserModel model)
        {
            var countUpdate = await _context.Users.Where(u => u.Id == model.Id)
                .ExecuteUpdateAsync(setter => setter
                .SetProperty(o => o.Email, model.Email)
                .SetProperty(o => o.FirstName, model.FirstName)
                .SetProperty(o => o.LastName, model.LastName)
                .SetProperty(o => o.Phone, model.Phone));

            if (countUpdate < 1)
                throw new NotFoundException("Not found user");
        }
        public async Task DeleteAsync(int id)
        {
            var countDelete = await _context.Users.Where(d => d.Id == id).ExecuteDeleteAsync();

            if (countDelete < 1)
                throw new NotFoundException("Not found user");
        }

        public  async Task<List<ProjectModel>> GetProjectsByUserIdAsync(int userId)
        {
            return await _context
                .Participants.Where(p => p.UserId == userId)
                .AsNoTracking()
                .Include(p => p.Project).Select(p => p.Project.ToDto()).ToListAsync();
        }

    }
}
