using MassTransit;
using Orangotango.Core.Abstractions;
using Orangotango.Core.Bus.Abstractions;
using Orangotango.Core.Enums;
using System;
using System.Threading.Tasks;
using Event = Orangotango.Core.Events.Event;

namespace Orangotango.Infra.Bus;

internal sealed class PublisherEvent(IBus _bus,
    ILoggerService _logger) : IPublisherEvent
{
    public async Task Publish<TEvent>(TEvent @event) where TEvent : Event
    {
        @event.Timestamp = DateTimeOffset.UtcNow;
        if (@event.TranceId == default)
        {
            @event.TranceId = _logger.GetTraceId();
        }

        await _bus.Publish(@event);
    }
}
