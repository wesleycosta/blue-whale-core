namespace PixelHotel.Core.Events
{
    public class BusConfiguration
    {
        public IEnumerable<PublishConfiguration> Publishes { get; set; }
        public IEnumerable<ReceiveConfiguration> Receives { get; set; }
    }

    public class PublishConfiguration
    {
        public string ExchangeName { get; set; }
        public IEnumerable<PublishEventConfig> Configs { get; set; }
    }

    public class PublishEventConfig
    {
        public Type EventType { get; set; }
        public string Queue { get; set; }
    }

    public class ReceiveConfiguration
    {
        public string ExchangeName { get; set; }
        public string QueueName { get; set; }
        public IEnumerable<Type> Consumers { get; set; }
    }
}
