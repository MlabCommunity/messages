using Convey.CQRS.Events;
using Lapka.Messages.Application.ExternalEvents;
using Lapka.Messages.Core.Repositories;

namespace Lapka.Messages.Infrastructure.ExternalEventHandlers;

internal sealed class UserDeletedEventHandler : IEventHandler<UserDeletedEvent>
{
    private readonly IAppUserRepository _repository;

    public UserDeletedEventHandler(IAppUserRepository repository)
    {
        _repository = repository;
    }
    
    public async Task HandleAsync(UserDeletedEvent @event, CancellationToken cancellationToken = new CancellationToken())
    {
        var user = await _repository.FindByIdAsync(@event.UserId);

        if (user is null)
        {
            return;
        }

        await _repository.DeleteAsync(user);
    }
}