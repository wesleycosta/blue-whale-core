using Orangotango.Core.Events;

namespace Orangotango.Events.Rooms;

public class RoomCreatedOrUpdatedEvent : Event
{
    public string Name { get; set; }
    public int Number { get; set; }
}
