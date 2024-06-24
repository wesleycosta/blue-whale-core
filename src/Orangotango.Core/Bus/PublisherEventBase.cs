using Orangotango.Core.Abstractions;
using Orangotango.Core.Enums;
using Orangotango.Core.Events;

namespace Orangotango.Core.Bus;

public abstract class PublisherEventBase(string _eventName,
    ILoggerService _logger)
{
    protected void LogPublishEvent<TEvent>(TEvent @event) where TEvent : Event
    {
        var message = $"Event published {_eventName}";

        _logger.Information(nameof(OperationLogs.EventPublished),
            message,
            @event,
            @event.TranceId);
    }
}
