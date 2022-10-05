using Convey.CQRS.Events;
using Convey.MessageBrokers;

namespace Lapka.Messages.Application.ExternalEvents;


[Message("identity")]
public record UserUpdatedEvent(Guid UserId, string Role, string FirstName, string LastName, string ProfilePicture,
    string Email) : IEvent;