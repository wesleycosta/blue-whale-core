using PixelHotel.Core.Messages;

namespace PixelHotel.Core.Events;

public interface IPublisherEvent
{
    Task PublishEvent<TEvent>(TEvent eventMessage) where TEvent : Event;
}
