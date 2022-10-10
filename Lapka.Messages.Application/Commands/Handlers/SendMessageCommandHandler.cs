using Convey.CQRS.Commands;
using Lapka.Messages.Application.Exceptions;
using Lapka.Messages.Core;
using Lapka.Messages.Core.Repositories;

namespace Lapka.Messages.Application.Commands.Handlers;

internal sealed class SendMessageCommandHandler : ICommandHandler<SendMessageCommand>
{
    private readonly IMessageRepository _messageRepository;
    private readonly IAppUserRepository _appUserRepository;

    public SendMessageCommandHandler(IMessageRepository messageRepository,
        IAppUserRepository appUserRepository)
    {
        _messageRepository = messageRepository;
        _appUserRepository = appUserRepository;
    }

    public async Task HandleAsync(SendMessageCommand command,
        CancellationToken cancellationToken = new CancellationToken())
    {

        var user = await _appUserRepository.FindByIdAsync(command.PrincipalId);

        if (user is null)
        {
            throw new UserNotFoundException();
        }

        var receiver = await _appUserRepository.FindByIdAsync(command.ReceiverId);

        var message = new Message(receiver.UserId, command.Content, receiver.FirstName, user);
        
        await _messageRepository.AddAsync(message);

    }
}