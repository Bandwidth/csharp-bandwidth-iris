using System.Net.Http;
using System.Text;
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
        public void CreateTest()
        {
            var order = new LnpOrderResponse
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
                EstimatedContent = Helper.ToXmlString(order),
                ContentToSend = Helper.CreateXmlContent(order)
            }))
            {
                var client = Helper.CreateClient();
                var r = PortIn.Create(client, order).Result;
                if (server.Error != null) throw server.Error;
                Helper.AssertObjects(order, r);
            }
        }

        [TestMethod]
        public void CreateWithXmlTest()
        {
            var order = new PortIn
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
                EstimatedContent = Helper.ToXmlString(order),
                ContentToSend = new StringContent(TestXmlStrings.ValidCreatePostInResponse, Encoding.UTF8, "application/xml")
            }))
            {
                var client = Helper.CreateClient();
                var r = PortIn.Create(client, order).Result;
                if (server.Error != null) throw server.Error;
                Assert.AreEqual("d28b36f7-fa96-49eb-9556-a40fca49f7c6", r.Id);
                Assert.AreEqual("201", r.Status.Code);
                Assert.AreEqual("Order request received. Please use the order id to check the status of your order later.", r.Status.Description);
                Assert.AreEqual("PENDING_DOCUMENTS", r.ProcessingStatus);
                Assert.AreEqual("John Doe", r.LoaAuthorizingPerson);
                Assert.AreEqual("6882015002", r.BillingTelephoneNumber);
                CollectionAssert.AreEqual(new[] { "6882015025", "6882015026" }, r.ListOfPhoneNumbers);
                Assert.IsFalse(r.Triggered);
                Assert.AreEqual("PORTIN", r.BillingType);
                
            }
        }
        [TestMethod]
        public void CreateWithDefaultClientTest()
        {
            var order = new LnpOrderResponse
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
                EstimatedContent = Helper.ToXmlString(order),
                ContentToSend = Helper.CreateXmlContent(order)
            }))
            {
                var r = PortIn.Create(order).Result;
                if (server.Error != null) throw server.Error;
                Helper.AssertObjects(order, r);
            }
        }
    }
}
