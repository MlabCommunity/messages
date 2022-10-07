using Convey.CQRS.Commands;

namespace Lapka.Messages.Application.Commands;

public record CreateRoomCommand(Guid PrincipalId,Guid ReceiverId) : ICommand;