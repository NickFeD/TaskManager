using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.DataAccess.Models;

namespace TaskManager.DataAccess.Configurations
{
    internal class ProjectConfiguration : IEntityTypeConfiguration<ProjectEntity>
    {
        public void Configure(EntityTypeBuilder<ProjectEntity> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(e => e.Creator);
            builder.HasMany(e => e.Participants).WithOne(e =>e.Project);
            builder.HasMany(e => e.Roles).WithOne(e =>e.Project);
            builder.HasMany(e => e.Boards).WithOne(e =>e.Project);
        }
    }
}