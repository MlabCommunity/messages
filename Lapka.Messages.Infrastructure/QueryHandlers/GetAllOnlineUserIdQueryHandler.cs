using Convey.CQRS.Queries;
using Lapka.Messages.Application.Queries;
using Lapka.Messages.Core;
using Lapka.Messages.Core.Entities;
using Lapka.Messages.Infrastructure.Database.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Lapka.Messages.Infrastructure.QueryHandlers;

internal sealed class GetAllOnlineUserIdQueryHandler : IQueryHandler<GetAllOnlineUserIdQuery, List<string>>
{
    private readonly DbSet<Room> _rooms;

    public GetAllOnlineUserIdQueryHandler(AppDbContext context)
    {
        _rooms = context.Rooms;
    }

    public async Task<List<string>> HandleAsync(GetAllOnlineUserIdQuery query,
        CancellationToken cancellationToken = new CancellationToken())
        => await _rooms
            .Include(x => x.AppUsers)
            .Where(x => x.AppUsers
                .Any(x => x.UserId == query.PrincipalId))
            .Select(x => x.AppUsers
                .Where(x => x.UserId != query.PrincipalId && x.IsOnline)
                .Select(x => x.UserId.ToString())
                .ToList())
            .FirstOrDefaultAsync();
}