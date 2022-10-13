using Convey.CQRS.Commands;
using Lapka.Messages.Application.Exceptions;
using Lapka.Messages.Application.Services;
using Lapka.Messages.Core.Entities;
using Lapka.Messages.Core.Repositories;

namespace Lapka.Messages.Application.Commands.Handlers;

internal sealed class CreateRoomCommandHandler : ICommandHandler<CreateRoomCommand>
{
    private readonly IRoomRepository _roomRepository;
    private readonly IAppUserRepository _appUserRepository;
    private readonly IUserCacheStorage _storage;

    public CreateRoomCommandHandler(IRoomRepository roomRepository, IUserCacheStorage storage,
        IAppUserRepository appUserRepository)
    {
        _roomRepository = roomRepository;
        _storage = storage;
        _appUserRepository = appUserRepository;
    }

    public async Task HandleAsync(CreateRoomCommand command,
        CancellationToken cancellationToken = new CancellationToken())
    {
        if (command.PrincipalId == command.ReceiverId)
        {
            throw new UnableToCreateRoomException();
        }

        var room = await _roomRepository.FindByUsers(command.PrincipalId, command.ReceiverId);

        if (room is not null)
        {
            _storage.SetRoomId(command.PrincipalId, room.RoomId);
            return;
        }

        var receiver = await _appUserRepository.FindByIdAsync(command.ReceiverId);

        if (receiver is null)
        {
            throw new UserNotFoundException();
        }

        var principal = await _appUserRepository.FindByIdAsync(command.PrincipalId);

        if (principal is null)
        {
            throw new UserNotFoundException();
        }

        var newRoom = new Room(new List<AppUser> { principal, receiver });

        await _roomRepository.AddAsync(newRoom);

        _storage.SetRoomId(command.PrincipalId, newRoom.RoomId);
    }
}