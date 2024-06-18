using PixelHotel.Core.Events;
using System.Threading.Tasks;

namespace PixelHotel.Core.Bus.Abstractions;

public interface IPublisherEvent
{
    Task Publish<TEvent>(TEvent @event) where TEvent : Event;
}
