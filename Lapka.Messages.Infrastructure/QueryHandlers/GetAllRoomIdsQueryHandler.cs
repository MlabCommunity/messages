using Convey.CQRS.Queries;
using Lapka.Messages.Application.Dto;
using Lapka.Messages.Application.Queries;
using Lapka.Messages.Core;
using Lapka.Messages.Core.Entities;
using Lapka.Messages.Infrastructure.Database.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Lapka.Messages.Infrastructure.QueryHandlers;

internal sealed class GetAllRoomIdsQueryHandler : IQueryHandler<GetAllRoomIdsQuery, List<string>>
{
    private readonly DbSet<AppUser> _users;

    public GetAllRoomIdsQueryHandler(AppDbContext context)
    {
        _users = context.AppUsers;
    }

    public async Task<List<string>> HandleAsync(GetAllRoomIdsQuery query,
        CancellationToken cancellationToken = new CancellationToken())
        => await _users
            .Include(x => x.Rooms)
            .Where(x => x.UserId == query.PrincipalId)
            .Select(x => x.Rooms
                .Select(x => x.RoomId.ToString())
                .ToList())
            .FirstOrDefaultAsync();
}