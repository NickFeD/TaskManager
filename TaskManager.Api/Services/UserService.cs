using Microsoft.EntityFrameworkCore;
using TaskManager.Api.Data;
using TaskManager.Api.Entity;
using TaskManager.Api.Controllers.Abstracted;
using TaskManager.Command.Models;

namespace TaskManager.Api.Services
{
    public class UserService: ICRUDService<UserModel>
    {
        private readonly ApplicationContext _context;
        public UserService( ApplicationContext context)
        {
            _context = context;
        }
        //CRUT
        public List<UserModel> GetAll()
        {
            return _context.Users.AsNoTracking().Select(u => (UserModel)u).ToList();
        }

        public UserModel? GetById(int id) 
        {
            var user = _context.Users.Find(id);
            if (user is null)
                return null;
            return user;
        }

        public void Update(UserModel model)
        {
            var user = _context.Find(typeof(User),model.Id);
            if (user is null) return;
            _context.Entry(user).CurrentValues.SetValues(model);
            _context.SaveChanges();
        }

        public UserModel Create(UserModel model)
        {
            _context.Users.Add(model);
            _context.SaveChanges();
            return model;
        }

        public void Delete(int id)
        {
            var userToDelete = _context.Users.Find(id);
            var project = _context.Projects.Where( p => p.Creator == userToDelete);

            if (userToDelete is null)
                return;
            _context.Users.Remove(userToDelete);
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
