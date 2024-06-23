using System;
using System.Collections.Generic;

namespace Orangotango.Core.Bus
{
    public class ReceiveConfiguration
    {
        public string ExchangeName { get; set; }
        public string QueueName { get; set; }
        public IEnumerable<Type> Consumers { get; set; }
    }
}
