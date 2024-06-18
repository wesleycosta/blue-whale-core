namespace PixelHotel.Core.Bus
{
    public class ReceiveConfiguration
    {
        public string ExchangeName { get; set; }
        public string QueueName { get; set; }
        public IEnumerable<Type> Consumers { get; set; }
    }
}
