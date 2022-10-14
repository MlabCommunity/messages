using Convey.CQRS.Commands;

namespace Lapka.Messages.Application.Commands;

public record IsShelterWorkerCommand(Guid PrincipalId,Guid RoomId) : ICommand;