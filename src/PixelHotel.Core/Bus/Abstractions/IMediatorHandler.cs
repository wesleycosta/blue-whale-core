using PixelHotel.Core.Domain;
using PixelHotel.Core.Services;
using System.Threading.Tasks;

namespace PixelHotel.Core.Bus.Abstractions;

public interface IMediatorHandler
{
    Task<Result> SendCommand<TCommand>(TCommand command) where TCommand : CommandBase;
}
