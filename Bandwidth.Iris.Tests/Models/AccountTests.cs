using System.Collections.Generic;
using Bandwidth.Iris.Model;
using Xunit;
using System.Net.Http;
using System.Text;

namespace Bandwidth.Iris.Tests.Models
{

    public class AccountTests
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
                EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}", Helper.AccountId),
                ContentToSend = new StringContent(TestXmlStrings.AccountResponse, Encoding.UTF8, "application/xml")
            }))
            {
                var client = Helper.CreateClient();
                var result = Account.Get(client).Result;
                if (server.Error != null) throw server.Error;
                Assert.Equal("14", result.Id);
            }
        }

        [Fact]
        public void GetWithDefaultClientTest()
        {
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}", Helper.AccountId),
                ContentToSend = new StringContent(TestXmlStrings.AccountResponse, Encoding.UTF8, "application/xml")
            }))
            {
                var result = Account.Get().Result;
                if (server.Error != null) throw server.Error;
                Assert.Equal("14", result.Id);
            }
        }
    }
}
