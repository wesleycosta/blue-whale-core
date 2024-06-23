using System.Collections.Generic;

namespace Orangotango.Core.Bus
{
    public class PublishConfiguration
    {
        public string ExchangeName { get; set; }
        public IEnumerable<PublishEventConfig> Configs { get; set; }
    }
}
