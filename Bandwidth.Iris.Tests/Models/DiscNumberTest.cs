using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Bandwidth.Iris.Model;
using Xunit;

namespace Bandwidth.Iris.Tests.Models
{

    public class DiscNumberTests
    {
        // [TestInitialize]
        public DiscNumberTests()
        {
            Helper.SetEnvironmetVariables();
        }

        [Fact]
        public void ListTest()
        {
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/discnumbers", Helper.AccountId),
                ContentToSend = new StringContent(TestXmlStrings.DiscNumberResponse, Encoding.UTF8, "application/xml")
            }))
            {
                var client = Helper.CreateClient();
                var result = DiscNumber.List(client).Result;
                if (server.Error != null) throw server.Error;
                Assert.Equal(2, result.Length);
            }
        }

        [Fact]
        public void ListWithDefaultClientTest()
        {
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/discnumbers", Helper.AccountId),
                ContentToSend = new StringContent(TestXmlStrings.DiscNumberResponse, Encoding.UTF8, "application/xml")
            }))
            {
                var result = DiscNumber.List().Result;
                if (server.Error != null) throw server.Error;
                Assert.Equal(2, result.Length);
            }
        }

        [Fact]
        public void GetTotalsTest()
        {
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/discnumbers/totals", Helper.AccountId),
                ContentToSend = new StringContent(TestXmlStrings.InServiceNumbersTotals, Encoding.UTF8, "application/xml")
            }))
            {
                var client = Helper.CreateClient();
                var result = DiscNumber.GetTotals(client).Result;
                if (server.Error != null) throw server.Error;
                Assert.Equal(3, result.Count);
            }
        }
    }
}
