using PixelHotel.Core.Events;

namespace PixelHotel.Events.Rooms;

public class RoomCreatedOrUpdatedEvent : Event
{
    public string Name { get; private set; }
    public int Number { get; private set; }

    public RoomCreatedOrUpdatedEvent(Guid id,
        string name,
        int number)
    {
        AggregateId = id;
        Name = name;
        Number = number;
    }
}

