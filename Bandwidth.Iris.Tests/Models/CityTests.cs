using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Bandwidth.Iris.Model;
using Xunit;

namespace Bandwidth.Iris.Tests.Models
{

    public class CityTests
    {
        // [TestInitialize]
        public void Setup()
        {
            Helper.SetEnvironmetVariables();
        }

        [Fact]
        public void ListTest()
        {
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = string.Format("/v1.0/cities?state=NC", Helper.AccountId),
                ContentToSend = new StringContent(TestXmlStrings.CitiesResponse, Encoding.UTF8, "application/xml")
            }))
            {
                var client = Helper.CreateClient();
                var result = City.List(client, new Dictionary<string, object>
                {
                    {"state", "NC"}
                }).Result;
                if (server.Error != null) throw server.Error;
                Assert.Equal(2, result.Length);
                Assert.Equal("SOUTHEPINS", result[0].RcAbbreviation);
                Assert.Equal("ABERDEEN", result[0].Name);
                Assert.Equal("JULIAN", result[1].RcAbbreviation);
                Assert.Equal("ADVANCE", result[1].Name);
            }
        }

        [Fact]
        public void ListWithDefaultClientTest()
        {
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = string.Format("/v1.0/cities?state=NC", Helper.AccountId),
                ContentToSend = new StringContent(TestXmlStrings.CitiesResponse, Encoding.UTF8, "application/xml")
            }))
            {
                var result = City.List(new Dictionary<string, object>
                {
                    {"state", "NC"}
                }).Result;
                if (server.Error != null) throw server.Error;
                Assert.Equal(2, result.Length);
                Assert.Equal("SOUTHEPINS", result[0].RcAbbreviation);
                Assert.Equal("ABERDEEN", result[0].Name);
                Assert.Equal("JULIAN", result[1].RcAbbreviation);
                Assert.Equal("ADVANCE", result[1].Name);
            }
        }
    }
}
