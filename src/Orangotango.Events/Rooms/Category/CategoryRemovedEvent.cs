using Orangotango.Core.Events;
using System;

namespace Orangotango.Events.Rooms.Category;

public class CategoryRemovedEvent : Event
{
    public CategoryRemovedEvent(Guid aggregateId)
        => AggregateId = aggregateId;
}
