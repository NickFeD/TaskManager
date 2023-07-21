using Microsoft.EntityFrameworkCore;
using TaskManager.Api.Data;
using TaskManager.Api.Entity;
using TaskManager.Api.Services.Abstracted;
using TaskManager.Command.Models;
using Task = System.Threading.Tasks.Task;

namespace TaskManager.Api.Services
{
    public class UserService
    {
        private readonly ApplicationContext _context;
        public UserService(ApplicationContext context) { _context = context; }

        public Response<UserModel> Create(UserModel model)
        {
            var user = _context.Users.AsNoTracking().FirstOrDefault(u => u.Email.Equals(model.Email));
            if (user is not null)
                return new() { IsSuccess = false, Reason = "At this moment, the email is busy" };
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
            return new() { IsSuccess = true, Model = model };
        }

        public Response Delete(User user)
        {
            if (user is null)
                return new() { IsSuccess = false, Reason = "User not found" };
            var projects = _context.Projects.Where(p => p.CreatorId == user.Id).ToList();
            _context.Users.Remove(user);
            _context.SaveChanges();
            return new() { IsSuccess = true };

        }

        public Response<List<UserModel>> GetAll()
        {
            var users = _context.Users.ToList().Select(u => u.ToDto()).ToList();
            if (users is null)
                return new() { IsSuccess = false, Reason = "No users" };
            return new() { IsSuccess = true, Model = users };
        }

        public Response<UserModel> GetById(int id)
        {
            var user = _context.Users.AsNoTracking().FirstOrDefault(u => u.Id == id);

            if (user is null)
                return new() { IsSuccess = false, Reason = "No user" };
            return new() { IsSuccess = true, Model = user.ToDto() };
        }

        public Response Update(UserModel model)
        {
            var userToUpdate = _context.Users.Find(model.Id);
            if (userToUpdate is null)
                return new() { IsSuccess = false, Reason = "There is no user" };
            //userToUpdate.Email = model.Email;
            userToUpdate.FirstName = model.FirstName;
            userToUpdate.LastName = model.LastName;
            userToUpdate.LastLoginData = model.LastLoginData;
            userToUpdate.Password = model.Password;
            userToUpdate.Phone = model.Phone;
            _context.Users.Update(userToUpdate);
            _context.SaveChanges();
            return new() { IsSuccess = true };
        }


        public  Task<Response<List<Project>>> GetProjectsByUserIdAsync(int userId)
        {
            var projectParticipants = _context
                .Users
                .AsNoTracking()
                .Include(u => u.Participants)
                .ThenInclude(p => p.Project)
                .FirstOrDefault(u => u.Id == userId)
                .Participants
                .Distinct()
                .ToList();
            if (projectParticipants.Select(p => p.Project) is null)
                return Task.FromResult<Response<List<Project>>>( new() { IsSuccess = false, Reason = "Not found project" });

            var response = new Response<List<Project>>()
            {
                IsSuccess = true,
                Model = projectParticipants.Select(p => p.Project).ToList(),
            };
            return Task.FromResult(response);
        }

        public Task<Response<List<UserModel>>> GetAllAsync()
            =>Task.FromResult(GetAll());

        public Task<Response<UserModel>> GetByIdAsync(int id)
            => Task.FromResult(GetById(id));

        public Task<Response<UserModel>> CreateAsync(UserModel model)
            => Task.FromResult(Create(model));

        public Task<Response> UpdateAsync(UserModel model)
            => Task.FromResult(Update(model));

        public Task<Response> DeleteAsync(User user)
            => Task.FromResult(Delete(user));
    }
}
