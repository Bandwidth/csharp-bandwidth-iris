using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Bandwidth.Iris.Model;
using Xunit;

namespace Bandwidth.Iris.Tests.Models
{

    public class ApplicationTest
    {

        // [TestInitialize]
        public ApplicationTest()
        {
            Helper.SetEnvironmentVariables();

        }

        [Fact]
        public void TestApplicationProvisioningResponse()
        {

            var apresponse = new ApplicationProvisioningResponse
            {
                ApplicationList = new Application[] { new Application
                    {
                        ApplicationId = "2cfcb382-161c-46d4-8c67-87ca09a72c85",
                        ServiceType = "Messaging-V2",
                        AppName = "app1",
                        MsgCallbackUrl = "http://a.com"

                    },
                    new Application
                    {
                        ApplicationId = "0cb0112b-5998-4c81-999a-0d3fb5e3f8e2",
                        ServiceType = "Voice-V2",
                        AppName = "app2",
                        MsgCallbackUrl = "http://b.com",
                        CallbackCreds = new CallbackCreds
                        {
                            UserId = "15jPWZmXdm"
                        }

                    }
                }
            };

            var actual = Helper.ToXmlStringMinified(apresponse);

            //Linerize the XML
            XDocument doc = XDocument.Parse(TestXmlStrings.multiApplicationProvisionResponse);
            var xmlExpected = doc.ToString(SaveOptions.DisableFormatting);


            Assert.Equal(xmlExpected, actual);


            apresponse = new ApplicationProvisioningResponse
            {
                Application = new Application
                {
                    ApplicationId = "d3e418e9-1833-49c1-b6c7-ca1700f79586",
                    ServiceType = "Voice-V2",
                    AppName = "v1",
                    CallInitiatedMethod = "GET",
                    CallInitiatedCallbackUrl = "https://a.com",
                    CallStatusCallbackUrl = "https://b.com",
                    CallStatusMethod = "GET",
                    CallbackCreds = new CallbackCreds
                    {
                        UserId = "login123"
                    }

                }
            };

            actual = Helper.ToXmlStringMinified(apresponse);
            xmlExpected = XDocument.Parse(TestXmlStrings.singleApplicationProvisionResponse).ToString(SaveOptions.DisableFormatting);

            Assert.Equal(xmlExpected, actual);

        }

        [Fact]
        public void TestAssociatedSipPeersResponse()
        {

            AssociatedSipPeersResponse response = new AssociatedSipPeersResponse
            {
                AssociatedSipPeers = new AssociatedSipPeer[]
                 {
                     new AssociatedSipPeer
                     {
                         SiteId = "13651",
                         SiteName = "Prod Sub-account",
                         PeerId = "540341",
                         PeerName = "Prod"
                     },
                     new AssociatedSipPeer
                     {
                         SiteId = "13622",
                         SiteName = "Dev Sub-zccount",
                         PeerId = "540349",
                         PeerName = "Dev"
                     }

                 }
            };

            var actual = Helper.ToXmlStringMinified(response);

            //Linerize the XML
            XDocument doc = XDocument.Parse(TestXmlStrings.associatedSipPeerResponse);
            var expected = doc.ToString(SaveOptions.DisableFormatting);

            Assert.Equal(expected, actual);

        }

