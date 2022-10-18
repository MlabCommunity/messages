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
    
    public SendMessageCommandHandler(IMessageRepository messageRepository, IRoomRepository roomRepository)
    {
        _messageRepository = messageRepository;
        _roomRepository = roomRepository;
    }

    public async Task HandleAsync(SendMessageCommand command,
        CancellationToken cancellationToken = new CancellationToken())
    {
        var room = await _roomRepository.FindAsync(command.RoomId);

        if (room is null)
        {
            throw new RoomNotFoundException();
        }

        var message = new Message(command.PrincipalId, command.Content, room);

        await _messageRepository.AddAsync(message);
    }
}