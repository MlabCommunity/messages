using Lapka.Messages.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lapka.Messages.Infrastructure.Database.Config;

internal sealed class AppUsersConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.HasKey(x => x.UserId);
        builder.ToTable("AppUsers");
        builder.HasMany(x => x.Rooms).WithOne(x => x.AppUser);
    }
}