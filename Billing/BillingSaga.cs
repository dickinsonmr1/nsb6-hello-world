using System;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;
using Shared;

namespace Billing
{
    public class BillingSaga: Saga<SagaData>, IAmStartedByMessages<Shared.BillCustomer>, IHandleMessages<OrderDelivered>
    {


        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<SagaData> mapper)
        {
            mapper.ConfigureMapping<BillCustomer>(message => message.OrderId).ToSaga(sagaData => sagaData.OrderId);
            mapper.ConfigureMapping<OrderDelivered>(message => message.OrderId).ToSaga(sagaData => sagaData.OrderId);
        }

        public async Task Handle(BillCustomer message, IMessageHandlerContext context)
        {
            Data.OrderId = message.OrderId;
            Data.BillCustomerReceived = true;

            await CheckForAllMessagesReceivedAndComplete(context);            
        }

        public async Task Handle(OrderDelivered message, IMessageHandlerContext context)
        {
            Data.OrderDeliveredReceived = true;

            await CheckForAllMessagesReceivedAndComplete(context);
        }

        private async Task CheckForAllMessagesReceivedAndComplete(IMessageHandlerContext context)
        {
            var allMessagesReceived = Data.BillCustomerReceived && Data.OrderDeliveredReceived;

            if (!allMessagesReceived)
                return;

            await context.Publish(new CustomerBilled {OrderId = Data.OrderId});
            MarkAsComplete();            
        }
    }

    public class SagaData : ContainSagaData
    {
        public Guid OrderId { get; set; }
        public bool BillCustomerReceived { get; set; }
        public bool OrderDeliveredReceived { get; set; }
    }
}