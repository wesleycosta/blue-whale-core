using PixelHotel.Core.Events;

namespace PixelHotel.Core.Bus.Abstractions;

public interface IPublisherEvent
{
    Task Publish<TEvent>(TEvent @event) where TEvent : Event;
}
