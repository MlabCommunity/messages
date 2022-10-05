using Convey.CQRS.Events;
using Convey.MessageBrokers;

namespace Lapka.Messages.Application.ExternalEvents;

[Message("identity")]
public record UserDeletedEvent(Guid UserId, string Role) : IEvent;