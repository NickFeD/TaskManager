using Microsoft.EntityFrameworkCore;
using TaskManager.Api.Data;
using TaskManager.Api.Entity;
using TaskManager.Api.Services.Abstracted;
using TaskManager.Command.Models;
using Task = System.Threading.Tasks.Task;

namespace TaskManager.Api.Services
{
    public class ParticipantService : ICRUDService<ProjectParticipantModel>, ICRUDServiceAsync<ProjectParticipantModel>
    {
        private readonly ApplicationContext _context;

        public ParticipantService(ApplicationContext context) { _context = context; }

        public Response<ProjectParticipantModel> Create(ProjectParticipantModel model)
        {
            var participant = new ProjectParticipant()
            {
                ProjectId = model.Id,
                UserId = model.UserId,
                UserRoleId = model.UserRoleId,
            };
            _context.Participants.Add(participant);
            _context.SaveChanges();
            model.Id = participant.Id;
            return new() { IsSuccess = true, Model = model };
        }

        public Task<Response<ProjectParticipantModel>> CreateAsync(ProjectParticipantModel model)
            => Task.FromResult(Create(model));

        public Response Delete(int id)
        {
            var participantToDelete = _context.Participants.Find(id);
            if (participantToDelete is null)
                return new() { IsSuccess = false, Reason = "Participants not found" };
            _context.Participants.Remove(participantToDelete);
            _context.SaveChanges();
            return new() { IsSuccess = true };
        }

        public Task<Response> DeleteAsync(int id)
            => Task.FromResult(Delete(id));

        public Response<List<ProjectParticipantModel>> GetAll()
        {
            var projectParticipants = _context.Participants.AsNoTracking().Select(p => p.ToDto()).ToList();
            if (projectParticipants is null)
                return new() { IsSuccess = false, Reason = "No participants" };
            return new() { IsSuccess = true, Model = projectParticipants };
        }

        public Task<Response<List<ProjectParticipantModel>>> GetAllAsync()
            => Task.FromResult(GetAll());

        public Response<ProjectParticipantModel> GetById(int id)
        {
            var participant = _context.Participants.AsNoTracking().FirstOrDefault(p => p.Id.Equals(id));
            if (participant is null)
                return new() { IsSuccess = false, Reason = "No participant" };
            return new() { IsSuccess = true, Model = participant.ToDto() };
        }

        public Task<Response<ProjectParticipantModel>> GetByIdAsync(int id)
            => Task.FromResult(GetById(id));

        public Response Update(ProjectParticipantModel model)
        {
            var participantToUpdate = _context.Participants.Find(model.Id);
            if (participantToUpdate is null)
                return new() { IsSuccess = false, Reason = "No participant" };
            participantToUpdate.ProjectId = model.ProjectId;
            participantToUpdate.UserId = model.UserId;
            participantToUpdate.UserRoleId = model.UserRoleId;
            _context.SaveChanges();
            return new() { IsSuccess = true };
        }

        public Task<Response> UpdateAsync(ProjectParticipantModel model)
            => Task.FromResult(Update(model));
    }
}
