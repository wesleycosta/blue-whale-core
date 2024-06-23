using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Orangotango.Infra.Abstractions;

public interface IModuleRegiterWithConfiguration
{
    IServiceCollection RegisterServices(IServiceCollection services, IConfiguration configuration);
}
