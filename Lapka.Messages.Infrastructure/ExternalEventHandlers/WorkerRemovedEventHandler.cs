using Convey.CQRS.Events;
using Lapka.Messages.Application.ExternalEvents;
using Lapka.Messages.Core.Consts;
using Lapka.Messages.Core.Entities;
using Lapka.Messages.Core.Repositories;

namespace Lapka.Messages.Infrastructure.ExternalEventHandlers;

internal sealed class WorkerRemovedEventHandler : IEventHandler<WorkerRemovedEvent>
{
    private readonly IAppUserRepository _userRepository;
    private readonly IWorkerRepository _workerRepository;

    public WorkerRemovedEventHandler(IAppUserRepository userRepository, IWorkerRepository workerRepository)
    {
        _userRepository = userRepository;
        _workerRepository = workerRepository;
    }


    public async Task HandleAsync(WorkerRemovedEvent @event, CancellationToken cancellationToken = new CancellationToken())
    {
        var user = await _userRepository.FindAsync(@event.UserId);

        if (user is null)
        {
            return;
        }
        
        user.SetRole(Role.User);
        await _userRepository.UpdateAsync(user);
        
        var worker = await _workerRepository.FindAsync(@event.UserId);

        if (worker is null)
        {
            return;
        }
        
        await _workerRepository.DeleteAsync(worker);
    }
}