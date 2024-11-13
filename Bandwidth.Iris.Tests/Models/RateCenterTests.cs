using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Bandwidth.Iris.Model;
using Xunit;

namespace Bandwidth.Iris.Tests.Models
{

    public class CoveredRateCenterTests
    {
        // [TestInitialize]
        public CoveredRateCenterTests()
        {
            Helper.SetEnvironmentVariables();
        }

        [Fact]
        public void ListTest()
        {
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = string.Format("/v1.0/rateCenters?state=NC", Helper.AccountId),
                ContentToSend = new StringContent(TestXmlStrings.RateCentersResponse, Encoding.UTF8, "application/xml")
            }))
            {
                var client = Helper.CreateClient();
                var result = RateCenter.List(client, new Dictionary<string, object>
                {
                    {"state", "NC"}
                }).Result;
                if (server.Error != null) throw server.Error;
                Assert.Equal(3, result.Length);
                Assert.Equal("ACME", result[0].Abbreviation);
                Assert.Equal("ACME", result[0].Name);
                Assert.Equal("AHOSKIE", result[1].Abbreviation);
                Assert.Equal("AHOSKIE", result[1].Name);
                Assert.Equal("ALBEMARLE", result[2].Abbreviation);
                Assert.Equal("ALBEMARLE", result[2].Name);
            }
        }

        [Fact]
        public void ListWithDefaultClientTest()
        {
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = string.Format("/v1.0/rateCenters?state=NC", Helper.AccountId),
                ContentToSend = new StringContent(TestXmlStrings.RateCentersResponse, Encoding.UTF8, "application/xml")
            }))
            {
                var result = RateCenter.List(new Dictionary<string, object>
                {
                    {"state", "NC"}
                }).Result;
                if (server.Error != null) throw server.Error;
                Assert.Equal(3, result.Length);
                Assert.Equal("ACME", result[0].Abbreviation);
                Assert.Equal("ACME", result[0].Name);
                Assert.Equal("AHOSKIE", result[1].Abbreviation);
                Assert.Equal("AHOSKIE", result[1].Name);
                Assert.Equal("ALBEMARLE", result[2].Abbreviation);
                Assert.Equal("ALBEMARLE", result[2].Name);
            }
        }
    }
}
