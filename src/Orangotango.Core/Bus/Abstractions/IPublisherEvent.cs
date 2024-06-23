using Orangotango.Core.Events;
using System.Threading.Tasks;

namespace Orangotango.Core.Bus.Abstractions;

public interface IPublisherEvent
{
    Task Publish<TEvent>(TEvent @event) where TEvent : Event;
}
