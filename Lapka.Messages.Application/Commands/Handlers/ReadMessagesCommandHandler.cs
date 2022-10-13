using Convey.CQRS.Commands;
using Lapka.Messages.Application.Exceptions;
using Lapka.Messages.Core.Repositories;

namespace Lapka.Messages.Application.Commands.Handlers;

internal sealed class ReadMessagesCommandHandler : ICommandHandler<ReadMessagesCommand>
{
    private readonly IMessageRepository _messageRepository;

    public ReadMessagesCommandHandler(IMessageRepository messageRepository)
    {
        _messageRepository = messageRepository;
    }

    public async Task HandleAsync(ReadMessagesCommand command,
        CancellationToken cancellationToken = new CancellationToken())
    {

        var messages = await _messageRepository.FindUnreadByRoomId(command.RoomId,command.PrincipalId);

        if (messages is null)
        {
            return;
        }
        foreach (var message in messages)
        {
            if (message.SenderId != command.PrincipalId)
            {
                message.Read();
            }
        }
        await _messageRepository.UpdateAsync(messages);
    }
}