using System.Net.Http;
using System.Text;
using Bandwidth.Iris.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bandwidth.Iris.Tests.Models
{
    [TestClass]
    public class LnpCheckerTests
    {
        [TestInitialize]
        public void Setup()
        {
            Helper.SetEnvironmetVariables();
        }

        [TestMethod]
        public void CheckTest()
        {
            var request = new NumberPortabilityRequest
            {
                TnList = new[] { "1111", "2222", "3333" }
            };
            var response = new NumberPortabilityResponse
            {
                SupportedRateCenters = new[]
                {
                    new RateCenterGroup
                    {
                        RateCenter = "Center1",
                        City = "City1",
                        State = "State1",
                        Lata = "11",
                        Tiers = new []{"111", "222", "333"},
                        TnList = new []{"1111", "2222", "3333"}
                    }, 
                },
                UnsupportedRateCenters = new RateCenterGroup[0]
            };
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "POST",
                EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/lnpchecker?fullCheck=true", Helper.AccountId),
                EstimatedContent = Helper.ToXmlString(request),
                ContentToSend = Helper.CreateXmlContent(response)
            }))
            {
                var client = Helper.CreateClient();
                var result = LnpChecker.Check(client, new[] { "1111", "2222", "3333" }, true).Result;
                if (server.Error != null) throw server.Error;
                Helper.AssertObjects(response, result);
            }
        }

        [TestMethod]
        public void CheckWithXmlTest()
        {
            var request = new NumberPortabilityRequest
            {
                TnList = new[] { "1111", "2222", "3333" }
            };
            
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "POST",
                EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/lnpchecker?fullCheck=true", Helper.AccountId),
                EstimatedContent = Helper.ToXmlString(request),
                ContentToSend = new StringContent(TestXmlStrings.LnpCheckResponse, Encoding.UTF8, "application/xml")
            }))
            {
                var client = Helper.CreateClient();
                var result = LnpChecker.Check(client, new[] { "1111", "2222", "3333" }, true).Result;
                if (server.Error != null) throw server.Error;
                CollectionAssert.AreEqual(new[] { "9195551212", "9195551213" }, result.PortableNumbers);
                Assert.AreEqual("NC", result.SupportedRateCenters[0].State);
                CollectionAssert.AreEqual(new[] { "9195551212", "9195551213" }, result.SupportedLosingCarriers.LosingCarrierTnList.TnList);

            }
        }

        [TestMethod]
        public void CheckWithDefaultClientTest()
        {
            var request = new NumberPortabilityRequest
            {
                TnList = new[] { "1111", "2222", "3333" }
            };
            var response = new NumberPortabilityResponse
            {
                SupportedRateCenters = new[]
                {
                    new RateCenterGroup
                    {
                        RateCenter = "Center1",
                        City = "City1",
                        State = "State1",
                        Lata = "11",
                        Tiers = new []{"111", "222", "333"},
                        TnList = new []{"1111", "2222", "3333"}
                    }, 
                },
                UnsupportedRateCenters = new RateCenterGroup[0]
            };
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "POST",
                EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/lnpchecker?fullCheck=false", Helper.AccountId),
                EstimatedContent = Helper.ToXmlString(request),
                ContentToSend = Helper.CreateXmlContent(response)
            }))
            {
                var result = LnpChecker.Check(new[] { "1111", "2222", "3333" }).Result;
                if (server.Error != null) throw server.Error;
                Helper.AssertObjects(response, result);
            }
        }
    }
}
