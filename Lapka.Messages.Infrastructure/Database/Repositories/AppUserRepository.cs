using Lapka.Messages.Core;
using Lapka.Messages.Core.Entities;
using Lapka.Messages.Core.Repositories;
using Lapka.Messages.Infrastructure.Database.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Lapka.Messages.Infrastructure.Database.Repositories;

internal sealed class AppUserRepository : IAppUserRepository
{
    private readonly DbSet<AppUser> _appUsers;
    private readonly AppDbContext _context;

    public AppUserRepository(AppDbContext context)
    {
        _context = context;
        _appUsers = context.AppUsers;
    }


    public async Task AddAsync(AppUser appUser)
    {
        await _appUsers.AddAsync(appUser);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistAsync(Guid userId)
        => await _appUsers.AnyAsync(x => x.UserId == userId);

    public async Task<AppUser> FindAsync(Guid userId)
        => await _appUsers
            .Include(x => x.Rooms)
            .FirstOrDefaultAsync(x => x.UserId == userId);

    public async Task UpdateAsync(AppUser appUser)
    {
        _appUsers.Update(appUser);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(AppUser appUser)
    {
        _appUsers.Remove(appUser);
        await _context.SaveChangesAsync();
    }
}