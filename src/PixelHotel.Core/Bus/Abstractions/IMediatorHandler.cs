using PixelHotel.Core.Domain;
using PixelHotel.Core.Services;

namespace PixelHotel.Core.Bus.Abstractions;

public interface IMediatorHandler
{
    Task<Result> SendCommand<TCommand>(TCommand command) where TCommand : CommandBase;
}
