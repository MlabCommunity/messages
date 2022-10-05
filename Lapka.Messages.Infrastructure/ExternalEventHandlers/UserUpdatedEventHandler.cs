using Convey.CQRS.Events;
using Lapka.Messages.Application.ExternalEvents;
using Lapka.Messages.Core.Repositories;

namespace Lapka.Messages.Infrastructure.ExternalEventHandlers;

internal sealed class UserUpdatedEventHandler : IEventHandler<UserUpdatedEvent>
{
    private readonly IAppUserRepository _repository;

    public UserUpdatedEventHandler(IAppUserRepository repository)
    {
        _repository = repository;
    }

    public async Task HandleAsync(UserUpdatedEvent @event,
        CancellationToken cancellationToken = new CancellationToken())
    {
        var user = await _repository.FindByIdAsync(@event.UserId);

        if (user is null)
        {
            return;
        }

        user.Update(@event.FirstName, @event.LastName, @event.Email, @event.ProfilePicture);

        await _repository.UpdateAsync(user);
    }
}