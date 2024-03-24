using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PixelHotel.Api.Middlewares;
using PixelHotel.Api.Swagger;
using PixelHotel.Infra;
using PixelHotel.Infra.Configurations;
using PixelHotel.Infra.Options;
using System.Reflection;

namespace PixelHotel.Api;

public static class DependencyRegisterCoordinator
{
    public static IServiceCollection AddApiConfiguration(this IServiceCollection services,
        IConfiguration configuration,
        Assembly[] assemblies)
    {
        var serviceOptions = configuration.GetServiceOptions();

        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwagger(serviceOptions);
        services.AddServicesDependencies(configuration, assemblies);
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
