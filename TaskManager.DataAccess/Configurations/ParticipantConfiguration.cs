using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.DataAccess.Models;

namespace TaskManager.DataAccess.Configurations
{
    internal class ParticipantConfiguration : IEntityTypeConfiguration<ProjectParticipantEntity>
    {
        public void Configure(EntityTypeBuilder<ProjectParticipantEntity> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(e => e.User).WithMany(e => e.Participants);
            builder.HasOne(e => e.Project).WithMany(e => e.Participants);
            builder.HasOne(e => e.Role).WithMany(e => e.Participants);
        }
    }
}