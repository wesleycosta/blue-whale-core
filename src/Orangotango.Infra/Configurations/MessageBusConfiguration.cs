using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Orangotango.Core.Bus;
using Orangotango.Core.Bus.Abstractions;
using Orangotango.Core.Extensions;
using Orangotango.Infra.Bus;
using Orangotango.Infra.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Orangotango.Infra.Configurations;

internal static class MessageBusConfiguration
{
    public static IServiceCollection AddMessageBus(this IServiceCollection services,
        IConfiguration configuration,
        IEnumerable<Assembly> assembliesConsumers)
    {
        var options = configuration.GetRabbitMQOptions();
        if (!options.IsValid())
            return services;

        services.AddScoped<IPublisherEvent, PublisherEvent>();
        var consumerRegistrations = services.GetConsumerRegistrations(assembliesConsumers);

        services.AddMassTransit(config =>
        {
            var configurations = consumerRegistrations.Select(p => p.GetConfiguration(configuration));
            config.ConfigureBus(options, consumerRegistrations, configurations);
            var consumers = configurations
                .Where(busConfig => busConfig.Receives is not null)
                .SelectMany(busConfig => busConfig.Receives.SelectMany(receive => receive.Consumers));

            foreach (var consumer in consumers)
                config.AddConsumer(consumer);
        });

        return services;
    }

    private static void ConfigureBus(this IBusRegistrationConfigurator config,
        RabbitMqOptions options,
        IEnumerable<IBusConfiguration> consumerRegistrations,
        IEnumerable<BusConfiguration> busConfigurations)
     =>
        config.UsingRabbitMq((context, cfg) =>
        {
            cfg.Host(options.HostName, options.VirtualHost, ConfigureCredential(options));

            foreach (var consumerRegistration in consumerRegistrations)
                foreach (var busConfig in busConfigurations)
                {
                    if (busConfig.Receives is not null)
                        foreach (var receive in busConfig.Receives)
                            cfg.ReceiveEndpoint(receive.QueueName, e =>
                            {
                                e.Bind(receive.ExchangeName);
                                foreach (var consumer in receive.Consumers)
                                    e.ConfigureConsumer(context, consumer);
                            });

                    if (busConfig.Publishes is not null)
                        foreach (var publishe in busConfig.Publishes)
                            foreach (var publishEventConfig in publishe.Configs)
                                cfg.Publish(publishEventConfig.EventType, p =>
                                {
                                    p.BindQueue(publishe.ExchangeName, publishEventConfig.QueueName);
                                });
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

    private static IEnumerable<IBusConfiguration> GetConsumerRegistrations(this IServiceCollection services, 
        IEnumerable<Assembly> assemblies)
    {
        var types = services.GetTypesFromAssemblies<IBusConfiguration>(assemblies);
        return types.Select(p => Activator.CreateInstance(p) as IBusConfiguration);
    }
}
