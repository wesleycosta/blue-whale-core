using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PixelHotel.Core.Abstractions;
using PixelHotel.Core.Bus.Abstractions;
using PixelHotel.Infra.Configurations;
using PixelHotel.Infra.Events;
using PixelHotel.Infra.Logger;
using System.Collections.Generic;
using System.Reflection;

namespace PixelHotel.Infra;

public static class DependencyRegisterCoordinator
{
    public static IServiceCollection AddServicesDependencies(this IServiceCollection services,
        IConfiguration configuration,
        IEnumerable<Assembly> assemblies)
    {
        services.AddMediator();
        services.AddMessageBus(configuration, assemblies);
        services.RegisterModules(configuration, assemblies);

        return services;
    }

    public static IServiceCollection AddLogger(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSerilog(configuration);
        services.AddSingleton<ILoggerService, LoggerService>();

        return services;
    }

    private static IServiceCollection AddMediator(this IServiceCollection services)
    {
        services.AddMediatR(typeof(MediatorHandler));
        return services.AddScoped<IMediatorHandler, MediatorHandler>();
    }
}
