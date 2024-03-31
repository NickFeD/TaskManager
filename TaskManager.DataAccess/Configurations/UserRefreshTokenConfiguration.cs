using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.DataAccess.Models;

namespace TaskManager.DataAccess.Configurations;

internal class UserRefreshTokenConfiguration : IEntityTypeConfiguration<UserRefreshTokenEntity>
{
    public void Configure(EntityTypeBuilder<UserRefreshTokenEntity> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(e => e.User).WithMany(e => e.RefreshTokens);
    }
}
