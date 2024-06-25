using MassTransit;
using Orangotango.Core.Bus.Abstractions;
using System;
using System.Threading.Tasks;
using Event = Orangotango.Core.Events.Event;

namespace Orangotango.Infra.Bus;

internal sealed class PublisherEvent(IBus _bus) : IPublisherEvent
{
    public async Task Publish<TEvent>(TEvent @event) where TEvent : Event
    {
        @event.Timestamp = DateTimeOffset.UtcNow;
        if (@event.TranceId == Guid.Empty)
        {
            @event.SetTraceId(Guid.NewGuid());
        }

        await _bus.Publish(@event);
    }
}
