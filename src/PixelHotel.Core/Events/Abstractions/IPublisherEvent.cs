namespace PixelHotel.Core.Events.Abstractions;

public interface IPublisherEvent
{
    Task Publish<TEvent>(TEvent @event) where TEvent : Event;
}
