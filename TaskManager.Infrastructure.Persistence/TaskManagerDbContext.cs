using Microsoft.EntityFrameworkCore;
using TaskManager.Core.Entities;
using TaskManager.Infrastructure.Persistence.Configurations;

namespace TaskManager.Infrastructure.Persistence;

public class TaskManagerDbContext(DbContextOptions<TaskManagerDbContext> options)
    : DbContext(options)
{
    public DbSet<UserRefreshToken> UserRefreshToken { get; set; }
    public DbSet<ProjectParticipant> Participant { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Board> Boards { get; set; }
    public DbSet<TaskEntity> Tasks { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.ApplyConfiguration(new BoardConfiguration());
        modelBuilder.ApplyConfiguration(new ParticipantConfiguration());
        modelBuilder.ApplyConfiguration(new ProjectConfiguration());
        modelBuilder.ApplyConfiguration(new RoleConfiguration());
        modelBuilder.ApplyConfiguration(new TaskConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new RefreshTokenConfiguration());


        base.OnModelCreating(modelBuilder);
    }
}
