using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PixelHotel.Core.Abstractions;
using PixelHotel.Core.Events.Abstractions;
using PixelHotel.Infra.Events;
using PixelHotel.Infra.Logger;

namespace PixelHotel.Infra;

public static partial class ServiceCollectionModule
{
    public static IServiceCollection AddInfraBasicServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediator();
        services.AddLogger(configuration);

        return services;
    }


    private static IServiceCollection AddMediator(this IServiceCollection services)
    {
        services.AddMediatR(typeof(MediatorHandler));
        return services.AddScoped<IMediatorHandler, MediatorHandler>();
    }

    private static IServiceCollection AddLogger(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<ILoggerService, LoggerService>();
        //services.AddSerilog(configuration);

        return services;
    }
}
