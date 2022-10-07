using Lapka.Messages.Core;
using Lapka.Messages.Core.Repositories;
using Lapka.Messages.Infrastructure.Database.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Lapka.Messages.Infrastructure.Database.Repositories;

internal sealed class MessageRepository : IMessageRepository
{

    private readonly DbSet<Message> _messages;
    private readonly AppDbContext _context;

    public MessageRepository(AppDbContext context)
    {
        _messages = context.Messages;
        _context = context;
    }

    public async Task AddAsync(Message message)
    {
        await _messages.AddAsync(message);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Message>> FindByRoomId(Guid roomId)
        => await _messages.Where(x => x.RoomId == roomId).ToListAsync();

    public async Task UpdateAsync(List<Message> messages)
    {
        _messages.UpdateRange(messages);
        await _context.SaveChangesAsync();
    }
}