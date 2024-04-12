using Mapster;
using Microsoft.EntityFrameworkCore;
using TaskManager.Core.Contracts.Services;
using TaskManager.Core.Entities;
using TaskManager.Core.Exceptions;
using TaskManager.Core.Models;
using TaskManager.Core.Models.Project;
using TaskManager.Core.Models.User;
using TaskManager.Infrastructure.Persistence;

namespace TaskManager.Infrastructure.Services
{
    public class UserService(TaskManagerDbContext context, IEncryptService encryptService) : IUserService
    {
        private readonly TaskManagerDbContext _context = context;
        private readonly IEncryptService _encryptService = encryptService;

        public Task<List<UserModel>> GetAllAsync()
        {
            return _context.Users.ProjectToType<UserModel>().ToListAsync();
        }

        public async Task<UserModel> GetByIdAsync(Guid id)
        {
            return await _context.Users.AsNoTracking().ProjectToType<UserModel>().FirstOrDefaultAsync(u => u.Id == id)
                ?? throw new BadRequestException("Invalid user uuid");
        }

        public async Task<UserModel> Registration(RegistrationModel registration)
        {
            if (await _context.Users.AnyAsync(u => u.Email == registration.Email))
                throw new BadRequestException($"By e-mail {registration.Email} the user is already registered");

            var salt = _encryptService.GenerateSalt();
            var user = new User()
            {
                Id = Guid.NewGuid(),
                Email = registration.Email,
                Phone = registration.Phone,
                RegistrationDate = DateTime.UtcNow,
                LastLoginData = DateTime.UtcNow,
                Username = registration.Username,
                FirstName = registration.FirstName,
                LastName = registration.LastName,
                Salt = salt,
                Password = _encryptService.HashPassword(registration.Password, salt),
            };
            await _context.Users.AddAsync(user);
            return user.Adapt<UserModel>();
        }

        public async Task UpdateAsync(Guid id, UserUpdateModel model)
        {
            var count = await _context.Users.Where(u => u.Id == id)
                .ExecuteUpdateAsync(setters => setters
                .SetProperty(p => p.FirstName, model.FirstName)
                .SetProperty(p => p.LastName, model.LastName)
                .SetProperty(p => p.Email, model.Email)
                .SetProperty(p => p.Phone, model.Phone));

            if (count < 1)
                throw new NotFoundException("Invalid project uuid");
        }

        public Task DeleteAsync(Guid id)
            => _context.Users.Where(u => u.Id == id).ExecuteDeleteAsync();

        public Task<List<ProjectModel>> GetProjectsByUserIdAsync(Guid userId)
        {
            return _context.Participant
                .AsNoTracking()
                .Where(p => p.UserId.Equals(userId))
                .Include(p => p.Project)
                .Select(p => p.Project)
                .ProjectToType<ProjectModel>()
                .ToListAsync();
        }

        public IAsyncEnumerable<UserRoleModel> GetByProjectId(Guid projectId)
        {
            var userRoles = _context.Participant
                .Where(p => p.ProjectId == projectId)
                .Include(p => p.User)
                .Include(p => p.Role)
                .ProjectToType<UserRoleModel>()
                .AsAsyncEnumerable();
            return userRoles;
        }

    }
}
