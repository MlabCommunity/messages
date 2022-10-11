using Convey.CQRS.Queries;
using Lapka.Messages.Application.Queries;
using Lapka.Messages.Core;
using Lapka.Messages.Infrastructure.Database.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Lapka.Messages.Infrastructure.QueryHandlers;

internal sealed class GetAllOnlineUserIdQueryHandler : IQueryHandler<GetAllOnlineUserIdQuery, List<string>>
{
    private readonly DbSet<Message> _messages;

    public GetAllOnlineUserIdQueryHandler(AppDbContext context)
    {
        _messages = context.Messages;
    }

    public async Task<List<string>> HandleAsync(GetAllOnlineUserIdQuery query,
        CancellationToken cancellationToken = new CancellationToken())
        => await _messages
            .Include(x=>x.SenderUser)
            .Where(x => (x.SenderId == query.PrincipalId || x.ReceiverId == query.PrincipalId ) && x.SenderUser.IsOnline)
            .Select(x => x.ReceiverId.ToString())
            .Distinct()
            .ToListAsync();
}