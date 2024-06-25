using Microsoft.Extensions.Configuration;

namespace Orangotango.Core.Bus.Abstractions;

public interface IBusConfiguration
{
    BusConfiguration GetConfiguration(IConfiguration configuration);
}
