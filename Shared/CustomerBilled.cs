﻿using System;
using NServiceBus;

namespace Shared
{
    public class CustomerBilled : IEvent
    {
        public Guid OrderId { get; set; }
    }
}