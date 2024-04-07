using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace PixelHotel.Core.Events.Abstractions;

public interface IRegistrationConsumers
{
    void Register(IServiceCollection services, IRabbitMqBusFactoryConfigurator config, IRegistrationContext context);
    void Register(IBusRegistrationConfigurator busRegistrationConfigurator);
}
