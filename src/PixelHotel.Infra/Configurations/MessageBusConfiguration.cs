using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PixelHotel.Core.Events;
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
        var configurations = consumerRegistrations.Select(p => p.GetConfiguration());

        services.AddMassTransit(config =>
        {
            config.ConfigureBus(options, services, consumerRegistrations, configurations);

            foreach (var busConfig in configurations)
            {
                if (busConfig.Receives is not null)
                {
                    foreach (var receive in busConfig.Receives)
                        foreach (var consumer in receive.Consumers)
                        {
                            config.AddConsumer(consumer);
                        }
                }
            }
        });

        return services;
    }

    private static void ConfigureBus(this IBusRegistrationConfigurator config,
        RabbitMqOptions options,
        IServiceCollection services,
        IEnumerable<IBusConfiguration> consumerRegistrations,
        IEnumerable<BusConfiguration> busConfigurations)
     =>
        config.UsingRabbitMq((context, cfg) =>
        {
            cfg.Host(options.HostName, options.VirtualHost, ConfigureCredential(options));

            foreach (var consumerRegistration in consumerRegistrations)
            {
                foreach (var busConfig in busConfigurations)
                {
                    if (busConfig.Receives is not null)
                    {
                        foreach (var receive in busConfig.Receives)
                        {
                            cfg.ReceiveEndpoint(receive.QueueName, e =>
                            {
                                foreach (var consumer in receive.Consumers)
                                {
                                    e.ConfigureConsumer(context, consumer);
                                }
                            });
                        }
                    }

                    if (busConfig.Publishes is not null)
                    {
                        foreach (var publishe in busConfig.Publishes)
                            foreach (var publishEventConfig in publishe.Configs)
                            {
                                cfg.Publish(publishEventConfig.EventType, p =>
                                {
                                    p.BindQueue(publishe.ExchangeName, publishEventConfig.Queue);
                                });
                            }
                    }
                }
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

    private static IEnumerable<IBusConfiguration> GetConsumerRegistrations(this IServiceCollection services, IEnumerable<Assembly> assemblies)
    {
        var types = services.GetTypesFromAssemblies<IBusConfiguration>(assemblies);
        return types.Select(p => Activator.CreateInstance(p) as IBusConfiguration);
    }
}
