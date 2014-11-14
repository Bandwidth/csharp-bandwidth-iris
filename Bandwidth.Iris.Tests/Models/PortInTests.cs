using Bandwidth.Iris.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bandwidth.Iris.Tests.Models
{
    [TestClass]
    public class PortInTests
    {
        [TestInitialize]
        public void Setup()
        {
            Helper.SetEnvironmetVariables();
        }

        [TestMethod]
        public void CreateOrderTest()
        {
            var order = new LnpOrder
            {
                BillingTelephoneNumber = "1111",
                Subscriber = new Subscriber
                {
                   SubscriberType = "BUSINESS",
                   BusinessName = "Company",
                   ServiceAddress = new Address
                   {
                       City = "City",
                       StateCode = "State",
                       Country = "Country"
                   }
                },
                SiteId = "1"                 
            };
            
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "POST",
                EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/portins", Helper.AccountId),
                EstimatedContent = Helper.ToXmlString(order)
            }))
            {
                var client = Helper.CreateClient();
                PortIn.CreateOrder(client, order).Wait();
                if (server.Error != null) throw server.Error;
            }
        }

        [TestMethod]
        public void CreateOrderWithDefaultClientTest()
        {
            var order = new LnpOrder
            {
                BillingTelephoneNumber = "1111",
                Subscriber = new Subscriber
                {
                    SubscriberType = "BUSINESS",
                    BusinessName = "Company",
                    ServiceAddress = new Address
                    {
                        City = "City",
                        StateCode = "State",
                        Country = "Country"
                    }
                },
                SiteId = "1"
            };

            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "POST",
                EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/portins", Helper.AccountId),
                EstimatedContent = Helper.ToXmlString(order)
            }))
            {
                PortIn.CreateOrder(order).Wait();
                if (server.Error != null) throw server.Error;
            }
        }
    }
}
