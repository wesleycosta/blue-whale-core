using MassTransit;
using PixelHotel.Core.Abstractions;
using EventBase = PixelHotel.Core.Events.Event;

namespace PixelHotel.Infra.Events;

public abstract class PublisherEventBase<TEvent> where TEvent : EventBase
{
    private readonly IBus _bus;
    private readonly ILoggerService _loggerService;

    protected PublisherEventBase(IBus bus,
        ILoggerService loggerService)
    {
        _bus = bus;
        _loggerService = loggerService;
    }

    public async Task Publish(TEvent @event)
    {
        var traceId = _loggerService.GetTraceId() ?? Guid.NewGuid();
        var eventType = @event.GetType().Name;
        @event.SetTraceId(traceId);

        await _bus.Publish(@event);
        _loggerService.Information("PublishedEvent", $"Published event {eventType}", @event, traceId);
    }
}
