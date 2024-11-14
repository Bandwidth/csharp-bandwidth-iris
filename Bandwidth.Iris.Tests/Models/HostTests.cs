using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Bandwidth.Iris.Model;
using Xunit;

namespace Bandwidth.Iris.Tests.Models
{

    public class HostTests
    {
        // [TestInitialize]
        public HostTests()
        {
            Helper.SetEnvironmentVariables();
        }

        [Fact]
        public void ListTest()
        {
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/hosts", Helper.AccountId),
                ContentToSend = new StringContent(TestXmlStrings.Hosts, Encoding.UTF8, "application/xml")
            }))
            {
                var client = Helper.CreateClient();
                var result = Host.List(client).Result;
                if (server.Error != null) throw server.Error;
                Assert.Single(result);
                Assert.Single(result[0].SipPeerHosts[0].SmsHosts);
            }
        }
        [Fact]
        public void ListWithDefaultClientTest()
        {
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/hosts", Helper.AccountId),
                ContentToSend = new StringContent(TestXmlStrings.Hosts, Encoding.UTF8, "application/xml")
            }))
            {
                var result = Host.List().Result;
                if (server.Error != null) throw server.Error;
                Assert.Single(result);
                Assert.Single(result[0].SipPeerHosts[0].SmsHosts);
            }
        }


    }
}
