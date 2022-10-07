using Convey.CQRS.Queries;
using Lapka.Messages.Application.Queries;
using Lapka.Messages.Core;
using Lapka.Messages.Infrastructure.Database.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Lapka.Messages.Infrastructure.QueryHandlers;

internal sealed class GetUnreadMessageCountQueryHandler : IQueryHandler<GetUnreadMessageCountQuery,int>
{
    private readonly DbSet<Message> _messages;

    public GetUnreadMessageCountQueryHandler(AppDbContext context)
    {
        _messages = context.Messages;
    }


    public async Task<int> HandleAsync(GetUnreadMessageCountQuery query,
        CancellationToken cancellationToken = new CancellationToken())
        => await _messages
            .Where(x=>x.ReceiverId==query.PrincipalId && x.IsUnread)
            .CountAsync();
}