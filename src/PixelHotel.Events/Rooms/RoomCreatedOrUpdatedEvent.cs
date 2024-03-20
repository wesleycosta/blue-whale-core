using PixelHotel.Core.Messages;

namespace PixelHotel.Events.Rooms;

public sealed class RoomCreatedOrUpdatedEvent : Event
{
    public string? Name { get; private set; }
    public int Number { get; private set; }

    public RoomCreatedOrUpdatedEvent(Guid id,
        string? name,
        int number)
    {
        AggregateId = id;
        Name = name;
        Number = number;
    }
}

