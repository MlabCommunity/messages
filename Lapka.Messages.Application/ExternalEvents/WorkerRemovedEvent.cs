using Convey.CQRS.Events;
using Convey.MessageBrokers;

namespace Lapka.Messages.Application.ExternalEvents;

[Message("pet")]
public record WorkerRemovedEvent(Guid UserId, Guid ShelterId) : IEvent;