using Convey.CQRS.Queries;
using Lapka.Messages.Application.Dto;
using Lapka.Messages.Application.Queries;
using Lapka.Messages.Core;
using Lapka.Messages.Infrastructure.Database.Contexts;
using Lapka.Messages.Infrastructure.Mapper;
using Microsoft.EntityFrameworkCore;

namespace Lapka.Messages.Infrastructure.QueryHandlers;

internal sealed class GetAllConversationQueryHandler : IQueryHandler<GetAllConversationQuery, Application.Dto.PagedResult<MessageDto>>
{
    private readonly DbSet<Message> _messages;
    private readonly DbSet<AppUser> _users;

    public GetAllConversationQueryHandler(AppDbContext context)
    {
        _messages = context.Messages;
        _users = context.AppUsers;
    }
    
    public async Task<Application.Dto.PagedResult<MessageDto>> HandleAsync(GetAllConversationQuery query, CancellationToken cancellationToken = new CancellationToken())
    {
        var ids = await _users
            .Include(x=>x.Messages)
            .Where(x=>x.UserId==query.PrincipalId)
            .Select(x=>x.Messages.GroupBy())
            
    }
}