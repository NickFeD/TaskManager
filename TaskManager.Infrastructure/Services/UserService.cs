using System.Collections.Frozen;
using TaskManager.Core.Contracts.Repository;
using TaskManager.Core.Contracts.Services;
using TaskManager.Core.Entities;
using TaskManager.Core.Exceptions;
using TaskManager.Core.Extentions;
using TaskManager.Core.Models;
using TaskManager.Core.Models.Project;
using TaskManager.Core.Models.User;

namespace TaskManager.Infrastructure.Services
{
    public class UserService(IUserRepository userRepository, IParticipantRepository participantRepository, IRoleRepository roleRepository, IEncryptService encryptService) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IEncryptService _encryptService = encryptService;
        private readonly IRoleRepository _roleRepository = roleRepository;
        private readonly IParticipantRepository _participantRepository = participantRepository;

        public async Task<List<UserModel>> GetAllAsync()
        {
            return (await _userRepository.GetAllAsync()).Select(u => u.ToModel()).ToList();
        }

        public async Task<UserModel> GetByIdAsync(Guid id)
        {
            return (await _userRepository.GetByIdAsync(id)).ToModel();
        }

        public async Task<UserModel> Registration(RegistrationModel registration)
        {
            if (await _userRepository.ContainsdByConditionAsync(u => u.Email == registration.Email))
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

            return (await _userRepository.AddAsync(user)).ToModel();
        }

        public async Task UpdateAsync(Guid id, UserUpdateModel model)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user is null)
                throw new BadRequestException("Invalid user uuid");
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.Phone = model.Phone;
            user = await _userRepository.UpdateAsync(user);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _userRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<ProjectModel>> GetProjectsByUserIdAsync(Guid userId)
        {
            var projects = await _participantRepository.GetProjectsByConditionAsync(p => p.UserId.Equals(userId));
            return projects.Select(p => p.ToModel());
        }

        public async Task<IEnumerable<UserRoleModel>> GetByProjectId(Guid projectId)
        {
            var participants = (await _participantRepository.GetByConditionAsync(p => p.ProjectId.Equals(projectId))).ToList();

            var usersId = participants.Select(p => p.UserId).ToHashSet();
            var rolesId = participants.Select(p => p.RoleId).ToHashSet();

            var users = (await _userRepository.GetByConditionAsync(u => usersId.Contains(u.Id))).ToFrozenDictionary(u => u.Id);
            var roles = (await _roleRepository.GetByConditionAsync(r => rolesId.Contains(r.Id))).ToFrozenDictionary(r => r.Id);

            List<UserRoleModel> userRoleModels = new();
            for (int i = 0; i < participants.Count(); i++)
            {
                userRoleModels.Add(new UserRoleModel()
                {
                    Role = roles[participants[i].RoleId!.Value].ToModel(),
                    User = users[participants[i].UserId].ToModel()
                });
            }

            return userRoleModels;
        }

    }
}
