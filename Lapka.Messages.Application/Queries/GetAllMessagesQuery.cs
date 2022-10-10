using Convey.CQRS.Queries;
using MessageDto = Lapka.Messages.Application.Dto.MessageDto;

namespace Lapka.Messages.Application.Queries;

public record GetAllMessagesQuery(Guid PrincipalId,Guid ReceiverId,int PageNumber =1,int PageSize = 10) : IQuery<Dto.PagedResult<MessageDto>>;