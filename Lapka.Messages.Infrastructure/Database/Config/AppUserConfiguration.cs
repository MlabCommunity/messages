using Lapka.Messages.Core;
using Lapka.Messages.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lapka.Messages.Infrastructure.Database.Config;

internal sealed class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.HasKey(x => x.UserId);
        builder.ToTable("AppUsers");
        builder.HasMany(x => x.Rooms).WithMany(x => x.AppUsers);
    }
}