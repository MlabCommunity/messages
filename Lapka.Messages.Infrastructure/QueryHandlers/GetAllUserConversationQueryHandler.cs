using Convey.CQRS.Queries;
using Lapka.Messages.Application.Queries;
using Lapka.Messages.Core;
using Lapka.Messages.Infrastructure.Database.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Lapka.Messages.Infrastructure.QueryHandlers;

internal sealed class GetAllUserConversationQueryHandler : IQueryHandler<GetAllUserConversationGuidQuery, List<string>>
{
    private readonly DbSet<Message> _messages;

    public GetAllUserConversationQueryHandler(AppDbContext context)
    {
        _messages = context.Messages;
    }

    public async Task<List<string>> HandleAsync(GetAllUserConversationGuidQuery query,
        CancellationToken cancellationToken = new CancellationToken())
        => await _messages
            .Where( x=> x.SenderId == query.PrincipalId)
            .Select(x => x.ReceiverId.ToString())
            .Distinct()
            .ToListAsync();
    
}