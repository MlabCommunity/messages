using Convey.CQRS.Events;
using Lapka.Messages.Application.ExternalEvents;
using Lapka.Messages.Core.Consts;
using Lapka.Messages.Core.Repositories;

namespace Lapka.Messages.Infrastructure.ExternalEventHandlers;

internal sealed class UserDeletedEventHandler : IEventHandler<UserDeletedEvent>
{
    private readonly IAppUserRepository _userRepository;
    private readonly IWorkerRepository _workerRepository;

    public UserDeletedEventHandler(IAppUserRepository userRepository, IWorkerRepository workerRepository)
    {
        _userRepository = userRepository;
        _workerRepository = workerRepository;
    }

    public async Task HandleAsync(UserDeletedEvent @event,
        CancellationToken cancellationToken = new CancellationToken())
    {
        var user = await _userRepository.FindAsync(@event.UserId);

        if (user is null)
        {
            return;
        }

        if (user.Role == Role.Worker)
        {
            var worker = await _workerRepository.FindAsync(@event.UserId);
            await _workerRepository.DeleteAsync(worker);
        }

        if (user.Role == Role.Shelter)
        {
            var workers = await _workerRepository.FindByShelterId(user.UserId);
            await _workerRepository.DeleteAsync(workers);
        }

        await _userRepository.DeleteAsync(user);
        
    }
}