using System;
using NServiceBus;

namespace Shared
{
    public class DeliverOrder : ICommand
    {
        public Guid OrderId { get; set; }
    }
}