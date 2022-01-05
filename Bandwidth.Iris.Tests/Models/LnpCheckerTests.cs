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
            Helper.SetEnvironmetVariables();
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
        public void CheckThrowsErrorTest()
        {
            var request = new NumberPortabilityRequest
            {
                TnList = new[] { "1111" }
            };
            var response = new BandwidthIrisException(
                "170",
                "error thrown",
                System.Net.HttpStatusCode.ExpectationFailed
            ) ;
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "POST",
                EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/lnpchecker?fullCheck=true", Helper.AccountId),
                EstimatedContent = Helper.ToXmlString(request),
                ContentToSend = new StringContent("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?><NumberPortabilityResponse><PortableNumbers><Tn>9192202164</Tn><Tn>9197891146</Tn></PortableNumbers><PortabilityErrors><Error><Code>7331</Code><Description>Rate Center Not Present in Bandwidth Dashboard</Description><TelephoneNumbers><Tn>5555555555</Tn></TelephoneNumbers></Error></PortabilityErrors><SupportedRateCenters><RateCenterGroup><RateCenter>DURHAM</RateCenter><City>DURHAM</City><State>NC</State><LATA>426</LATA><Tiers><Tier>0</Tier></Tiers><TnList><Tn>9192202164</Tn></TnList></RateCenterGroup><RateCenterGroup><RateCenter>RALEIGH</RateCenter><City>RALEIGH</City><State>NC</State><LATA>426</LATA><Tiers><Tier>0</Tier></Tiers><TnList><Tn>9197891146</Tn></TnList></RateCenterGroup></SupportedRateCenters><UnsupportedRateCenters/></NumberPortabilityResponse>", Encoding.UTF8, "application/xml")

        }))
            {
                var client = Helper.CreateClient();
                try
                {
                    var result = LnpChecker.Check(client, new[] { "1111" }, true).Result;
                } catch (BandwidthIrisException e )
                {
                    return;
                }
                catch (AggregateException e)
                {
                    Exception innerEx = e;
                    while(innerEx != null)
                    {
                        string mesg = innerEx.Message;
                        innerEx = innerEx.InnerException;
                        Console.WriteLine(mesg);
                    }
                    return;
                }
                 catch (Exception e)
                {
                    return;
                }
                Assert.True(false, "The exception was not thrown");
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
