using Convey.CQRS.Queries;

namespace Lapka.Messages.Application.Queries;

public record GetUnreadMessageCountQuery(Guid RoomId) : IQuery<int>;