namespace PixelHotel.Core.Domain;

public abstract class EntityBase
{
    public Guid Id { get; protected set; }
    public bool Removed { get; protected set; }

    public void GenerateId()
        => Id = Guid.NewGuid();

    public void Remove()
        => Removed = true;
}
