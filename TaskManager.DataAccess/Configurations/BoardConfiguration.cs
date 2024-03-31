using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using TaskManager.DataAccess.Models;

namespace TaskManager.DataAccess.Configurations;

internal class BoardConfiguration : IEntityTypeConfiguration<BoardEntity>
{
    public void Configure(EntityTypeBuilder<BoardEntity> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasMany(e => e.Tasks).WithOne(e => e.Board);
        builder.HasOne(e => e.Project).WithMany(e => e.Boards);
    }
}
