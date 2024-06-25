using Orangotango.Core.Events;
using System;

namespace Orangotango.Events.Rooms.Category;

public class RoomUpsertedEvent : Event
{
    public string Name { get; private set; }
    public int Number { get; private set; }
    public Guid CategoryId { get; private set; }

    public RoomUpsertedEvent(Guid aggregateId,
        string name,
        int number,
        Guid categoryId)
    {
        AggregateId = aggregateId;
        Name = name;
        Number = number;
        CategoryId = categoryId;
    }
}
