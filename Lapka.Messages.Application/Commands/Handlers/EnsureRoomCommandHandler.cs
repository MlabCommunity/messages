using Convey.CQRS.Commands;
using Lapka.Messages.Application.Exceptions;
using Lapka.Messages.Core;
using Lapka.Messages.Core.Repositories;
using Lapka.Pet.Application.Services;

namespace Lapka.Messages.Application.Commands.Handlers;

internal sealed class EnsureRoomCommandHandler : ICommandHandler<EnsureRoomCommand>
{
    private readonly IAppUserRepository _appUserRepository;
    private readonly IRoomRepository _roomRepository;
    private readonly IUserCacheStorage _storage;


    public EnsureRoomCommandHandler(IAppUserRepository appUserRepository, IRoomRepository roomRepository, IUserCacheStorage storage)
    {
        _appUserRepository = appUserRepository;
        _roomRepository = roomRepository;
        _storage = storage;
    }

    public async Task HandleAsync(EnsureRoomCommand command,
        CancellationToken cancellationToken = new CancellationToken())
    {
        if (command.PrincipalId == command.ReceiverId)
        {
            throw new UnableToCreateRoomException();
        }
        
        var user1 = await _appUserRepository.FindByIdAsync(command.PrincipalId);
        var user2 = await _appUserRepository.FindByIdAsync(command.ReceiverId);

        if (user1 is null || user2 is null)
        {
            throw new UserNotFoundException();
        }

        var existedRoom = await _roomRepository.FindByUserIds(command.PrincipalId,command.ReceiverId);

        if (existedRoom is not null)
        {
            _storage.SetRoomId(command.PrincipalId,existedRoom.RoomId);
            
            return;
        }

        var room = new Room(user1, user2);

        await _roomRepository.CreateRoom(room);
        _storage.SetRoomId(command.PrincipalId,room.RoomId);
    }
}