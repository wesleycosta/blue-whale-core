using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Orangotango.Api.Configurations;
using Orangotango.Api.Middlewares;
using Orangotango.Infra.Configurations;
using Orangotango.Infra.Options;
using System;

namespace Orangotango.Api;

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

    public static IApplicationBuilder UseApiConfiguration(this IApplicationBuilder app,
        IConfiguration configuration,
        Action<IApplicationBuilder> customConfig = null)
    {
        var serviceOptions = configuration.GetServiceOptions();

        app.UseSwaggerConfiguration(serviceOptions);
        app.UseHttpsRedirection();
        app.UseMiddlewares();
        customConfig?.Invoke(app);
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
