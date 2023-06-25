using Microsoft.EntityFrameworkCore;
using TaskManager.Api.Data;
using TaskManager.Api.Entity;
using TaskManager.Api.Services.Abstracted;
using TaskManager.Command.Models;

namespace TaskManager.Api.Services
{
    public class ParticipantService : CRUDService<ProjectParticipantModel,ProjectParticipant>
    {

        public ParticipantService(ApplicationContext context) : base(context) { }
        public override List<ProjectParticipantModel> GetAll()
        {
            return _context.Participants.Select(p=> (ProjectParticipantModel)p).ToList();
        }
    }
}
