using System;
using System.Linq;
using System.Threading.Tasks;
using Billing;
using NServiceBus.Testing;
using NUnit.Framework;
using Shared;

namespace Tests.Billing
{
    public static class BillingSagaTests
    {        
        [TestFixture]
        public class WhenHandlingBillCustomerAndOrderDelivered
        {
            private readonly Guid orderId = Guid.NewGuid();
            private BillingSaga saga;
            private TestableMessageHandlerContext context;

            [OneTimeSetUp]
            public async Task Given()
            {
                saga = new BillingSaga { Data = new SagaData() };

                context = new TestableMessageHandlerContext();
                var billCustomer = new BillCustomer { OrderId = orderId };
                var orderDelivered = new OrderDelivered { OrderId = orderId };

                await saga.Handle(billCustomer, context);
                await saga.Handle(orderDelivered, context);
            }

            [Test]
            public void PublishesCustomerBilled()
            {
                var result = (CustomerBilled)context.PublishedMessages.Single().Message;
                Assert.That(result.OrderId, Is.EqualTo(orderId));
            }

            [Test]
            public void MarksSagaAsComplete()
            {                
                Assert.That(saga.Completed);
            }
        }
    }
}