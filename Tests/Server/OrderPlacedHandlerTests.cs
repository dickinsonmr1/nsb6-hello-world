using System;
using System.Linq;
using System.Threading.Tasks;
using Server;
using NServiceBus.Testing;
using NUnit.Framework;
using Shared;

namespace Tests.Server
{
    public static class OrderPlacedHandlerTests
    {
        [TestFixture]
        public class WhenHandlingOrderPlaced
        {
            private OrderPlaced message;
            private TestableMessageHandlerContext context;

            [OneTimeSetUp]
            public async Task Given()
            {
                message = new OrderPlaced { OrderId = Guid.NewGuid() };

                var sut = new OrderPlacedHandler();
                context = new TestableMessageHandlerContext();

                await sut.Handle(message, context);
            }

            [Test]
            public void SendsBillCustomer()
            {
                var result = (BillCustomer)context.SentMessages.Single().Message;
                Assert.That(result.OrderId, Is.EqualTo(message.OrderId));
            }
        }        
    }
}