using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PixelHotel.Core.Abstractions;
using PixelHotel.Core.Events.Abstractions;
using PixelHotel.Infra.Configurations;
using PixelHotel.Infra.Events;
using PixelHotel.Infra.Logger;
using PixelHotel.Infra.Options;
using System.Reflection;

namespace PixelHotel.Infra;

public static partial class DependencyRegisterCoordinator
{
    public static IServiceCollection AddServicesDependencies(this IServiceCollection services,
        IConfiguration configuration,
        Assembly assembly)
    {
        services.AddBaseOptions();
        services.AddLogger(configuration);
        services.AddMediator();
        services.RegisterModules(assembly);

        return services;
    }

    private static IServiceCollection AddMediator(this IServiceCollection services)
    {
        services.AddMediatR(typeof(MediatorHandler));
        return services.AddScoped<IMediatorHandler, MediatorHandler>();
    }

    private static IServiceCollection AddLogger(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSerilog(configuration);
        services.AddSingleton<ILoggerService, LoggerService>();

        return services;
    }
}
