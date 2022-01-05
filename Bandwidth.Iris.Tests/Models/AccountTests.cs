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
            // Helper.SetEnvironmetVariables();
            Environment.SetEnvironmentVariable(Client.BandwidthApiUserName, UserName);
            Environment.SetEnvironmentVariable(Client.BandwidthApiPassword, Password);
            Environment.SetEnvironmentVariable(Client.BandwidthApiAccountId, AccountId);
            Environment.SetEnvironmentVariable(Client.BandwidthApiEndpoint, baseUrl ?? "http://localhost:3001/");
            Environment.SetEnvironmentVariable(Client.BandwidthApiVersion, "v1.0");
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
                Assert.AreEqual("14", result.Id);
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
                Assert.AreEqual("14", result.Id);
            }
        }
    }
}
