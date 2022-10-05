using Convey.CQRS.Events;
using Lapka.Messages.Application.ExternalEvents;
using Lapka.Messages.Core;
using Lapka.Messages.Core.Repositories;

namespace Lapka.Messages.Infrastructure.ExternalEventHandlers;

internal sealed class UserCreatedEventHandler : IEventHandler<UserCreatedEvent>
{
    private readonly IAppUserRepository _repository;

    public UserCreatedEventHandler(IAppUserRepository repository)
    {
        _repository = repository;
    }
    
    public async Task HandleAsync(UserCreatedEvent @event, CancellationToken cancellationToken = new CancellationToken())
    {
        var exists = await _repository.ExistAsync(@event.UserId);

        if (exists)
        {
            return;
        }

        await _repository.AddAsync(new AppUser(@event.UserId, @event.Email,@event.FirstName, @event.LastName,@event.ProfilePicture));
    }
}