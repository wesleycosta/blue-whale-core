using MediatR;
using Microsoft.Extensions.DependencyInjection;
using PixelHotel.Core.Events.Abstractions;
using PixelHotel.Infra.Events;

namespace PixelHotel.Infra.Extensions;

public static class InfraDependencyInjectionExtension
{
    public static IServiceCollection AddInfraConfiguration(this IServiceCollection services)
    {
        services.AddMediator();

        return services;
    }

    private static IServiceCollection AddMediator(this IServiceCollection services)
    {
        services.AddMediatR(typeof(MediatorHandler));
        return services.AddScoped<IMediatorHandler, MediatorHandler>();
    }
}
