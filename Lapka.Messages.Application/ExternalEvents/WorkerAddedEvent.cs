using Convey.CQRS.Events;
using Convey.MessageBrokers;

namespace Lapka.Messages.Application.ExternalEvents;

[Message("pet")]
public record WorkerAddedEvent(Guid UserId,Guid ShelterId) : IEvent;