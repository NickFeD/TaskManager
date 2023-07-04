using Microsoft.EntityFrameworkCore;
using TaskManager.Api.Data;
using TaskManager.Api.Entity;
using TaskManager.Api.Controllers.Abstracted;
using TaskManager.Command.Models;
using TaskManager.Api.Services.Abstracted;

namespace TaskManager.Api.Services
{
    public class UserService: CRUDService<UserModel,User>
    {
        public UserService( ApplicationContext context) : base(context) { }
        
        public override List<UserModel> GetAll()
        {
            return _context.Users.AsNoTracking().Select(u => (UserModel)u).ToList();
        }

        public override void Delete(int id)
        {
            var entity = _context.Users.Find(id);
            if (entity is null)
                return;
            var projects = _context.Projects.Where(p=> p.CreatorId == id).ToList();
            _context.Users.Remove(entity);
            _context.SaveChanges();
        }

        public IEnumerable<Project> GetProjectsByUserId(int userId)
        {
            var projectParticipants = _context?
                .Users?
                .AsNoTracking()?
                .Include(u => u.Participants)?
                .ThenInclude(p => p.Project)?
                .FirstOrDefault(u => u.Id == userId)?
                .Participants?
                .Distinct()
                .ToArray();
            return projectParticipants.Select(p => p.Project).ToList();
        }

    }
}
