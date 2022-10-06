using Convey.CQRS.Commands;

namespace Lapka.Messages.Application.Commands;

public record EnsureRoomCommand(Guid PrincipalId, Guid ReceiverId) : ICommand;
