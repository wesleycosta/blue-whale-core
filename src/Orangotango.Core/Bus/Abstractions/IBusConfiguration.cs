using Orangotango.Core.Bus;

namespace Orangotango.Core.Bus.Abstractions;

public interface IBusConfiguration
{
    BusConfiguration GetConfiguration();
}
