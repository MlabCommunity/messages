using Convey.CQRS.Commands;
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
        var messages = await _repository.FindByRoomId(command.RoomId);

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