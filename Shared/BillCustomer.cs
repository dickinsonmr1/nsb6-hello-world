using System;
using NServiceBus;

namespace Shared
{
    public class BillCustomer : ICommand
    {
        public Guid OrderId { get; set; }
    }
}