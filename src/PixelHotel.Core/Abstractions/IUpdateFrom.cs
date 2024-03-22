using PixelHotel.Core.Domain;

namespace PixelHotel.Core.Abstractions;

public interface IUpdateFrom<in TCommand> where TCommand : CommandBase
{
    void UpdateFrom(TCommand command);
}
