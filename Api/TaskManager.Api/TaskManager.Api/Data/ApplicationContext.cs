using Microsoft.EntityFrameworkCore;
using TaskManager.Api.Entity;

namespace TaskManager.Api.Data
{
    public class ApplicationContext: DbContext
    {
        public ApplicationContext( DbContextOptions<ApplicationContext> options): base(options) 
        {
            Database.Migrate();
        }

        public DbSet<User> Users =>Set<User>();
        public DbSet<UserRefreshToken> UserRefreshTokens =>Set<UserRefreshToken>();
        public DbSet<Role> Roles =>Set<Role>();
        public DbSet<Project> Projects =>Set<Project>();
        public DbSet<Desk> Desks =>Set<Desk>();
        public DbSet<Entity.Task> Tasks =>Set<Entity.Task>();
        public DbSet<ProjectParticipant> Participants =>Set<ProjectParticipant>();

    }
}
