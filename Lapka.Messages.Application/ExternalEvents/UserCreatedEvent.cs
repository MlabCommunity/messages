using Convey.CQRS.Events;

namespace Lapka.Messages.Application.ExternalEvents;


public record UserCreatedEvent(Guid UserId, string Role, string FirstName, string LastName, string ProfilePicture, string Email, string UserName) : IEvent;