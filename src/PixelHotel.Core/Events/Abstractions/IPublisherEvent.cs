using PixelHotel.Core.Events;

namespace PixelHotel.Core.Events.Abstractions;

public interface IPublisherEvent
{
    Task PublishEvent<TEvent>(TEvent eventMessage) where TEvent : Event;
}
