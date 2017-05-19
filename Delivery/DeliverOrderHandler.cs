using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;
using Shared;

namespace Delivery
{
    public class DeliverOrderHandler : IHandleMessages<Shared.DeliverOrder>
    {
        static ILog log = LogManager.GetLogger<DeliverOrderHandler>();
        public Task Handle(DeliverOrder message, IMessageHandlerContext context)
        {
            log.Info($"@@@ DELIVERING ORDER: {message.OrderId} @@@");
            log.Info($"Publishing: OrderDelivered for Order Id: {message.OrderId}");

            var orderPlaced = new OrderDelivered() { OrderId = message.OrderId };
            return context.Publish(orderPlaced);
        }
    }
}