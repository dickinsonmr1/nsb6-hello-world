using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;
using Shared;

namespace Subscriber
{
    public class OrderPlacedHandler : IHandleMessages<OrderPlaced>
    {
        private static ILog log = LogManager.GetLogger<OrderPlacedHandler>();

        public Task Handle(OrderPlaced message, IMessageHandlerContext context)
        {
            log.Info($"Handling: OrderPlaced for Order Id: {message.OrderId}");
            return Task.CompletedTask;
        }
    }
}