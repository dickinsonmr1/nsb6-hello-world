using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;
using Shared;

namespace Server
{
    public class PlaceOrderHandler : IHandleMessages<PlaceOrder>
    {
        static ILog log = LogManager.GetLogger<PlaceOrderHandler>();

        public Task Handle(PlaceOrder message, IMessageHandlerContext context)
        {
            log.Info($"Order for Product:{message.Product} placed with id: {message.OrderId}");
            log.Info($"Publishing: OrderPlaced for Order Id: {message.OrderId}");

            var orderPlaced = new OrderPlaced { OrderId = message.OrderId };
            return context.Publish(orderPlaced);
        }
    }
}