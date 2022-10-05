using Convey.CQRS.Queries;
using Lapka.Messages.Application.Queries;
using Lapka.Messages.Core;
using Microsoft.EntityFrameworkCore;
using MessageDto = Lapka.Messages.Application.Dto.MessageDto;

namespace Lapka.Messages.Infrastructure.QueryHandlers;

internal sealed class GetAllMessagesQueryHandler : IQueryHandler<GetAllMessagesQuery,PagedResult<MessageDto>>
{
    private readonly DbSet<Message> _messages;
    private readonly DbSet<AppUser> _appUsers;
    private readonly DbSet<Room> _rooms;
    
    public Task<PagedResult<MessageDto>> HandleAsync(GetAllMessagesQuery query, CancellationToken cancellationToken = new CancellationToken())
    {
        
    }
}