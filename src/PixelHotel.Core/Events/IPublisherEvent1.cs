using PixelHotel.Core.Messages;

namespace PixelHotel.Core.Events;
public interface IPublisherEvent1
{
    Task PublishEvent<T>(T eventMessage) where T : Event;
}