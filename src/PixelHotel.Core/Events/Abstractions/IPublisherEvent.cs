namespace PixelHotel.Core.Events.Abstractions;

public interface IPublisherEvent
{
    Task Publish<TEvent>(TEvent eventMessage) where TEvent : Event;
}
