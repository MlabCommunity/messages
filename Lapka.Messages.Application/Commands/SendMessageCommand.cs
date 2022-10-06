using Convey.CQRS.Commands;

namespace Lapka.Messages.Application.Commands;

public record SendMessageCommand(Guid PrincipalId,Guid RoomId, string Content) : ICommand;