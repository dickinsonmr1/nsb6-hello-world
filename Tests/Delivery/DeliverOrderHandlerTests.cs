using System;
using System.Linq;
using System.Threading.Tasks;
using Delivery;
using NServiceBus.Testing;
using NUnit.Framework;
using Shared;

namespace Tests.Delivery
{
    public static class DeliverOrderHandlerTests
    {
        [TestFixture]
        public class WhenHandlingDeliverOrder
        {
            private DeliverOrder message;
            private TestableMessageHandlerContext context;

            [OneTimeSetUp]
            public async Task Given()
            {
                message = new DeliverOrder { OrderId = Guid.NewGuid() };

                var sut = new DeliverOrderHandler();
                context = new TestableMessageHandlerContext();

                await sut.Handle(message, context);
            }

            [Test]
            public void PublishesOrderDelivered()
            {                
                var result = (OrderDelivered)context.PublishedMessages.Single().Message;
                Assert.That(result.OrderId, Is.EqualTo(message.OrderId));
            }
        }
    }
}