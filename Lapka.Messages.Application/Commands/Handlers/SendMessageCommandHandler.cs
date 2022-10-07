using Convey.CQRS.Commands;
using Lapka.Messages.Application.Exceptions;
using Lapka.Messages.Core;
using Lapka.Messages.Core.Repositories;

namespace Lapka.Messages.Application.Commands.Handlers;

internal sealed class SendMessageCommandHandler : ICommandHandler<SendMessageCommand>
{
    private readonly IRoomRepository _roomRepository;
    private readonly IMessageRepository _messageRepository;
    private readonly IAppUserRepository _appUserRepository;

    public SendMessageCommandHandler(IRoomRepository roomRepository, IMessageRepository messageRepository,
        IAppUserRepository appUserRepository)
    {
        _roomRepository = roomRepository;
        _messageRepository = messageRepository;
        _appUserRepository = appUserRepository;
    }

    public async Task HandleAsync(SendMessageCommand command,
        CancellationToken cancellationToken = new CancellationToken())
    {
        var room = await _roomRepository.FindById(command.RoomId);

        if (room is null)
        {
            throw new RoomNotFoundException();
        }

        var receiver = room.AppUsers.FirstOrDefault(x => x.UserId != command.PrincipalId);
        
        if (receiver is null)
        {
            throw new UserNotFoundException();
        }

        var message = new Message(command.PrincipalId, command.Content, receiver.FirstName, room);
        
        await _messageRepository.AddAsync(message);

    }
}