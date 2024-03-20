namespace PixelHotel.Core.DomainObjects;

public sealed class DomainException(string message) : Exception(message)
{
}
