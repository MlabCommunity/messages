using Convey.CQRS.Queries;
using Lapka.Messages.Application.Queries;
using Lapka.Messages.Core;
using Lapka.Messages.Core.Entities;
using Lapka.Messages.Infrastructure.Database.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Lapka.Messages.Infrastructure.QueryHandlers;

internal sealed class GetUnreadMessageCountQueryHandler : IQueryHandler<GetUnreadMessageCountQuery, int>
{
    private readonly DbSet<Room> _rooms;

    public GetUnreadMessageCountQueryHandler(AppDbContext context)
    {
        _rooms = context.Rooms;
    }


    public async Task<int> HandleAsync(GetUnreadMessageCountQuery query,
        CancellationToken cancellationToken = new CancellationToken())
        => await _rooms
            .Include(x => x.Messages)
            .Where(x => x.RoomId == query.RoomId)
            .Select(x => x.Messages
                .Count(x => x.IsUnread))
            .FirstOrDefaultAsync();
}