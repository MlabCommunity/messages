using Convey.CQRS.Commands;

namespace Lapka.Messages.Application.Commands;


public record SendMessageAsShelterCommand(Guid PrincipalId, Guid RoomId, string Content) : ICommand;