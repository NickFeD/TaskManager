using Microsoft.EntityFrameworkCore;
using TaskManager.Api.Data;
using TaskManager.Api.Entity;
using TaskManager.Command.Models;

namespace TaskManager.Api.Services
{
    public class ParticipantService : ICRUDService<ProjectParticipantModel>
    {
        private readonly ApplicationContext _context;

        public ParticipantService(ApplicationContext context)
        {
            _context = context;
        }

        public ProjectParticipantModel? GetById(int id)
        {
            var participant = _context.Participants.Find(id);
            return participant is null? null: (ProjectParticipantModel)participant;
        }

        public ProjectParticipantModel Create(ProjectParticipantModel model)
        {
            _context.Participants.Add(model);
            _context.SaveChanges();
            return model;
        }

        public void Update(ProjectParticipantModel model)
        {
            _context.Participants.Update(model);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var participant = _context.Participants.SingleOrDefault(p => p.Id == id);
            if (participant is null)
                return;
            _context.Participants.Remove(participant);
        }

    }
}
