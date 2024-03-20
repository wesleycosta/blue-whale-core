namespace PixelHotel.Core.DomainObjects;

public abstract class EntityWithSoftDelete : Entity
{
    public bool Removed { get; private set; }

    public void Remove()
    {
        Removed = true;
    }
}
