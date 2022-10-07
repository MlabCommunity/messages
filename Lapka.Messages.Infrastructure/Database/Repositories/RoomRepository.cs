using System.Collections;
using Lapka.Messages.Core;
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

    public async Task<Room> FindByUserIds(Guid user1, Guid user2)
    {
        var rooms =  await _rooms
            .Include(x => x.AppUsers)
            .Where(x => x.AppUsers.Any(x => x.UserId == user1)).ToListAsync();

        return rooms.Where(x=>x.AppUsers.Any(x=>x.UserId==user2)).FirstOrDefault();
    }

    public async Task<Room> FindById(Guid roomId)
        => await _rooms
            .Include(x=>x.AppUsers)
            .Include(x => x.Messages)
            .FirstOrDefaultAsync(x => x.RoomId == roomId);
    
    public async Task CreateRoom(Room room)
    {
        await _rooms.AddAsync(room);
        await _context.SaveChangesAsync();
    }
}