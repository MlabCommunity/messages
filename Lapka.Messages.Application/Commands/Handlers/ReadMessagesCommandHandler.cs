using Convey.CQRS.Commands;
using Lapka.Messages.Application.Exceptions;
using Lapka.Messages.Core.Repositories;

namespace Lapka.Messages.Application.Commands.Handlers;

internal sealed class ReadMessagesCommandHandler : ICommandHandler<ReadMessagesCommand>
{
    private readonly IMessageRepository _repository;

    public ReadMessagesCommandHandler(IMessageRepository repository)
    {
        _repository = repository;
    }

    public async Task HandleAsync(ReadMessagesCommand command, CancellationToken cancellationToken = new CancellationToken())
    {
        var messages = await _repository.FindByUserIdAndReceiverId(command.PrincipalId,command.ReceiverId);

        if (messages is null)
        {
            throw new RoomNotFoundException();
        }
        
        foreach (var message in messages)
        {
            if (message.ReceiverId == command.PrincipalId)
            {
                message.Read();
            }
        }

        await _repository.UpdateAsync(messages);
    }
}