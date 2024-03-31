using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.DataAccess.Configurations;
using TaskManager.DataAccess.Models;

namespace TaskManager.DataAccess;

public class TaskManagerDbContext(DbContextOptions<TaskManagerDbContext> options) 
    : DbContext(options)
{
    public DbSet<UserRefreshTokenEntity> UserRefreshToken { get; set; }
    public DbSet<ProjectParticipantEntity> Participant { get; set; }
    public DbSet<ProjectEntity> Projects { get; set; }
    public DbSet<BoardEntity> Boards { get; set; }
    public DbSet<TaskEntity> Tasks { get; set; }
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<RoleEntity> Roles { get; set; }
}