        [Fact]
        public void TestListApplications()
        {

            string strResponse = XDocument.Parse(TestXmlStrings.multiApplicationProvisionResponse).ToString(SaveOptions.DisableFormatting);

            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = $"/v1.0/accounts/{Helper.AccountId}/applications",
                ContentToSend = new StringContent(strResponse, Encoding.UTF8, "application/xml")
            }))


            {
                var client = Helper.CreateClient();
                var t = Application.List(client);
                var r = t.Result;
                if (server.Error != null) throw server.Error;

                Assert.Equal(2, r.ApplicationList.Length);

                Assert.Equal("2cfcb382-161c-46d4-8c67-87ca09a72c85", r.ApplicationList[0].ApplicationId);
                Assert.Equal("Messaging-V2", r.ApplicationList[0].ServiceType);
                Assert.Equal("app1", r.ApplicationList[0].AppName);
                Assert.Equal("http://a.com", r.ApplicationList[0].MsgCallbackUrl);


            }
        }

        [Fact]
        public void TestCreateApplication()
        {

            var application = new Application
            {
                ApplicationId = "d3e418e9-1833-49c1-b6c7-ca1700f79586",
                ServiceType = "Voice-V2",
                AppName = "v1",
                CallbackCreds = new CallbackCreds
                {
                    UserId = "login123"
                },
                CallStatusMethod = "GET",
                CallInitiatedMethod = "GET",
                CallInitiatedCallbackUrl = "https://a.com",
                CallStatusCallbackUrl = "https://b.com"
            };

            string strResponse = XDocument.Parse(TestXmlStrings.singleApplicationProvisionResponse).ToString(SaveOptions.DisableFormatting);

            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "POST",
                EstimatedPathAndQuery = $"/v1.0/accounts/{Helper.AccountId}/applications",
                ContentToSend = new StringContent(strResponse, Encoding.UTF8, "application/xml")
            }))


            {
                var client = Helper.CreateClient();
                var t = Application.Create(client, application);
                var r = t.Result;
                if (server.Error != null) throw server.Error;

                Assert.NotNull(r.Application);

                Assert.Equal("d3e418e9-1833-49c1-b6c7-ca1700f79586", r.Application.ApplicationId);
                Assert.Equal("Voice-V2", r.Application.ServiceType);
                Assert.Equal("v1", r.Application.AppName);
                Assert.Equal("https://a.com", r.Application.CallInitiatedCallbackUrl);
                Assert.Equal("https://b.com", r.Application.CallStatusCallbackUrl);
                Assert.Equal("GET", r.Application.CallInitiatedMethod);
                Assert.Equal("GET", r.Application.CallStatusMethod);
                Assert.Equal("login123", r.Application.CallbackCreds.UserId);


            }
        }


        [Fact]
        public void TestGetApplication()
        {

            string applicationId = "d3e418e9-1833-49c1-b6c7-ca1700f79586";

            string strResponse = XDocument.Parse(TestXmlStrings.singleApplicationProvisionResponse).ToString(SaveOptions.DisableFormatting);

            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = $"/v1.0/accounts/{Helper.AccountId}/applications/{applicationId}",
                ContentToSend = new StringContent(strResponse, Encoding.UTF8, "application/xml")
            }))


            {
                var client = Helper.CreateClient();
                var t = Application.Get(client, applicationId);
                var r = t.Result;
                if (server.Error != null) throw server.Error;

                Assert.NotNull(r.Application);

                Assert.Equal("d3e418e9-1833-49c1-b6c7-ca1700f79586", r.Application.ApplicationId);
                Assert.Equal("Voice-V2", r.Application.ServiceType);
                Assert.Equal("v1", r.Application.AppName);
                Assert.Equal("https://a.com", r.Application.CallInitiatedCallbackUrl);
                Assert.Equal("https://b.com", r.Application.CallStatusCallbackUrl);
                Assert.Equal("GET", r.Application.CallInitiatedMethod);
                Assert.Equal("GET", r.Application.CallStatusMethod);
                Assert.Equal("login123", r.Application.CallbackCreds.UserId);


            }
        }

        [Fact]
        public void TestFullUpdateApplication()
        {

            string applicationId = "d3e418e9-1833-49c1-b6c7-ca1700f79586";

            var application = new Application
            {
                ApplicationId = applicationId
            };

            string strResponse = XDocument.Parse(TestXmlStrings.singleApplicationProvisionResponse).ToString(SaveOptions.DisableFormatting);

            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "PUT",
                EstimatedPathAndQuery = $"/v1.0/accounts/{Helper.AccountId}/applications/{applicationId}",
                ContentToSend = new StringContent(strResponse, Encoding.UTF8, "application/xml")
            }))


            {
                var client = Helper.CreateClient();
                var t = Application.FullUpdate(client, applicationId, application);
                var r = t.Result;
                if (server.Error != null) throw server.Error;

                Assert.NotNull(r.Application);

                Assert.Equal("d3e418e9-1833-49c1-b6c7-ca1700f79586", r.Application.ApplicationId);
                Assert.Equal("Voice-V2", r.Application.ServiceType);
                Assert.Equal("v1", r.Application.AppName);
                Assert.Equal("https://a.com", r.Application.CallInitiatedCallbackUrl);
                Assert.Equal("https://b.com", r.Application.CallStatusCallbackUrl);
                Assert.Equal("GET", r.Application.CallInitiatedMethod);
                Assert.Equal("GET", r.Application.CallStatusMethod);
                Assert.Equal("login123", r.Application.CallbackCreds.UserId);


            }
        }

        [Fact]
        public void TestPartialUpdateApplication()
        {

            string applicationId = "d3e418e9-1833-49c1-b6c7-ca1700f79586";

            var application = new Application
            {
                ApplicationId = applicationId
            };

            string strResponse = XDocument.Parse(TestXmlStrings.singleApplicationProvisionResponse).ToString(SaveOptions.DisableFormatting);

            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "PATCH",
                EstimatedPathAndQuery = $"/v1.0/accounts/{Helper.AccountId}/applications/{applicationId}",
                ContentToSend = new StringContent(strResponse, Encoding.UTF8, "application/xml")
            }))


            {
                var client = Helper.CreateClient();
                var t = Application.PartialUpdate(client, applicationId, application);
                var r = t.Result;
                if (server.Error != null) throw server.Error;

                Assert.NotNull(r.Application);

                Assert.Equal("d3e418e9-1833-49c1-b6c7-ca1700f79586", r.Application.ApplicationId);
                Assert.Equal("Voice-V2", r.Application.ServiceType);
                Assert.Equal("v1", r.Application.AppName);
                Assert.Equal("https://a.com", r.Application.CallInitiatedCallbackUrl);
                Assert.Equal("https://b.com", r.Application.CallStatusCallbackUrl);
                Assert.Equal("GET", r.Application.CallInitiatedMethod);
                Assert.Equal("GET", r.Application.CallStatusMethod);
                Assert.Equal("login123", r.Application.CallbackCreds.UserId);


            }
        }

        [Fact]
        public void TestDeleteApplication()
        {

            string applicationId = "d3e418e9-1833-49c1-b6c7-ca1700f79586";


            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "DELETE",
                EstimatedPathAndQuery = $"/v1.0/accounts/{Helper.AccountId}/applications/{applicationId}",
            }))


            {
                var client = Helper.CreateClient();
                var t = Application.Delete(client, applicationId);
                var r = t.Result;
                if (server.Error != null) throw server.Error;

                Assert.Equal(System.Net.HttpStatusCode.OK, r.StatusCode);


            }
        }

        [Fact]
        public void TestGetAssociatesSipPeers()
        {

            string applicationId = "d3e418e9-1833-49c1-b6c7-ca1700f79586";

            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = $"/v1.0/accounts/{Helper.AccountId}/applications/{applicationId}/associatedsippeers",
                ContentToSend = new StringContent(TestXmlStrings.associatedSipPeerResponse, Encoding.UTF8, "application/xml")
            }))

            {
                var client = Helper.CreateClient();
                var t = Application.ListAssociatedSippeers(client, applicationId);
                var r = t.Result;
                if (server.Error != null) throw server.Error;

                Assert.NotNull(r.AssociatedSipPeers);

                Assert.Equal(2, r.AssociatedSipPeers.Length);
                Assert.Equal("13651", r.AssociatedSipPeers[0].SiteId);
                Assert.Equal("Prod Sub-account", r.AssociatedSipPeers[0].SiteName);
                Assert.Equal("540341", r.AssociatedSipPeers[0].PeerId);
                Assert.Equal("Prod", r.AssociatedSipPeers[0].PeerName);



            }
        }

        [Fact]
        public void TestGetAssociatesSipPeers400()
        {

            string applicationId = "d3e418e9-1833-49c1-b6c7-ca1700f79586";

            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = $"/v1.0/accounts/{Helper.AccountId}/applications/{applicationId}/associatedsippeers",
                ContentToSend = new StringContent(TestXmlStrings.associatedSipPeerResponse400, Encoding.UTF8, "application/xml"),
                StatusCodeToSend = 400
            }))

            {
                var client = Helper.CreateClient();
                var t = Application.ListAssociatedSippeers(client, applicationId);
                try
                {
                    var r = t.Result;
                    Assert.True(false, "The exception was not thrown");
                }
                catch (AggregateException e)
                {
                    e.Handle(ex =>
                    {
                        if (ex is BandwidthIrisException)
                        {
                            Assert.Equal(" Current 1 Account have no Catapult association ", ex.Message);
                            return true;
                        }
                        return true;
                    });
                }

            }
        }

        [Fact]
        public void TestGetAssociatesSipPeers404()
        {

            string applicationId = "d3e418e9-1833-49c1-b6c7-ca1700f79586";

            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = $"/v1.0/accounts/{Helper.AccountId}/applications/{applicationId}/associatedsippeers",
                ContentToSend = new StringContent(TestXmlStrings.associatedSipPeerResponse404, Encoding.UTF8, "application/xml"),
                StatusCodeToSend = 404
            }))

            {
                var client = Helper.CreateClient();
                var t = Application.ListAssociatedSippeers(client, applicationId);
                try
                {
                    var r = t.Result;
                    Assert.True(false, "The exception was not thrown");
                }
                catch (AggregateException e)
                {
                    e.Handle(ex =>
                    {
                        if (ex is BandwidthIrisException)
                        {
                            Assert.Equal(" Application with id 'non_existing' not found ", ex.Message);
                            return true;
                        }
                        return true;
                    });
                }

            }
        }


    }
}
