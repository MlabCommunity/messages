using Convey.CQRS.Commands;
using Lapka.Messages.Application.Exceptions;
using Lapka.Messages.Core.Repositories;

namespace Lapka.Messages.Application.Commands.Handlers;

internal sealed class IsRoomMemberCommandHandler : ICommandHandler<IsRoomMemberCommand>
{
    private readonly IRoomRepository _repository;

    public IsRoomMemberCommandHandler(IRoomRepository repository)
    {
        _repository = repository;
    }

    public async Task HandleAsync(IsRoomMemberCommand command, CancellationToken cancellationToken = new CancellationToken())
    {
        var room = await _repository.FindAsync(command.RoomId);

        if (room is null)
        {
            throw new RoomNotFoundException();
        }
        
        if(!room.AppUsers.Any(x=>x.UserId==command.PrincipalId))
        {
            throw new UserNotFoundException();
        }
    }
}