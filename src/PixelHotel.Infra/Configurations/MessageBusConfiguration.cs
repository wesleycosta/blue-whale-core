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
        var consumerRegistrations = services.GetConsumerRegistrations(assembliesConsumers);

        services.AddMassTransit(config =>
        {
            config.ConfigureBus(options, services, consumerRegistrations);

            foreach (var consumerRegistration in consumerRegistrations)
            {
                consumerRegistration.Register(config);
            }
        });

        return services;
    }

    private static void ConfigureBus(this IBusRegistrationConfigurator config,
        RabbitMqOptions options,
        IServiceCollection services,
        IEnumerable<IConsumerRegistration> consumerRegistrations)
     =>
        config.UsingRabbitMq((context, cfg) =>
        {
            cfg.Host(options.HostName, options.VirtualHost, ConfigureCredential(options));

            foreach (var consumerRegistration in consumerRegistrations)
            {
                consumerRegistration.ConfigureEndpoint(services, cfg, context);
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

    private static IEnumerable<IConsumerRegistration> GetConsumerRegistrations(this IServiceCollection services, IEnumerable<Assembly> assemblies)
    {
        var types = services.GetTypesFromAssemblies<IConsumerRegistration>(assemblies);
        return types.Select(p => Activator.CreateInstance(p) as IConsumerRegistration);
    }
}
