using Microsoft.EntityFrameworkCore;
using TaskManager.Api.Entity;

namespace TaskManager.Api.Data
{
    public class ApplicationContext: DbContext
    {
        public ApplicationContext( DbContextOptions<ApplicationContext> options): base(options) 
        {
            Database.EnsureCreated();
        }

        public DbSet<User> Users =>Set<User>();
        public DbSet<UserRole> Roles =>Set<UserRole>();
        public DbSet<Project> Projects =>Set<Project>();
        public DbSet<ProjectParticipant> Participants =>Set<ProjectParticipant>();

    }
}
