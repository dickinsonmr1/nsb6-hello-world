using System;
using NServiceBus;

namespace Shared
{
    public class OrderDelivered : IEvent
    {
        public Guid OrderId { get; set; }
    }
}