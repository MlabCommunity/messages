using Convey.CQRS.Queries;
using Lapka.Messages.Application.Queries;
using Lapka.Messages.Core;
using Lapka.Messages.Core.Entities;
using Lapka.Messages.Infrastructure.Database.Contexts;
using Lapka.Messages.Infrastructure.Mapper;
using Microsoft.EntityFrameworkCore;
using MessageDto = Lapka.Messages.Application.Dto.MessageDto;

namespace Lapka.Messages.Infrastructure.QueryHandlers;

internal sealed class
    GetAllMessagesQueryHandler : IQueryHandler<GetAllMessagesQuery, Application.Dto.PagedResult<MessageDto>>
{
    private readonly DbSet<Room> _rooms;

    public GetAllMessagesQueryHandler(AppDbContext context)
    {
        _rooms = context.Rooms;
    }

    public async Task<Application.Dto.PagedResult<MessageDto>> HandleAsync(GetAllMessagesQuery query,
        CancellationToken cancellationToken = new CancellationToken())
    {
        var messages = await _rooms
            .Include(x => x.Messages)
            .Where(x => x.RoomId == query.RoomId)
            .Select(x => x.Messages
                .OrderByDescending(x => x.CreatedAt)
                .Skip(query.PageSize * (query.PageNumber - 1))
                .Take(query.PageSize)
                .Select(x => x
                    .AsDto(query.PrincipalId))
                .ToList())
            .FirstOrDefaultAsync();


        var count = await _rooms
            .Include(x => x.Messages)
            .Where(x => x.RoomId == query.RoomId)
            .Select(x => x.Messages
                .Count())
            .FirstOrDefaultAsync();


        return new Application.Dto.PagedResult<MessageDto>(messages, count, query.PageSize, query.PageNumber);
    }
}