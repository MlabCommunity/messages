using Lapka.Messages.Core;
using Lapka.Messages.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lapka.Messages.Infrastructure.Database.Config;

internal sealed class RoomConfiguration : IEntityTypeConfiguration<Room>
{
    public void Configure(EntityTypeBuilder<Room> builder)
    {
        builder.HasKey(x => x.RoomId);

        builder.HasMany(x => x.Messages).WithOne(x => x.Room);

        builder.ToTable("Rooms");
    }
}