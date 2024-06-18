namespace PixelHotel.Core.Bus
{
    public class BusConfiguration
    {
        public IEnumerable<PublishConfiguration> Publishes { get; set; }
        public IEnumerable<ReceiveConfiguration> Receives { get; set; }
    }
}
