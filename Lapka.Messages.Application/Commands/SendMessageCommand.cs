using Convey.CQRS.Commands;

namespace Lapka.Messages.Application.Commands;

public record SendMessageCommand(Guid PrincipalId,Guid ReceiverId, string Content) : ICommand;