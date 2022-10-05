using Lapka.Messages.Core;
using Microsoft.EntityFrameworkCore;

namespace Lapka.Messages.Infrastructure.Database.Contexts;

internal class AppDbContext : DbContext
{

    public DbSet<AppUser> AppUsers { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<Room> Rooms { get; set; }

    public AppDbContext(DbContextOptions options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("messages");

        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}