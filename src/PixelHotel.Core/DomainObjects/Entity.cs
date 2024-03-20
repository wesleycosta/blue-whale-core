namespace PixelHotel.Core.DomainObjects;

public abstract class Entity : DomainEvent
{
    public Guid Id { get; protected set; }
}
