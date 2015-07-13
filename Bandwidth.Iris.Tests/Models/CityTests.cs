using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Bandwidth.Iris.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bandwidth.Iris.Tests.Models
{
    [TestClass]
    public class CityTests
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
                Assert.AreEqual(2, result.Length);
                Assert.AreEqual("SOUTHEPINS", result[0].RcAbbreviation);
                Assert.AreEqual("ABERDEEN", result[0].Name);
                Assert.AreEqual("JULIAN", result[1].RcAbbreviation);
                Assert.AreEqual("ADVANCE", result[1].Name);
            }
        }

        [TestMethod]
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
                Assert.AreEqual(2, result.Length);
                Assert.AreEqual("SOUTHEPINS", result[0].RcAbbreviation);
                Assert.AreEqual("ABERDEEN", result[0].Name);
                Assert.AreEqual("JULIAN", result[1].RcAbbreviation);
                Assert.AreEqual("ADVANCE", result[1].Name);
            }
        }
    }
}
