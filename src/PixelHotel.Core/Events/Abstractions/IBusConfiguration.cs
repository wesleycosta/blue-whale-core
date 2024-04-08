namespace PixelHotel.Core.Events.Abstractions;

public interface IBusConfiguration
{
    //void ConfigureEndpoint(IServiceCollection services, IRabbitMqBusFactoryConfigurator config, IRegistrationContext context);
    BusConfiguration GetConfiguration();
}
