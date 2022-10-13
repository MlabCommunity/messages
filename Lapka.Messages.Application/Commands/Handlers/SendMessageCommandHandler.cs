using Convey.CQRS.Commands;
using Lapka.Messages.Application.Exceptions;
using Lapka.Messages.Application.Services;
using Lapka.Messages.Core;
using Lapka.Messages.Core.Entities;
using Lapka.Messages.Core.Repositories;

namespace Lapka.Messages.Application.Commands.Handlers;

internal sealed class SendMessageCommandHandler : ICommandHandler<SendMessageCommand>
{
    private readonly IMessageRepository _messageRepository;
    private readonly IRoomRepository _roomRepository;
    private readonly IUserCacheStorage _storage;

    public SendMessageCommandHandler(IMessageRepository messageRepository, IRoomRepository roomRepository,
        IUserCacheStorage storage)
    {
        _messageRepository = messageRepository;
        _roomRepository = roomRepository;
        _storage = storage;
    }

    public async Task HandleAsync(SendMessageCommand command,
        CancellationToken cancellationToken = new CancellationToken())
    {
        var room = await _roomRepository.FindById(command.RoomId);

        if (room is null)
        {
            throw new RoomNotFoundException();
        }

        var message = new Message(command.PrincipalId, command.Content, room);

        await _messageRepository.AddAsync(message);

        _storage.SetReceiverIds(room.RoomId, room.AppUsers.Select(x => x.UserId.ToString()).ToList());

        var x = _storage.GetReceiverIds(room.RoomId);
    }
}