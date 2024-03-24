using PixelHotel.Core.Abstractions;
using PixelHotel.Core.Events;
using PixelHotel.Core.Events.Abstractions;
using PixelHotel.Infra.Logger;

namespace PixelHotel.Infra.Events;

internal class PublisherEvent(ILoggerService _loggerService) : IPublisherEvent
{
    public async Task Publish<TEvent>(TEvent eventMessage) where TEvent : Event
    {
        var traceId = _loggerService.GetTraceId() ?? Guid.NewGuid();
        var eventType = eventMessage.GetType().Name;

        var wrapper = new EventWrapper(eventType,
            traceId,
            eventMessage);

        _loggerService.Information(nameof(OperationLogs.PublishedEvent), $"Published event {eventType}", wrapper, traceId);

        await Task.CompletedTask;
    }
}
