using MediatR;
using Orangotango.Core.Services;

namespace Orangotango.Core.Domain;

public class CommandBase : IRequest<Result>
{
}
