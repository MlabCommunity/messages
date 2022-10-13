using Convey.CQRS.Commands;
using Lapka.Messages.Application.Exceptions;
using Lapka.Messages.Core.Repositories;

namespace Lapka.Messages.Application.Commands.Handlers;

internal sealed class UserOfflineCommandHandler : ICommandHandler<UserOfflineCommand>
{
    private readonly IAppUserRepository _repository;

    public UserOfflineCommandHandler(IAppUserRepository repository)
    {
        _repository = repository;
    }

    public async Task HandleAsync(UserOfflineCommand command,
        CancellationToken cancellationToken = new CancellationToken())
    {
        var user = await _repository.FindByIdAsync(command.PrincipalId);

        if (user is null)
        {
            throw new UserNotFoundException();
        }

        user.Offline();

        await _repository.UpdateAsync(user);
    }
}