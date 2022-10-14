using Convey.CQRS.Commands;

namespace Lapka.Messages.Application.Commands;

public record IsRoomMemberCommand(Guid PrincipalId, Guid RoomId) :ICommand;