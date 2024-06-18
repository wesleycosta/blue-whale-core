using System.Collections.Generic;

namespace PixelHotel.Core.Bus
{
    public class PublishConfiguration
    {
        public string ExchangeName { get; set; }
        public IEnumerable<PublishEventConfig> Configs { get; set; }
    }
}
