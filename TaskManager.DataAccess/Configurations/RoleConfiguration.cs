using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.DataAccess.Models;

namespace TaskManager.DataAccess.Configurations
{
    internal class RoleConfiguration : IEntityTypeConfiguration<RoleEntity>
    {
        public void Configure(EntityTypeBuilder<RoleEntity> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(e => e.Project).WithMany(e => e.Roles);
            builder.HasMany(e => e.Participants).WithOne(e => e.Role);
        }
    }
}