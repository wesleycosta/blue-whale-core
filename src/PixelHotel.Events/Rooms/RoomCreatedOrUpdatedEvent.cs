using PixelHotel.Core.Events;

namespace PixelHotel.Events.Rooms;

public class RoomCreatedOrUpdatedEvent : Event
{
    public string Name { get; set; }
    public int Number { get; set; }
}
