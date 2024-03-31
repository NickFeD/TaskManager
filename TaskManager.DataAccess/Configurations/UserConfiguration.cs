using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.DataAccess.Models;

namespace TaskManager.DataAccess.Configurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(e => e.Username).IsUnique();

            builder.HasMany(e => e.Participants).WithOne(e => e.User);
            builder.HasMany(e => e.RefreshTokens).WithOne(e => e.User);
        }
    }
}
