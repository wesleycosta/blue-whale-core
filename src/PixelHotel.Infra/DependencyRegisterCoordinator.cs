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
        params Assembly[] assemblies)
    {
        services.AddBaseOptions(configuration);
        services.AddLogger(configuration);
        services.AddMediator();
        services.AddPublisherEvent();
        services.RegisterModules(assemblies);

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

    private static IServiceCollection AddPublisherEvent(this IServiceCollection services)
    {
        services.AddScoped<IPublisherEvent, PublisherEvent>();
        return services;
    }
}
