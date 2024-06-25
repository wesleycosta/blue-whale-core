using Orangotango.Core.Abstractions;
using Orangotango.Core.Bus.Abstractions;
using Orangotango.Core.Enums;
using Orangotango.Core.Events;
using System.Threading.Tasks;

namespace Orangotango.Core.Bus;

public abstract class PublisherEventBase(ILoggerService _logger,
    IPublisherEvent _publisherEvent) : IPublisherEvent
{
    public virtual async Task Publish<T>(T @event) where T : Event
    {
        @event.SetTraceId(_logger.GetTraceId());
        await _publisherEvent.Publish(@event);

        LogPublishEvent(@event);
    }

    protected void LogPublishEvent<TEvent>(TEvent @event) where TEvent : Event
    {
        var message = $"Event published {typeof(TEvent).Name}";

        _logger.Information(nameof(OperationLogs.EventPublished),
            message,
            @event,
            @event.TranceId);
    }
}
