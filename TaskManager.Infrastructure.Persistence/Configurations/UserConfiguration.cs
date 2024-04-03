using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Core.Entities;

namespace TaskManager.Infrastructure.Persistence.Configurations;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(e => e.Username).IsUnique();

        builder.HasMany(e => e.Participants).WithOne(e => e.User);
        builder.HasMany(e => e.RefreshTokens).WithOne(e => e.User);
    }
}
