using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PixelHotel.Infra.Abstractions;

public interface IModuleRegiterWithConfiguration
{
    IServiceCollection RegisterServices(IServiceCollection services, IConfiguration configuration);
}
