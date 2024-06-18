using PixelHotel.Core.Events;
using System;
using System.Drawing;

namespace PixelHotel.Events.Rooms;

public class RoomCreatedOrUpdatedEvent : Event
{
    public string Name { get; set; }
    public int Number { get; set; }

    public RoomCreatedOrUpdatedEvent()
    {

    }

    public RoomCreatedOrUpdatedEvent(Guid id,
        string name,
        int number)
    {
        AggregateId = id;
        Name = name;
        Number = number;
    }
}
