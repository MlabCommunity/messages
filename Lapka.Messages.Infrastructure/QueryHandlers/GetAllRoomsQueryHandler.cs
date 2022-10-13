using Convey.CQRS.Queries;
using Lapka.Messages.Application.Dto;
using Lapka.Messages.Application.Queries;
using Lapka.Messages.Core;
using Lapka.Messages.Core.Entities;
using Lapka.Messages.Infrastructure.Database.Contexts;
using Lapka.Messages.Infrastructure.Mapper;
using Microsoft.EntityFrameworkCore;

namespace Lapka.Messages.Infrastructure.QueryHandlers;

internal sealed class GetAllRoomsQueryHandler : IQueryHandler<GetAllRoomsQuery, Application.Dto.PagedResult<RoomDto>>
{
    private readonly DbSet<AppUser> _users;
    private readonly DbSet<Room> _rooms;
    private readonly DbSet<Message> _messages;

    public GetAllRoomsQueryHandler(AppDbContext context)
    {
        _users = context.AppUsers;
        _rooms = context.Rooms;
        _messages = context.Messages;
    }

    public async Task<Application.Dto.PagedResult<RoomDto>> HandleAsync(GetAllRoomsQuery query,
        CancellationToken cancellationToken = new CancellationToken())
    {
        var rooms = await _rooms
            .Include(x => x.AppUsers)
            .Include(x => x.Messages)
            .Where(x => x.AppUsers
                .Any(x => x.UserId == query.PrincipalId))
            .Select(x => x.AsDto(query.PrincipalId))
            .ToListAsync();

        var result = rooms
            .OrderByDescending(x => x.Message.CreatedAt)
            .Skip(query.PageSize * (query.PageNumber - 1))
            .Take(query.PageSize)
            .ToList();

        var count = await _rooms
            .Include(x => x.AppUsers)
            .Where(x => x.AppUsers
                .Any(x => x.UserId == query.PrincipalId))
            .CountAsync();

        return new Application.Dto.PagedResult<RoomDto>(result, count, query.PageSize, query.PageNumber);
    }
}