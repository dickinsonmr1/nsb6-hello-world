using System;
using System.Linq;
using System.Threading.Tasks;
using Server;
using NServiceBus.Testing;
using NUnit.Framework;
using Shared;

namespace Tests.Server
{
    public static class PlaceOrderHandlerTests
    {
        [TestFixture]
        public class WhenHandlingDeliverOrder
        {
            private PlaceOrder message;
            private TestableMessageHandlerContext context;

            [OneTimeSetUp]
            public async Task Given()
            {
                message = new PlaceOrder { OrderId = Guid.NewGuid() };

                var sut = new PlaceOrderHandler();
                context = new TestableMessageHandlerContext();

                await sut.Handle(message, context);
            }

            [Test]
            public void PublishesOrderPlaced()
            {
                var result = (OrderPlaced)context.PublishedMessages.Single().Message;
                Assert.That(result.OrderId, Is.EqualTo(message.OrderId));
            }
        }
    }
}