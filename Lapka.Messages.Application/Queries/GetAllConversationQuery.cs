using Convey.CQRS.Queries;
using Lapka.Messages.Application.Dto;

namespace Lapka.Messages.Application.Queries;

public record GetAllConversationQuery(Guid PrincipalId) : IQuery<Dto.PagedResult<UserDto>>;

