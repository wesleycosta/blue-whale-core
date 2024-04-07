using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PixelHotel.Core.Events.Abstractions;
using PixelHotel.Core.Extensions;
using PixelHotel.Infra.Options;
using System.Reflection;

namespace PixelHotel.Infra.Configurations;

internal static class MessageBusConfiguration
{
    public static IServiceCollection AddMessageBus(this IServiceCollection services,
        IConfiguration configuration,
        IEnumerable<Assembly> assembliesConsumers)
    {
        var options = configuration.GetRabbitMQOptions();
        services.AddMassTransit(config =>
        {
            config.ConfigureBus(options, services, assembliesConsumers);

            var types = services.GetTypesFromAssemblies<IRegistrationConsumers>(assembliesConsumers);

            foreach (var type in types)
            {
                var instancia = Activator.CreateInstance(type) as IRegistrationConsumers;
                instancia.Register(config);
            }
        });

        return services;
    }

    private static void ConfigureBus(this IBusRegistrationConfigurator config, RabbitMqOptions options, IServiceCollection services, IEnumerable<Assembly> assembliesConsumers) =>
       config.UsingRabbitMq((context, cfg) =>
       {
           cfg.Host(options.HostName, options.VirtualHost, ConfigureCredential(options));

           var types = services.GetTypesFromAssemblies<IRegistrationConsumers>(assembliesConsumers);

           foreach (var type in types)
           {
               var instancia = Activator.CreateInstance(type) as IRegistrationConsumers;
               instancia.Register(services, cfg, context);
           }
       });

    private static Action<IRabbitMqHostConfigurator> ConfigureCredential(RabbitMqOptions options) =>
        configure =>
        {
            configure.Username(options.Username);
            configure.Password(options.Password);

        };

    private static RabbitMqOptions GetRabbitMQOptions(this IConfiguration configuration)
    {
        var options = new RabbitMqOptions();
        configuration.Bind(RabbitMqOptions.RabbitMQ, options);

        return options;
    }
}
