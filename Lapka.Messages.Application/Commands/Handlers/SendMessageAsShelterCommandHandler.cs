using Convey.CQRS.Commands;
using Lapka.Messages.Application.Exceptions;
using Lapka.Messages.Application.Services;
using Lapka.Messages.Core.Consts;
using Lapka.Messages.Core.Entities;
using Lapka.Messages.Core.Repositories;

namespace Lapka.Messages.Application.Commands.Handlers;

internal sealed class SendMessageAsShelterCommandHandler : ICommandHandler<SendMessageAsShelterCommand>
{
    private readonly IMessageRepository _messageRepository;
    private readonly IAppUserRepository _userRepository;
    private readonly IRoomRepository _roomRepository;
    private readonly IWorkerRepository _workerRepository;
    private readonly IUserCacheStorage _storage;

    public SendMessageAsShelterCommandHandler(IMessageRepository messageRepository, IRoomRepository roomRepository,
        IWorkerRepository workerRepository, IUserCacheStorage storage, IAppUserRepository userRepository)
    {
        _messageRepository = messageRepository;
        _roomRepository = roomRepository;
        _workerRepository = workerRepository;
        _storage = storage;
        _userRepository = userRepository;
    }

    public async Task HandleAsync(SendMessageAsShelterCommand command,
        CancellationToken cancellationToken = new CancellationToken())
    {
        var user = await _userRepository.FindAsync(command.PrincipalId);

        if (user is null)
        {
            throw new UserNotFoundException();
        }

        var room = await _roomRepository.FindAsync(command.RoomId);

        if (room is null)
        {
            throw new RoomNotFoundException();
        }

        switch (user.Role)
        {
            case Role.Shelter:
            {
                var message = new Message(user.UserId, command.Content, room);
                await _messageRepository.AddAsync(message);
                
                _storage.SetShelterId(user.UserId, user.UserId);
            } break;

            case Role.Worker:
            {
                var worker = await _workerRepository.FindAsync(command.PrincipalId);

                if (worker is null)
                {
                    throw new UserNotFoundException();
                }

                if (room.AppUsers.Any(x => x.UserId == worker.ShelterId))
                {
                    throw new ProjectForbidden();
                }

                var message = new Message(worker.ShelterId, command.Content, room);
                await _messageRepository.AddAsync(message);


                _storage.SetShelterId(worker.WorkerId, worker.ShelterId);
            } break;
        }
    }
}