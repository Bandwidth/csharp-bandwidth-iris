using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Bandwidth.Iris.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bandwidth.Iris.Tests.Models
{
    [TestClass]
    public class HostTests
    {
        [TestInitialize]
        public void Setup()
        {
            Helper.SetEnvironmetVariables();
        }
        
        [TestMethod]
        public void ListTest()
        {
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/hosts", Helper.AccountId),
                ContentToSend = new  StringContent(TestXmlStrings.Hosts, Encoding.UTF8, "application/xml")
            }))
            {
                var client = Helper.CreateClient();
                var result = Host.List(client).Result;
                if (server.Error != null) throw server.Error;
                Assert.AreEqual(1, result.Length);
                Assert.AreEqual(1, result[0].SipPeerHosts[0].SmsHosts.Length);
            }
        }
        [TestMethod]
        public void ListWithDefaultClientTest()
        {
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/hosts", Helper.AccountId),
                ContentToSend = new  StringContent(TestXmlStrings.Hosts, Encoding.UTF8, "application/xml")
            }))
            {
                var result = Host.List().Result;
                if (server.Error != null) throw server.Error;
                Assert.AreEqual(1, result.Length);
                Assert.AreEqual(1, result[0].SipPeerHosts[0].SmsHosts.Length);
            }
        }

        
    }
}
