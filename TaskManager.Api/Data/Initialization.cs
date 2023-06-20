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
            _context.Participants.AddRange(InitializationParticipants(Faker.RandomNumber.Next( num,num*50)));
            _context.SaveChanges();
            _context.Roles.AddRange(InitializationRoles(num));
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
            List<UserRole> roles = _context.Roles.ToList();
            List<ProjectParticipant> participants = _context.Participants.ToList();
            for (int i = 0; i < numProject; i++)
            {
                var values = new List<UserRole>();
                if (roles.Count !=0)
                    for (int j = 0; j < Faker.RandomNumber.Next(roles.Count - 1); j++)
                    {
                        values.Add(roles[Faker.RandomNumber.Next( roles.Count - 1)]);
                    }

                List<ProjectParticipant> values1 = new();
                if (participants.Count !=0)
                    for (int j = 0; j < Faker.RandomNumber.Next(0, participants.Count - 1); j++)
                    {
                        values1.Add(participants[Faker.RandomNumber.Next(participants.Count - 1)]);
                    }

                Project project = new Project()
                {
                    Name = Faker.Company.Name(),
                    Description = Faker.Lorem.Sentence(),
                    Creator = users[Faker.RandomNumber.Next(users.Count - 1)],
                    Status = Faker.Enum.Random<ProjectStatus>(),
                    UserRoles = values,
                    Participants = values1,
                };
                projects.Add(project);
            }
            return projects;
        }

        public List<ProjectParticipant> InitializationParticipants(int numParticipants)
        {
            List<ProjectParticipant> participants = new ();
            List<Project> projects = _context.Projects.ToList();
            List<User > users = _context.Users.ToList();

            for (int i = 0; i < numParticipants; i++)
            {
                ProjectParticipant participant = new()
                {
                    Project = projects[Faker.RandomNumber.Next(projects.Count-1)],
                    User = users[Faker.RandomNumber.Next(users.Count-1)]
                };
                participants.Add(participant);
            }
            return participants;
        }

        public List<UserRole> InitializationRoles(int numRoles)
        {
            List<Project> projects = _context.Projects.ToList();
            List<UserRole> roles = new List<UserRole>();

            for (int i = 0; i < numRoles; i++)
            {
                UserRole role = new()
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
