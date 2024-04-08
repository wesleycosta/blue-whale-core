using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace PixelHotel.Core.Events.Abstractions;

public interface IConsumerRegistration
{
    void Register(IBusRegistrationConfigurator busRegistrationConfigurator);
    void ConfigureEndpoint(IServiceCollection services, IRabbitMqBusFactoryConfigurator config, IRegistrationContext context);
}
