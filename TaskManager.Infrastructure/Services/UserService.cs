using TaskManager.Core.Contracts.Repository;
using TaskManager.Core.Contracts.Services;
using TaskManager.Core.Entities;
using TaskManager.Core.Exceptions;
using TaskManager.Core.Extentions;
using TaskManager.Core.Models;

namespace TaskManager.Infrastructure.Services
{
    public class UserService(IUserRepository userRepository, IParticipantRepository participantRepository) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IParticipantRepository _participantRepository = participantRepository;

        public async Task<List<UserModel>> GetAllAsync()
        {
            return (await _userRepository.GetAllAsync()).Select(u => u.ToModel()).ToList();
        }

        public async Task<UserModel> GetByIdAsync(Guid id)
        {
            return (await _userRepository.GetByIdAsync(id)).ToModel();
        }

        public async Task<UserModel> Registration(User user)
        {
            if (await _userRepository.ContainsdByConditionAsync(u => u.Email == user.Email))
                throw new BadRequestException($"By e-mail {user.Email} the user is already registered");
            
            return (await _userRepository.AddAsync(user)).ToModel();
        }

        public async Task UpdateAsync(UserUpdateModel model)
        {
            var user = new User() 
            { 
                Id= model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Phone = model.Phone,
            };
            user = await _userRepository.UpdateAsync(user);
        }
        public async Task DeleteAsync(Guid id)
        {
            await _userRepository.DeleteAsync(id);
        }

        public  async Task<List<ProjectModel>> GetProjectsByUserIdAsync(Guid userId)
        {
            var projects = await _participantRepository.GetProjectsByConditionAsync(p => p.UserId.Equals( userId));
            return projects.Select(p=>p.ToModel()).ToList();
        }

    }
}
