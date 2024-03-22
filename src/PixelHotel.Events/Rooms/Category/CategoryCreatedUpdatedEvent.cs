using PixelHotel.Core.Events;

namespace PixelHotel.Events.Rooms.Category;

public class CategoryCreatedUpdatedEvent : Event
{
    public string Name { get; private set; }

    public CategoryCreatedUpdatedEvent(Guid aggregateId,
        string name)
    {
        AggregateId = aggregateId;
        Name = name;
    }
}
