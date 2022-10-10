using Convey.CQRS.Queries;

namespace Lapka.Messages.Application.Queries;

public record GetAllUserConversationGuidQuery(Guid PrincipalId) : IQuery<List<string>>;
