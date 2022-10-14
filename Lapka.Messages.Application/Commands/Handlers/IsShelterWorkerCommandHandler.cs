using Convey.CQRS.Commands;
using Lapka.Messages.Application.Exceptions;
using Lapka.Messages.Application.Services;
using Lapka.Messages.Core.Repositories;

namespace Lapka.Messages.Application.Commands.Handlers;

internal sealed class IsShelterWorkerCommandHandler : ICommandHandler<IsShelterWorkerCommand>
{
    private readonly IWorkerRepository _workerRepository;
    private readonly IAppUserRepository _userRepository;
    private readonly IUserCacheStorage _storage;
    
    public IsShelterWorkerCommandHandler(IWorkerRepository workerRepository, IAppUserRepository userRepository, IUserCacheStorage storage)
    {
        _workerRepository = workerRepository;
        _userRepository = userRepository;
        _storage = storage;
    }

    public async Task HandleAsync(IsShelterWorkerCommand command, CancellationToken cancellationToken = new CancellationToken())
    {
        var user = await _userRepository.FindAsync(command.PrincipalId);

        if (user is null)
        {
            throw new UserNotFoundException();
        }
        
        var worker = await _workerRepository.FindAsync(command.PrincipalId);

        if (worker is null)
        {
            throw new ProjectForbidden();
        }
    }
}