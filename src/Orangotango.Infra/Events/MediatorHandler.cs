using MediatR;
using Orangotango.Core.Bus.Abstractions;
using Orangotango.Core.Domain;
using Orangotango.Core.Services;
using System.Threading.Tasks;

namespace Orangotango.Infra.Events;

public sealed class MediatorHandler(IMediator _mediator) : IMediatorHandler
{
    public async Task<Result> SendCommand<TCommand>(TCommand command) where TCommand : CommandBase
        => await _mediator.Send(command);
}
