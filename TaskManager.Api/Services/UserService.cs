using Microsoft.EntityFrameworkCore;
using TaskManager.Api.Data;
using TaskManager.Api.Models;
using TaskManager.Api.Models.Abstracted;
using TaskManager.Command.Models;

namespace TaskManager.Api.Services
{
    public class UserService
    {
        private readonly ApplicationContext _context;
        public UserService( ApplicationContext context)
        {
            _context = context;
        }

        public IEnumerable<User> GetAll() 
        {
           return _context.Users.AsNoTracking().ToList();
        }

        public User GetById(int id) 
        {
            return _context.Users.Find(id);
        }
        public void Update(User model)
        {
            _context.Users.Update(model);
            _context.SaveChanges();
        }

        public User Create(User model)
        {
            _context.Users.Add(model);
            _context.SaveChanges();
            return model;
        }

        public void Delete(int id)
        {
            var userToDelete = _context.Users.Find(id);
            if (userToDelete is null)
                return;
            _context.Users.Remove(userToDelete);
            _context.SaveChanges();
        }

        
    }
}
