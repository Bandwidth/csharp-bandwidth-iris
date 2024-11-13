using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Bandwidth.Iris.Model;
using Xunit;

namespace Bandwidth.Iris.Tests.Models
{

    public class AvailableNumbersTests
    {
        // [TestInitialize]
        public AvailableNumbersTests()
        {
            Helper.SetEnvironmentVariables();
        }

        [Fact]
        public void ListTest()
        {
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/availableNumbers?areaCode=866&quantity=5", Helper.AccountId),
                ContentToSend = Helper.CreateXmlContent(new AvailableNumbersResult
                {
                    ResultCount = 2,
                    TelephoneNumberList = new[] { "1111", "2222" }
                })
            }))
            {
                var client = Helper.CreateClient();
                var result = AvailableNumbers.List(client, new Dictionary<string, object>
                {
                    {"areaCode", 866},
                    {"quantity", 5}
                }).Result;
                if (server.Error != null) throw server.Error;
                Assert.Equal(2, result.ResultCount);
                Assert.Equal(new[] { "1111", "2222" }, result.TelephoneNumberList);
            }
        }

        [Fact]
        public void ListWithXmlTest()
        {
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/availableNumbers?areaCode=866&quantity=5", Helper.AccountId),
                ContentToSend = new StringContent(TestXmlStrings.LocalAreaSearchResultXml, Encoding.UTF8, "application/xml")
            }))
            {
                var client = Helper.CreateClient();
                var result = AvailableNumbers.List(client, new Dictionary<string, object>
                {
                    {"areaCode", 866},
                    {"quantity", 5}
                }).Result;
                if (server.Error != null) throw server.Error;
                Assert.Equal(2, result.ResultCount);
                var d = result.TelephoneNumberDetailList[0];
                Assert.Equal("JERSEY CITY", d.City);
                Assert.Equal("224", d.Lata);
                Assert.Equal("JERSEYCITY", d.RateCenter);
                Assert.Equal("NJ", d.State);
                Assert.Equal("2012001555", d.TelephoneNumber);
                d = result.TelephoneNumberDetailList[1];
                Assert.Equal("JERSEY CITY", d.City);
                Assert.Equal("224", d.Lata);
                Assert.Equal("JERSEYCITY", d.RateCenter);
                Assert.Equal("NJ", d.State);
                Assert.Equal("123123123", d.TelephoneNumber);
            }
        }

        [Fact]
        public void ListWithDefaultClientTest()
        {
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/availableNumbers?areaCode=866&quantity=5", Helper.AccountId),
                ContentToSend = Helper.CreateXmlContent(new AvailableNumbersResult
                {
                    ResultCount = 3,
                    TelephoneNumberList = new[] { "1111", "2222", "3333" }
                })
            }))
            {
                var result = AvailableNumbers.List(new Dictionary<string, object>
                {
                    {"areaCode", 866},
                    {"quantity", 5}
                }).Result;
                if (server.Error != null) throw server.Error;
                Assert.Equal(3, result.ResultCount);
                Assert.Equal(new[] { "1111", "2222", "3333" }, result.TelephoneNumberList);
            }
        }
    }
}
