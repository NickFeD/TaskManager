using AutoMapper;
using TaskManager.Core.Contracts.Repository;
using TaskManager.Core.Contracts.Services;
using TaskManager.Core.Entities;
using TaskManager.Core.Exceptions;
using TaskManager.Core.Extentions;
using TaskManager.Core.Models;

namespace TaskManager.Infrastructure.Services
{
    public class ParticipantService(IParticipantRepository participantRepository, IMapper mapper, IUserRepository userRepository, IRoleRepository roleRepository, IProjectRepository projectRepository) : IParticipantService
    {
        private readonly IParticipantRepository _participantRepository = participantRepository;
        private readonly IProjectRepository _projectRepository = projectRepository;
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IRoleRepository _roleRepository = roleRepository;
        private readonly IMapper _mapper = mapper;
        public async Task<ParticipantModel> CreateAsync(ParticipantCreateModel model)
        {
            var task = ValidateDetails(model.ProjectId, model.UserId, model.RoleId);

            var participant = _mapper.Map<ProjectParticipant>(model);
            participant.Id = Guid.NewGuid();

            await task;
            participant = await _participantRepository.AddAsync(participant);
            return _mapper.Map<ParticipantModel>(participant);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _participantRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<ParticipantModel>> GetAllAsync()
        {

            var participant = (await _participantRepository.GetAllAsync()).Select(p => p.ToModel());
            return participant.ToList();
        }

        public async Task<ParticipantModel> GetByIdAsync(Guid id)
        {
            var participant = await _participantRepository.GetByIdAsync(id);

            return _mapper.Map<ParticipantModel>(participant);
        }

        public async Task UpdateAsync(Guid id, ParticipantUpdateModel model)
        {
            await ValidateDetails(model.ProjectId, model.UserId, model.RoleId);
            var participant = _mapper.Map<ProjectParticipant>(model);


            await _participantRepository.UpdateAsync(participant);
        }

        private async Task ValidateDetails(Guid projectId, Guid userId, Guid roleId)
        {

            var roleContaind = _roleRepository.ContainsdAsync(roleId);
            var userContaind = _userRepository.ContainsdAsync(userId);
            var projectContaind = _projectRepository.ContainsdAsync(projectId);
            if (await roleContaind)
                throw new BadRequestException("Invalid role uuid");
            if (await userContaind)
                throw new BadRequestException("Invalid user uuid");
            if (await projectContaind)
                throw new BadRequestException("Invalid project uuid");
        }
    }
}
