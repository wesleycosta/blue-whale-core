using Orangotango.Core.Domain;
using Orangotango.Core.Services;
using System.Threading.Tasks;

namespace Orangotango.Core.Bus.Abstractions;

public interface IMediatorHandler
{
    Task<Result> SendCommand<TCommand>(TCommand command) where TCommand : CommandBase;
}
