using Microsoft.EntityFrameworkCore;
using TaskManager.Api.Data;
using TaskManager.Api.Entity;
using TaskManager.Api.Services.Abstracted;
using TaskManager.Command.Models;

namespace TaskManager.Api.Services
{
    public class UserService : ICRUDService<UserModel>
    {
        private readonly ApplicationContext _context;
        public UserService(ApplicationContext context) { _context = context; }

        public UserModel? Create(UserModel model)
        {
            var user = _context.Users.AsNoTracking().FirstOrDefault(u => u.Email.Equals(model.Email));
            if (user is not null)
                return null;
            user = new Entity.User()
            {
                Id = model.Id,
                FirstName = model.FirstName,
                Email = model.Email,
                Password = model.Password,
                LastName = model.LastName,
                Phone = model.Phone,
                LastLoginData = model.LastLoginData,
                RegistrationDate = DateTime.Now,
            };
            _context.Users.Add(user);
            _context.SaveChanges();
            model.Id = user.Id;
            return model;
        }

        public void Delete(int id)
        {
            var userToDelete = _context.Users.Find(id);
            if (userToDelete is null)
                return;
            var projects = _context.Projects.Where(p => p.CreatorId == id).ToList();
            _context.Users.Remove(userToDelete);
            _context.SaveChanges();
            return;

        }

        public List<UserModel> GetAll()
        {
            return _context.Users.ToList().Select(u => u.ToDto()).ToList();
        }

        public UserModel? GetById(int id)
        {
            var user = _context.Users.AsNoTracking().FirstOrDefault(u => u.Id == id);

            return user?.ToDto();
        }

        public void Update(UserModel model)
        {
            var user = new User()
            {
                Id = model.Id,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                LastLoginData = model.LastLoginData,
                Password = model.Password,
                Phone = model.Phone,
            };
            _context.Users.Update(user);
            _context.SaveChanges();
            return;
        }


        public IEnumerable<Project> GetProjectsByUserId(int userId)
        {
            var projectParticipants = _context
                .Users
                .AsNoTracking()
                .Include(u => u.Participants)
                .ThenInclude(p => p.Project)
                .FirstOrDefault(u => u.Id == userId)
                .Participants
                .Distinct()
                .ToArray();
            return projectParticipants.Select(p => p.Project).ToList();
        }

        internal User? GetByEmail(string emailUser)
        {
            return _context.Users.AsNoTracking().FirstOrDefault(u => u.Email.Equals(emailUser));
        }
    }
}
