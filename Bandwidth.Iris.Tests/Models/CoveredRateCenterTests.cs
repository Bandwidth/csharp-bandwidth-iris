using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Bandwidth.Iris.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bandwidth.Iris.Tests.Models
{
    [TestClass]
    public class RateCenterTests
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
                Assert.AreEqual(1, result.Length);
                Assert.AreEqual("ACME", result[0].Abbreviation);
                Assert.AreEqual("ACME", result[0].Name);
            }
        }

        [TestMethod]
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
                Assert.AreEqual(1, result.Length);
                Assert.AreEqual("ACME", result[0].Abbreviation);
                Assert.AreEqual("ACME", result[0].Name);
            }
        }
    }
}
