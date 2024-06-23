using Microsoft.Extensions.DependencyInjection;

namespace Orangotango.Core.Abstractions;

public interface IModuleRegister
{
    IServiceCollection RegisterServices(IServiceCollection services);
}
