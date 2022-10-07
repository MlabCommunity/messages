using Convey.CQRS.Commands;
using Lapka.Messages.Application.Exceptions;
using Lapka.Messages.Core;
using Lapka.Messages.Core.Repositories;
using Lapka.Pet.Application.Services;

namespace Lapka.Messages.Application.Commands.Handlers;

internal sealed class CreateRoomCommandHandler : ICommandHandler<CreateRoomCommand>
{
    private readonly IRoomRepository _roomRepository;
    private readonly IAppUserRepository _appUserRepository;
    private readonly IUserCacheStorage _storage;
    
    public CreateRoomCommandHandler(IRoomRepository roomRepository, IUserCacheStorage storage, IAppUserRepository appUserRepository)
    {
        _roomRepository = roomRepository;
        _storage = storage;
        _appUserRepository = appUserRepository;
    }

    public async Task HandleAsync(CreateRoomCommand command, CancellationToken cancellationToken = new CancellationToken())
    {
        var room = await _roomRepository.FindByUserIds(command.PrincipalId, command.ReceiverId);

        if (room is null)
        {
            _storage.SetRoomId(command.PrincipalId,room.RoomId);
            return;
        }

        var user1 = await _appUserRepository.FindByIdAsync(command.PrincipalId);
        var user2 = await _appUserRepository.FindByIdAsync(command.ReceiverId);

        if (user1 is null || user2 is null)
        {
            throw new UserNotFoundException();
        }
        
        await _roomRepository.CreateRoom(new Room(user1, user2));
        
        _storage.SetRoomId(command.PrincipalId,room.RoomId);
    }
}