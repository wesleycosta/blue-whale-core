using Orangotango.Core.Events;
using System;

namespace Orangotango.Events.Rooms.Category;

public class RoomRemovedEvent : Event
{
    public RoomRemovedEvent(Guid aggregateId)
        => AggregateId = aggregateId;
}
