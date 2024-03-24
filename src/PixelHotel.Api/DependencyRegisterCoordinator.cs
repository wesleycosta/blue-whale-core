using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PixelHotel.Api.Configurations;
using PixelHotel.Api.Middlewares;
using PixelHotel.Infra.Configurations;
using PixelHotel.Infra.Options;

namespace PixelHotel.Api;

public static class DependencyRegisterCoordinator
{
    public static IServiceCollection AddApiConfiguration(this IServiceCollection services,
        IConfiguration configuration)
    {
        var serviceOptions = configuration.GetServiceOptions();

        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwagger(serviceOptions);
        services.AddHttpContextAccessor();

        return services;
    }

    public static IApplicationBuilder UseApiConfiguration(this IApplicationBuilder app, IConfiguration configuration)
    {
        var serviceOptions = configuration.GetServiceOptions();

        app.UseSwaggerConfiguration(serviceOptions);
        app.UseHttpsRedirection();
        app.UseMiddlewares();
        app.UseAuthorization();
        app.AddStartupAndShutdownLog();

        return app;
    }

    private static ServiceOptions GetServiceOptions(this IConfiguration configuration)
    {
        var serviceOptions = new ServiceOptions();
        configuration.Bind(ServiceOptions.Service, serviceOptions);

        return serviceOptions;
    }
}
