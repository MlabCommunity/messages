using Lapka.Messages.Core;
using Lapka.Messages.Core.Entities;
using Lapka.Messages.Core.Repositories;
using Lapka.Messages.Infrastructure.Database.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Lapka.Messages.Infrastructure.Database.Repositories;

internal sealed class RoomRepository : IRoomRepository
{
    private readonly DbSet<Room> _rooms;
    private readonly AppDbContext _context;

    public RoomRepository(AppDbContext context)
    {
        _rooms = context.Rooms;
        _context = context;
    }

    public async Task AddAsync(Room room)
    {
        await _rooms.AddAsync(room);
        await _context.SaveChangesAsync();
    }

    public async Task<Room> FindAsync(Guid roomId)
        => await _rooms
            .Include(x => x.AppUsers)
            .Include(x=>x.Messages)
            .FirstOrDefaultAsync(x => x.RoomId == roomId);

    public async Task<Room> FindByUsers(Guid principalId, Guid receiverId)
    {
        var room = await _rooms
            .Include(x => x.AppUsers)
            .Include(x=>x.Messages)
            .Where(x => x.AppUsers.Any(x => x.UserId == principalId))
            .ToListAsync();


        return room
            .Where(x => x.AppUsers
                .Any(x => x.UserId == receiverId))
            .FirstOrDefault();
    }
}