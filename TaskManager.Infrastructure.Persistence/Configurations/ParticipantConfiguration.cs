using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Core.Entities;

namespace TaskManager.Infrastructure.Persistence.Configurations;

internal class ParticipantConfiguration : IEntityTypeConfiguration<ProjectParticipant>
{
    public void Configure(EntityTypeBuilder<ProjectParticipant> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(e => e.User).WithMany(e => e.Participants);
        builder.HasOne(e => e.Project).WithMany(e => e.Participants);
        builder.HasOne(e => e.Role).WithMany(e => e.Participants);
    }
}