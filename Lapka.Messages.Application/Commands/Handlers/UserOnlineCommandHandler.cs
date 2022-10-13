using Convey.CQRS.Commands;
using Lapka.Messages.Application.Exceptions;
using Lapka.Messages.Core.Repositories;

namespace Lapka.Messages.Application.Commands.Handlers;

internal sealed class UserOnlineCommandHandler : ICommandHandler<UserOnlineCommand>
{
    private readonly IAppUserRepository _repository;

    public UserOnlineCommandHandler(IAppUserRepository repository)
    {
        _repository = repository;
    }

    public async Task HandleAsync(UserOnlineCommand command,
        CancellationToken cancellationToken = new CancellationToken())
    {
        var user = await _repository.FindByIdAsync(command.PrincipalId);

        if (user is null)
        {
            throw new UserNotFoundException();
        }

        user.Online();

        await _repository.UpdateAsync(user);
    }
}