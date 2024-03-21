using Microsoft.Extensions.DependencyInjection;

namespace PixelHotel.Core.Abstractions;

public interface IModuleRegister
{
    IServiceCollection RegisterServices(IServiceCollection services);
}
