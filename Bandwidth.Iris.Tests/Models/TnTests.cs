using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Bandwidth.Iris.Model;
using Xunit;

namespace Bandwidth.Iris.Tests.Models
{
    
    public class TnTests
    {
        // [TestInitialize]
        public void Setup()
        {
            Helper.SetEnvironmetVariables();
        }

        [Fact]
        public void GetTest()
        {

            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = string.Format("/v1.0/tns/1234", Helper.AccountId),
                ContentToSend = new StringContent(TestXmlStrings.TnResponse, Encoding.UTF8, "application/xml")
            }))
            {
                var client = Helper.CreateClient();
                var result = Tn.Get(client, "1234").Result;
                if (server.Error != null) throw server.Error;
                Assert.AreEqual("1234", result.TelephoneNumber);
                Assert.AreEqual("Inservice", result.Status);
                Assert.AreEqual("5f3a4dab-aac7-4b0a-8ee4-1b6a67ae04be", result.OrderId);
                Assert.AreEqual("NEW_NUMBER_ORDER", result.OrderType);
                
            }
        }

        [Fact]
        public void ListTest()
        {
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = string.Format("/v1.0/tns?npa=818", Helper.AccountId),
                ContentToSend = new StringContent(TestXmlStrings.TnsListResponse, Encoding.UTF8, "application/xml")
            }))
            {
                var client = Helper.CreateClient();
                var result = Tn.List(client, new Dictionary<string, object>{{"npa", "818"}}).Result;
                if (server.Error != null) throw server.Error;
                Assert.AreEqual(5, result.TelephoneNumberCount);

            }
            
        }

        [Fact]
        public void ListWithDefaultClientTest()
        {
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = string.Format("/v1.0/tns?npa=818", Helper.AccountId),
                ContentToSend = new StringContent(TestXmlStrings.TnsListResponse, Encoding.UTF8, "application/xml")
            }))
            {
                var result = Tn.List(new Dictionary<string, object> { { "npa", "818" } }).Result;
                if (server.Error != null) throw server.Error;
                Assert.AreEqual(5, result.TelephoneNumberCount);

            }
            
        }

        [Fact]
        public void GetWithDefaultClientTest()
        {

            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = string.Format("/v1.0/tns/1234", Helper.AccountId),
                ContentToSend = new StringContent(TestXmlStrings.TnResponse, Encoding.UTF8, "application/xml")
            }))
            {
                var result = Tn.Get("1234").Result;
                if (server.Error != null) throw server.Error;
                Assert.AreEqual("1234", result.TelephoneNumber);
                Assert.AreEqual("Inservice", result.Status);
                Assert.AreEqual("5f3a4dab-aac7-4b0a-8ee4-1b6a67ae04be", result.OrderId);
                Assert.AreEqual("NEW_NUMBER_ORDER", result.OrderType);

            }
        }

        [Fact]
        public void GetSitesTest()
        {

            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = string.Format("/v1.0/tns/1234/sites", Helper.AccountId),
                ContentToSend = new StringContent(TestXmlStrings.TnSitesResponse, Encoding.UTF8, "application/xml")
            }))
            {
                var client = Helper.CreateClient();
                var tn = new Tn {TelephoneNumber = "1234", Client = client};
                var result = tn.GetSites().Result;
                if (server.Error != null) throw server.Error;
                Assert.AreEqual("1435", result.Id);
                Assert.AreEqual("Sales Training", result.Name);

            }
        }

        [Fact]
        public void GetSipPeersTest()
        {

            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = string.Format("/v1.0/tns/1234/sippeers", Helper.AccountId),
                ContentToSend = new StringContent(TestXmlStrings.TnSipPeersResponse, Encoding.UTF8, "application/xml")
            }))
            {
                var client = Helper.CreateClient();
                var tn = new Tn { TelephoneNumber = "1234", Client = client };
                var result = tn.GetSipPeers().Result;
                if (server.Error != null) throw server.Error;
                Assert.AreEqual("4064", result.Id);
                Assert.AreEqual("Sales", result.Name);

            }
        }

        [Fact]
        public void GetRateCenterTest()
        {

            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = string.Format("/v1.0/tns/1234/ratecenter", Helper.AccountId),
                ContentToSend = new StringContent(TestXmlStrings.TnRateCenterResponse, Encoding.UTF8, "application/xml")
            }))
            {
                var client = Helper.CreateClient();
                var tn = new Tn { TelephoneNumber = "1234", Client = client };
                var result = tn.GetRateCenter().Result;
                if (server.Error != null) throw server.Error;
                Assert.AreEqual("CO", result.State);
                Assert.AreEqual("DENVER", result.RateCenter);

            }
        }

        [Fact]
        public void GetLataTest()
        {

            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = string.Format("/v1.0/tns/1234/lata", Helper.AccountId),
                ContentToSend = new StringContent(TestXmlStrings.TnLataResponse, Encoding.UTF8, "application/xml")
            }))
            {
                var client = Helper.CreateClient();
                var tn = new Tn { TelephoneNumber = "1234", Client = client };
                var result = tn.GetLata().Result;
                if (server.Error != null) throw server.Error;
                Assert.AreEqual("656", result);
            }
        }

        [Fact]
        public void GetDetailsTest()
        {

            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = string.Format("/v1.0/tns/1234/tndetails", Helper.AccountId),
                ContentToSend = new StringContent(TestXmlStrings.TnDetailsResponse, Encoding.UTF8, "application/xml")
            }))
            {
                var client = Helper.CreateClient();
                var tn = new Tn { TelephoneNumber = "1234", Client = client };
                var result = tn.GetDetails().Result;
                if (server.Error != null) throw server.Error;
                Assert.AreEqual("9500149", result.AccountId);
                Assert.AreEqual("DENVER", result.City);
                Assert.AreEqual("656", result.Lata);
                Assert.AreEqual("CO", result.State);
                Assert.AreEqual("1234", result.FullNumber);
                Assert.AreEqual("0", result.Tier);
                Assert.AreEqual("49", result.VendorId);
                Assert.AreEqual("9500149", result.AccountId);
            }
        }
    }
}
