using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Bandwidth.Iris.Model;
using Xunit;

namespace Bandwidth.Iris.Tests.Models
{

    public class RateCenterTests
    {
        // [TestInitialize]
        public RateCenterTests()
        {
            Helper.SetEnvironmetVariables();
        }

        [Fact]
        public void ListTest()
        {
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = string.Format("/v1.0/coveredRateCenters?state=NC", Helper.AccountId),
                ContentToSend = new StringContent(TestXmlStrings.CoveredRateCentersResponse, Encoding.UTF8, "application/xml")
            }))
            {
                var client = Helper.CreateClient();
                var result = CoveredRateCenter.List(client, new Dictionary<string, object>
                {
                    {"state", "NC"}
                }).Result;
                if (server.Error != null) throw server.Error;
                Assert.Equal(1, result.Length);
                Assert.Equal("ACME", result[0].Abbreviation);
                Assert.Equal("ACME", result[0].Name);
            }
        }

        [Fact]
        public void ListWithDefaultClientTest()
        {
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = string.Format("/v1.0/coveredRateCenters?state=NC", Helper.AccountId),
                ContentToSend = new StringContent(TestXmlStrings.CoveredRateCentersResponse, Encoding.UTF8, "application/xml")
            }))
            {
                var result = CoveredRateCenter.List(new Dictionary<string, object>
                {
                    {"state", "NC"}
                }).Result;
                if (server.Error != null) throw server.Error;
                Assert.Equal(1, result.Length);
                Assert.Equal("ACME", result[0].Abbreviation);
                Assert.Equal("ACME", result[0].Name);
            }
        }
    }
}
