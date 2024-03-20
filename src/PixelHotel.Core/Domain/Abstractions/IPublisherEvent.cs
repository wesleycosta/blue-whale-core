using PixelHotel.Core.Domain.Events;

namespace PixelHotel.Core.Domain.Abstractions;

public interface IPublisherEvent
{
    Task PublishEvent<TEvent>(TEvent eventMessage) where TEvent : Event;
}
