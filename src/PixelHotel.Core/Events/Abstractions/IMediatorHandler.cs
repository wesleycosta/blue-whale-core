using PixelHotel.Core.Domain;
using PixelHotel.Core.Services;

namespace PixelHotel.Core.Events.Abstractions;

public interface IMediatorHandler
{
    Task<Result> SendCommand<TCommand>(TCommand command) where TCommand : CommandBase;
}
