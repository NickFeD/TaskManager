using Microsoft.EntityFrameworkCore;
using TaskManager.Api.Data;
using TaskManager.Api.Entity;
using TaskManager.Api.Models.Abstracted;
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
        public UserModel? GetById(int id) 
        {
            var user = _context.Users.Find(id);
            if (user is null)
                return null;
            return user;
        }

        public void Update(UserModel model)
        {
            _context.Users.Update(model);
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




        public IEnumerable<User> GetAll()
        {
            return _context.Users.AsNoTracking().ToList();
        }

    }
}
