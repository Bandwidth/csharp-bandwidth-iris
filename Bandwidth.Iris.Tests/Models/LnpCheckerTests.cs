using System;
using System.Net.Http;
using System.Text;
using Bandwidth.Iris.Model;
using Xunit;

namespace Bandwidth.Iris.Tests.Models
{

    public class LnpCheckerTests
    {
        // [TestInitialize]
        public LnpCheckerTests()
        {
            Helper.SetEnvironmentVariables();
        }

        [Fact]
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

        [Fact]
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
                Assert.Equal(new[] { "9195551212", "9195551213" }, result.PortableNumbers);
                Assert.Equal("NC", result.SupportedRateCenters[0].State);
                Assert.Equal(new[] { "9195551212", "9195551213" }, result.SupportedLosingCarriers.LosingCarrierTnList.TnList);

            }
        }

        [Fact]
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

        [Fact]
        public void LnpCheckerPortabilityErrorsTest()
        {
            var xml = TestXmlStrings.xmlNumberPortabilityResponseWithPortabilityErrros;

            NumberPortabilityResponse numberPortabilityResponse = Helper.ParseXml<NumberPortabilityResponse>(xml);


            Assert.Equal(2, numberPortabilityResponse.PortabilityErrors.Errors.Length);
            Assert.Equal("7378", numberPortabilityResponse.PortabilityErrors.Errors[0].Code);
            Assert.Equal("test description", numberPortabilityResponse.PortabilityErrors.Errors[0].Description);
            Assert.Equal(2, numberPortabilityResponse.PortabilityErrors.Errors[0].TelephoneNumbers.Length);
            Assert.Equal("9199199999", numberPortabilityResponse.PortabilityErrors.Errors[0].TelephoneNumbers[0]);

        }

    }
}
