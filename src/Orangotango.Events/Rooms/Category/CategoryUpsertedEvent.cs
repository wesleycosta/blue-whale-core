using Orangotango.Core.Events;
using System;

namespace Orangotango.Events.Rooms.Category;

public class CategoryUpsertedEvent : Event
{
    public string Name { get; private set; }

    public CategoryUpsertedEvent(Guid aggregateId,
        string name)
    {
        AggregateId = aggregateId;
        Name = name;
    }
}
