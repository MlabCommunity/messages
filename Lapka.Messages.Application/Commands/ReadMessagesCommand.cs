using Convey.CQRS.Commands;

namespace Lapka.Messages.Application.Commands;

public record ReadMessagesCommand(Guid PrincipalId,Guid ReceiverId) : ICommand;