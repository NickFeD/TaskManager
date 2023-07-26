using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TaskManager.Api.Entity;
using TaskManager.Command.Models;

namespace TaskManager.Api.Data
{
    public class Initialization
    {
        private readonly ApplicationContext _context;

        public Initialization(ApplicationContext context)
        {
            _context = context;
        }
        public void InitializationDb(int num)
        {
            _context.Users.AddRange(InitializationUser(num));
            _context.SaveChanges();
            _context.Projects.AddRange(InitializationProject(num));
            _context.SaveChanges();
            _context.Desks.AddRange(InitializationDesk(Faker.RandomNumber.Next(num, num * num)));
            _context.SaveChanges();
            _context.Tasks.AddRange(InitializationTask(Faker.RandomNumber.Next(num, num*num)));
            _context.SaveChanges();
            _context.Roles.AddRange(InitializationRoles(Faker.RandomNumber.Next(num, num * num)));
            _context.SaveChanges();
            _context.Participants.AddRange(InitializationParticipants(Faker.RandomNumber.Next( num,num * num)));
            _context.SaveChanges();
        }
        public List<User> InitializationUser(int numUsers)
        {
            List<User> users = new();
            for (int i = 0; i < numUsers; i++)
            {
                var user = new User()
                {
                    FirstName = Faker.Name.First(),
                    LastName = Faker.Name.Last(),
                    Email = Faker.Internet.Email(),
                    Phone = Faker.Phone.Number(),
                    Password = Faker.RandomNumber.Next(999999, 10000000).ToString(),
                    LastLoginData = DateTime.Now,
                };
                users.Add(user);
            }
            return users;
        }

        public List<Project> InitializationProject(int numProject)
        {
            List<Project> projects = new();
            List<User> users = _context.Users.ToList();
            for (int i = 0; i < numProject; i++)
            {

                Project project = new Project()
                {
                    Name = Faker.Company.Name(),
                    Description = Faker.Lorem.Sentence(),
                    Creator = users[Faker.RandomNumber.Next(users.Count - 1)],
                    Status = Faker.Enum.Random<ProjectStatus>(),
                };
                projects.Add(project);
            }
            return projects;
        }

        public List<Desk> InitializationDesk(int numDesk)
        {
            List<Desk> desks = new();
            List<Project> projects = _context.Projects.ToList();
            for (int i = 0; i < numDesk; i++)
            {
                var desk = new Desk()
                {
                    CreationData = DateTime.Now,
                    Description = Faker.Lorem.Sentence(),
                    Name = Faker.Company.Name(),
                    Project = projects[Faker.RandomNumber.Next(projects.Count - 1)],
                };
                desks.Add(desk);
            }
            return desks;
        }

        public List<Entity.Task> InitializationTask(int numTask)
        {
            List<Entity.Task> tasks = new();
            List<User> users = _context.Users.ToList();
            List<Desk> desks = _context.Desks.ToList();
            for (int i = 0; i < numTask; i++)
            {
                var task = new Entity.Task()
                {
                    CreationData = DateTime.Now,
                    Description = Faker.Lorem.Sentence(),
                    Name = Faker.Company.Name(),
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(1),
                    Сreator = users[Faker.RandomNumber.Next(users.Count - 1)],
                    Desk = desks[Faker.RandomNumber.Next(desks.Count - 1)],
                };
                tasks.Add(task);
            }
            return tasks;
        }

        public List<ProjectParticipant> InitializationParticipants(int numParticipants)
        {
            List<ProjectParticipant> participants = new ();
            List<Project> projects = _context.Projects.Include(p=> p.Roles).ToList();
            List<User > users = _context.Users.ToList();

            for (int i = 0; i < numParticipants; i++)
            {
                var project = projects[Faker.RandomNumber.Next(projects.Count - 1)];
                var usersTemp = users[Faker.RandomNumber.Next(users.Count - 1)];
                Role role = null;
                if (project.Roles.Count !=0)
                    role = project.Roles[Faker.RandomNumber.Next(project.Roles.Count - 1)];
                ProjectParticipant participant = new()
                {
                    Project = project,
                    User = usersTemp,
                    Role = role,
                };
                participants.Add(participant);
            }
            return participants;
        }

        public List<Role> InitializationRoles(int numRoles)
        {
            List<Project> projects = _context.Projects.ToList();
            List<Role> roles = new List<Role>();

            for (int i = 0; i < numRoles; i++)
            {
                Role role = new()
                {
                    Name = Faker.Company.Suffix(),
                    Project = projects[Faker.RandomNumber.Next(projects.Count - 1)],
                };
                roles.Add(role);
            }
            return roles;
        }


    }
}
