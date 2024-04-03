using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Core.Entities;

namespace TaskManager.Infrastructure.Persistence.Configurations;

internal class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(e => e.Creator);
        builder.HasMany(e => e.Participants).WithOne(e => e.Project);
        builder.HasMany(e => e.Roles).WithOne(e => e.Project);
        builder.HasMany(e => e.Boards).WithOne(e => e.Project);
    }
}