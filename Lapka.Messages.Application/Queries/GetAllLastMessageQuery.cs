using Convey.CQRS.Queries;
using Lapka.Messages.Application.Dto;

namespace Lapka.Messages.Application.Queries;

public record GetAllLastMessageQuery(Guid PrincipalId) : IQuery<MessageDto>;