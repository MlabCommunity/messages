using Convey.CQRS.Queries;
using Lapka.Messages.Application.Queries;
using Lapka.Messages.Core;
using Lapka.Messages.Infrastructure.Database.Contexts;
using Lapka.Messages.Infrastructure.Mapper;
using Microsoft.EntityFrameworkCore;
using MessageDto = Lapka.Messages.Application.Dto.MessageDto;

namespace Lapka.Messages.Infrastructure.QueryHandlers;

internal sealed class GetAllMessagesQueryHandler : IQueryHandler<GetAllMessagesQuery, Application.Dto.PagedResult<MessageDto>>
{
    private readonly DbSet<Message> _messages;

    public GetAllMessagesQueryHandler(AppDbContext context)
    {
        _messages = context.Messages;
    }

    public async Task<Application.Dto.PagedResult<MessageDto>> HandleAsync(GetAllMessagesQuery query, CancellationToken cancellationToken = new CancellationToken())
    {
        var messages = await _messages
            .Where(x => (x.ReceiverId == query.PrincipalId && x.SenderId==query.ReceiverId) || (x.ReceiverId == query.ReceiverId && x.SenderId==query.PrincipalId))
            .OrderByDescending(x => x.CreatedAt)
            .Skip(query.PageSize * (query.PageNumber - 1))
            .Take(query.PageSize)
            .Select(x => x.AsDto(query.PrincipalId)).ToListAsync();

        var count = await _messages
            .Where(x => x.ReceiverId == query.PrincipalId)
            .CountAsync();

        return new Application.Dto.PagedResult<MessageDto>(messages, count, query.PageSize, query.PageNumber);
    }
}