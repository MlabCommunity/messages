using Convey.CQRS.Queries;

namespace Lapka.Messages.Application.Queries;

public record GetAllOnlineUserIdQuery(Guid PrincipalId) : IQuery<List<string>>;