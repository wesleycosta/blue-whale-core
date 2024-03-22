using MediatR;
using PixelHotel.Core.Services;

namespace PixelHotel.Core.Domain;

public class CommandBase : IRequest<Result>
{
}
