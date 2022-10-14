using Convey.CQRS.Events;
using Lapka.Messages.Application.ExternalEvents;
using Lapka.Messages.Core.Consts;
using Lapka.Messages.Core.Entities;
using Lapka.Messages.Core.Repositories;

namespace Lapka.Messages.Infrastructure.ExternalEventHandlers;

internal sealed class WorkerAddedEventHandler : IEventHandler<WorkerAddedEvent>
{
    private readonly IWorkerRepository _workerRepository;
    private readonly IAppUserRepository _userRepository;

    public WorkerAddedEventHandler(IWorkerRepository workerRepository, IAppUserRepository userRepository)
    {
        _workerRepository = workerRepository;
        _userRepository = userRepository;
    }

    public async Task HandleAsync(WorkerAddedEvent @event, CancellationToken cancellationToken = new CancellationToken())
    {
        var user = await _userRepository.FindAsync(@event.UserId);

        if (user is null)
        {
            return;
        }
        
        user.SetRole(Role.Worker);
        
        var exists = await _workerRepository.ExistsAsync(@event.UserId);

        if (exists)
        {
            return;
        }
        
        await _workerRepository.AddAsync(new Worker(@event.UserId, @event.ShelterId));
        await _userRepository.UpdateAsync(user);
    }
}