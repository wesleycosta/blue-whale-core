using PixelHotel.Core.Events;

namespace PixelHotel.Events.Rooms.Category;

public class CategoryRemovedEvent : Event
{
    public CategoryRemovedEvent(Guid aggregateId)
        => AggregateId = aggregateId;
}
