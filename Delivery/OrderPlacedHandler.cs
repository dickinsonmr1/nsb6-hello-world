using System;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;
using Shared;

namespace Delivery
{
    public class OrderPlacedHandler : IHandleMessages<Shared.OrderPlaced>
    {
        static ILog log = LogManager.GetLogger<OrderPlacedHandler>();

        public Task Handle(OrderPlaced message, IMessageHandlerContext context)
        {
            log.Info($"Order placed:{message.OrderId}");
            log.Info($"Sending: DeliverOrder for Order Id: {message.OrderId}");

            return context.Send(new DeliverOrder { OrderId = Guid.NewGuid() });
        }
    }
}