using MediatR;
using PixelHotel.Core.Data;
using PixelHotel.Core.Domain;

namespace PixelHotel.Core.Services;

public abstract class CommandHandlerBase<TCommand> : ServiceBase, IRequestHandler<TCommand, Result> where TCommand : Command
{
    protected CommandHandlerBase(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public abstract Task<Result> Handle(TCommand request, CancellationToken cancellationToken);
}
