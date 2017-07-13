using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;
using Shared;

namespace Billing
{
    public class CustomerBilledHandler : IHandleMessages<CustomerBilled>
    {
        static ILog log = LogManager.GetLogger<BillingSaga>();

        public Task Handle(CustomerBilled message, IMessageHandlerContext context)
        {
            log.Info($"Customer billed and order delivered for Order Id: {message.OrderId}");

            return Task.CompletedTask;
        }
    }
}