using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Linq;
using TaskManager.Command.Models;

namespace TaskManager.Api.Models.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<ProjectAdmin> ProjectAdmins { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Desk> Desks { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
            if (Users.Any(u => u.Status != UserStatus.Admin))
            {
                User admin = new()
                {
                    FirstName = "admin",
                    LastName = "admin",
                    Email = "admin",
                    Password = "admin",
                    Phone = "dwda",
                    RegistrationData = DateTime.Now,
                    LastLoginData = DateTime.Now,
                    Photo = null,
                    Status = UserStatus.Admin,
                };
                Users.Add(admin);
                SaveChanges();
            }
        }
    }
}
