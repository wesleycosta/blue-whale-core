using System;

namespace Orangotango.Core.Bus
{
    public class PublishEventConfig
    {
        public Type EventType { get; set; }
        public string QueueName { get; set; }
    }
}
