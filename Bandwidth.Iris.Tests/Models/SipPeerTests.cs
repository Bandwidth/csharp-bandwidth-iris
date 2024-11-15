﻿using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Bandwidth.Iris.Model;
using Xunit;

namespace Bandwidth.Iris.Tests.Models
{

    public class SipPeerTests
    {
        // [TestInitialize]
        public SipPeerTests()
        {
            Helper.SetEnvironmentVariables();
        }
        [Fact]
        public void CreateTest()
        {
            var item = new SipPeer
            {
                Name = "test",
                SiteId = "1",
                IsDefaultPeer = true
            };
            var createdItem = new SipPeer
            {
                Id = "1",
                SiteId = "1",
                Name = "test",
                IsDefaultPeer = true
            };
            using (var server = new HttpServer(new[]
            {
                new RequestHandler
                {
                    EstimatedMethod = "POST",
                    EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/sites/1/sippeers", Helper.AccountId),
                    EstimatedContent = Helper.ToXmlString(item),
                    HeadersToSend = new Dictionary<string, string>
                    {
                        {"Location", string.Format("/v1.0/accounts/{0}/sites/1/sippeers/10", Helper.AccountId)}
                    },
                    StatusCodeToSend = 201
                },
                new RequestHandler
                {
                    EstimatedMethod = "GET",
                    EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/sites/1/sippeers/10", Helper.AccountId),
                    ContentToSend = Helper.CreateXmlContent(new SipPeerResponse{SipPeer = createdItem})
                }
            }))
            {
                var client = Helper.CreateClient();
                var r = SipPeer.Create(client, item).Result;
                Helper.AssertObjects(createdItem, r);
                if (server.Error != null) throw server.Error;
            }
        }

        [Fact]
        public void CreateWithDefaultClientTest()
        {
            var item = new SipPeer
            {
                Name = "test",
                SiteId = "1",
                IsDefaultPeer = true
            };
            var createdItem = new SipPeer
            {
                Id = "1",
                SiteId = "1",
                Name = "test",
                IsDefaultPeer = true
            };
            using (var server = new HttpServer(new[]
            {
                new RequestHandler
                {
                    EstimatedMethod = "POST",
                    EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/sites/1/sippeers", Helper.AccountId),
                    EstimatedContent = Helper.ToXmlString(item),
                    HeadersToSend = new Dictionary<string, string>
                    {
                        {"Location", string.Format("/v1.0/accounts/{0}/sites/1/sippeers/10", Helper.AccountId)}
                    },
                    StatusCodeToSend = 201
                },
                new RequestHandler
                {
                    EstimatedMethod = "GET",
                    EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/sites/1/sippeers/10", Helper.AccountId),
                    ContentToSend = Helper.CreateXmlContent(new SipPeerResponse{SipPeer = createdItem})
                }
            }))
            {
                var r = SipPeer.Create(item).Result;
                Helper.AssertObjects(createdItem, r);
                if (server.Error != null) throw server.Error;
            }
        }

        [Fact]
        public void GetTest()
        {
            var item = new SipPeer
            {
                Id = "10",
                SiteId = "1",
                Name = "test",
                IsDefaultPeer = true
            };
            using (var server = new HttpServer(new[]
            {
                new RequestHandler
                {
                    EstimatedMethod = "GET",
                    EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/sites/1/sippeers/10", Helper.AccountId),
                    ContentToSend = Helper.CreateXmlContent(new SipPeerResponse{SipPeer = item})
                }
            }))
            {
                var client = Helper.CreateClient();
                var r = SipPeer.Get(client, "1", "10").Result;
                if (server.Error != null) throw server.Error;
                Helper.AssertObjects(item, r);

            }
        }

        [Fact]
        public void GetWithDefaultClientTest()
        {
            var item = new SipPeer
            {
                Id = "10",
                SiteId = "1",
                Name = "test",
                IsDefaultPeer = true
            };
            using (var server = new HttpServer(new[]
            {
                new RequestHandler
                {
                    EstimatedMethod = "GET",
                    EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/sites/1/sippeers/10", Helper.AccountId),
                    ContentToSend = Helper.CreateXmlContent(new SipPeerResponse{SipPeer = item})
                }
            }))
            {
                var r = SipPeer.Get("1", "10").Result;
                if (server.Error != null) throw server.Error;
                Helper.AssertObjects(item, r);

            }
        }

        [Fact]
        public void ListTest()
        {
            var items = new[]{
                new SipPeer
                {
                    Id = "10",
                    SiteId = "1",
                    Name = "test",
                    IsDefaultPeer = true
                },
                new SipPeer
                {
                    Id = "11",
                    SiteId = "1",
                    Name = "test2",
                    IsDefaultPeer = false
                }

            };
            using (var server = new HttpServer(new[]
            {
                new RequestHandler
                {
                    EstimatedMethod = "GET",
                    EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/sites/1/sippeers", Helper.AccountId),
                    ContentToSend = Helper.CreateXmlContent(new SipPeersResponse{SipPeers = items})
                }
            }))
            {
                var client = Helper.CreateClient();
                var r = SipPeer.List(client, "1").Result;
                if (server.Error != null) throw server.Error;
                Helper.AssertObjects(items[0], r[0]);
                Helper.AssertObjects(items[1], r[1]);
            }
        }

        [Fact]
        public void ListWithDefaultClientTest()
        {
            var items = new[]{
                new SipPeer
                {
                    Id = "10",
                    SiteId = "1",
                    Name = "test",
                    IsDefaultPeer = true
                },
                new SipPeer
                {
                    Id = "11",
                    SiteId = "1",
                    Name = "test2",
                    IsDefaultPeer = false
                }

            };
            using (var server = new HttpServer(new[]
            {
                new RequestHandler
                {
                    EstimatedMethod = "GET",
                    EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/sites/1/sippeers", Helper.AccountId),
                    ContentToSend = Helper.CreateXmlContent(new SipPeersResponse{SipPeers = items})
                }
            }))
            {
                var r = SipPeer.List("1").Result;
                if (server.Error != null) throw server.Error;
                Helper.AssertObjects(items[0], r[0]);
                Helper.AssertObjects(items[1], r[1]);
            }
        }

        [Fact]
        public void GetTnsTest()
        {
            var item = new SipPeerTelephoneNumber
            {
                FullNumber = "Number",
                RewriteUser = "User"
            };
            var response = new SipPeerTelephoneNumberResponse
            {
                SipPeerTelephoneNumber = item
            };
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/sites/1/sippeers/10/tns/00", Helper.AccountId),
                ContentToSend = Helper.CreateXmlContent(response)
            }))
            {
                var client = Helper.CreateClient();
                var peer = new SipPeer
                {
                    Id = "10",
                    SiteId = "1"
                };
                peer.SetClient(client);
                var result = peer.GetTns("00").Result;
                if (server.Error != null) throw server.Error;
                Helper.AssertObjects(item, result);
            }
        }

        [Fact]
        public void GetTnsWithXmlTest()
        {
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/sites/1/sippeers/10/tns/00", Helper.AccountId),
                ContentToSend = new StringContent(TestXmlStrings.ValidSipPeerTnResponseXml, Encoding.UTF8, "application/xml")
            }))
            {
                var client = Helper.CreateClient();
                var peer = new SipPeer
                {
                    Id = "10",
                    SiteId = "1"
                };
                peer.SetClient(client);
                var result = peer.GetTns("00").Result;
                if (server.Error != null) throw server.Error;
                Assert.Equal("9195551212", result.FullNumber);
            }
        }

        [Fact]
        public void GetTns2WithXmlTest()
        {
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/sites/1/sippeers/10/tns?page=1&size=50000", Helper.AccountId),
                ContentToSend = new StringContent(TestXmlStrings.ValidSipPeerTnsResponseXml, Encoding.UTF8, "application/xml")
            }))
            {
                var client = Helper.CreateClient();
                var peer = new SipPeer
                {
                    Id = "10",
                    SiteId = "1"
                };
                peer.SetClient(client);
                var result = peer.GetTns().Result;
                if (server.Error != null) throw server.Error;
                Assert.Equal(17, result.Length);
                Assert.Equal("3034162216", result[0].FullNumber);
                Assert.Equal("3034162218", result[1].FullNumber);
            }
        }

        [Fact]
        public void UpdateTnsTest()
        {
            var item = new SipPeerTelephoneNumber
            {
                FullNumber = "Number",
                RewriteUser = "User"
            };
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "PUT",
                EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/sites/1/sippeers/10/tns/00", Helper.AccountId),
                EstimatedContent = Helper.ToXmlString(item)
            }))
            {
                var client = Helper.CreateClient();
                var peer = new SipPeer
                {
                    Id = "10",
                    SiteId = "1"
                };
                peer.SetClient(client);
                peer.UpdateTns("00", item).Wait();
                if (server.Error != null) throw server.Error;
            }
        }

        [Fact]
        public void MoveTnsTest()
        {
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "POST",
                EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/sites/1/sippeers/10/movetns", Helper.AccountId),
                EstimatedContent = "<?xml version=\"1.0\" encoding=\"utf-8\"?><SipPeerTelephoneNumbers>  <FullNumber>111</FullNumber>  <FullNumber>222</FullNumber></SipPeerTelephoneNumbers>"
            }))
            {
                var client = Helper.CreateClient();
                var peer = new SipPeer
                {
                    Id = "10",
                    SiteId = "1"
                };
                peer.SetClient(client);
                peer.MoveTns("111", "222").Wait();
                if (server.Error != null) throw server.Error;
            }
        }

        [Fact]
        public void GetOriginationSettingsTest()
        {

            string siteId = "1";
            string sipPeerId = "test";
            using (var server = new HttpServer(new[]
            {
                new RequestHandler
                {
                    EstimatedMethod = "GET",
                    EstimatedPathAndQuery = $"/v1.0/accounts/{Helper.AccountId}/sites/{siteId}/sippeers/{sipPeerId}/products/origination/settings",
                    ContentToSend = new StringContent(TestXmlStrings.SipPeerOriginationSettingsResponse, Encoding.UTF8, "application/xml")
                }
            }))
            {
                var client = Helper.CreateClient();
                var r = SipPeer.GetOriginationSettings(siteId, sipPeerId).Result;
                if (server.Error != null) throw server.Error;

                Assert.NotNull(r.SipPeerOriginationSettings);
                Assert.Equal("HTTP", r.SipPeerOriginationSettings.VoiceProtocol);
                Assert.Equal("469ebbac-4459-4d98-bc19-a038960e787f", r.SipPeerOriginationSettings.HttpSettings.HttpVoiceV2AppId);


            }
        }

        [Fact]
        public void SetOriginationSettingsTest()
        {

            string siteId = "1";
            string sipPeerId = "test";

            var SipPeerOriginationSettings = new SipPeerOriginationSettings
            {
                VoiceProtocol = "HTTP",
                HttpSettings = new HttpSettings
                {
                    HttpVoiceV2AppId = "469ebbac-4459-4d98-bc19-a038960e787f"
                }
            };

            using (var server = new HttpServer(new[]
            {
                new RequestHandler
                {
                    EstimatedMethod = "POST",
                    EstimatedPathAndQuery = $"/v1.0/accounts/{Helper.AccountId}/sites/{siteId}/sippeers/{sipPeerId}/products/origination/settings",
                    ContentToSend = new StringContent(TestXmlStrings.SipPeerOriginationSettingsResponse, Encoding.UTF8, "application/xml")
                }
            }))
            {
                var client = Helper.CreateClient();
                var r = SipPeer.SetOriginationSettings(siteId, sipPeerId, SipPeerOriginationSettings).Result;
                if (server.Error != null) throw server.Error;

                Assert.NotNull(r.SipPeerOriginationSettings);
                Assert.Equal("HTTP", r.SipPeerOriginationSettings.VoiceProtocol);
                Assert.Equal("469ebbac-4459-4d98-bc19-a038960e787f", r.SipPeerOriginationSettings.HttpSettings.HttpVoiceV2AppId);


            }
        }

        [Fact]
        public void UpdateOriginationSettingsTest()
        {
            string siteId = "1";
            string sipPeerId = "test";

            var SipPeerOriginationSettings = new SipPeerOriginationSettings
            {
                VoiceProtocol = "HTTP",
                HttpSettings = new HttpSettings
                {
                    HttpVoiceV2AppId = "469ebbac-4459-4d98-bc19-a038960e787f"
                }
            };

            using (var server = new HttpServer(new[]
            {
                new RequestHandler
                {
                    EstimatedMethod = "PUT",
                    EstimatedPathAndQuery = $"/v1.0/accounts/{Helper.AccountId}/sites/{siteId}/sippeers/{sipPeerId}/products/origination/settings",
                }
            }))
            {
                var client = Helper.CreateClient();
                SipPeer.UpdateOriginationSettings(siteId, sipPeerId, SipPeerOriginationSettings).Wait();
                if (server.Error != null) throw server.Error;

            }
        }


        [Fact]
        public void GetTerminationSettingsTest()
        {

            string siteId = "1";
            string sipPeerId = "test";
            using (var server = new HttpServer(new[]
            {
                new RequestHandler
                {
                    EstimatedMethod = "GET",
                    EstimatedPathAndQuery = $"/v1.0/accounts/{Helper.AccountId}/sites/{siteId}/sippeers/{sipPeerId}/products/termination/settings",
                    ContentToSend = new StringContent(TestXmlStrings.SipPeerTerminationSettingResponse, Encoding.UTF8, "application/xml")
                }
            }))
            {
                var client = Helper.CreateClient();
                var r = SipPeer.GetTerminationSetting(siteId, sipPeerId).Result;
                if (server.Error != null) throw server.Error;

                Assert.NotNull(r.SipPeerTerminationSettings);
                Assert.Equal("HTTP", r.SipPeerTerminationSettings.VoiceProtocol);
                Assert.Equal("469ebbac-4459-4d98-bc19-a038960e787f", r.SipPeerTerminationSettings.HttpSettings.HttpVoiceV2AppId);


            }
        }

        [Fact]
        public void SetTerminationSettingsTest()
        {

            string siteId = "1";
            string sipPeerId = "test";

            var SipPeerTerminationSettings = new SipPeerTerminationSettings
            {
                VoiceProtocol = "HTTP",
                HttpSettings = new HttpSettings
                {
                    HttpVoiceV2AppId = "469ebbac-4459-4d98-bc19-a038960e787f"
                }
            };

            using (var server = new HttpServer(new[]
            {
                new RequestHandler
                {
                    EstimatedMethod = "POST",
                    EstimatedPathAndQuery = $"/v1.0/accounts/{Helper.AccountId}/sites/{siteId}/sippeers/{sipPeerId}/products/termination/settings",
                    ContentToSend = new StringContent(TestXmlStrings.SipPeerTerminationSettingResponse, Encoding.UTF8, "application/xml")
                }
            }))
            {
                var client = Helper.CreateClient();
                var r = SipPeer.SetTerminationSettings(client, siteId, sipPeerId, SipPeerTerminationSettings).Result;
                if (server.Error != null) throw server.Error;

                Assert.NotNull(r.SipPeerTerminationSettings);
                Assert.Equal("HTTP", r.SipPeerTerminationSettings.VoiceProtocol);
                Assert.Equal("469ebbac-4459-4d98-bc19-a038960e787f", r.SipPeerTerminationSettings.HttpSettings.HttpVoiceV2AppId);


            }
        }

        [Fact]
        public void UpdateTerminationSettingsTest()
        {
            string siteId = "1";
            string sipPeerId = "test";

            var SipPeerTerminationSettings = new SipPeerTerminationSettings
            {
                VoiceProtocol = "HTTP",
                HttpSettings = new HttpSettings
                {
                    HttpVoiceV2AppId = "469ebbac-4459-4d98-bc19-a038960e787f"
                }
            };

            using (var server = new HttpServer(new[]
            {
                new RequestHandler
                {
                    EstimatedMethod = "PUT",
                    EstimatedPathAndQuery = $"/v1.0/accounts/{Helper.AccountId}/sites/{siteId}/sippeers/{sipPeerId}/products/termination/settings",
                }
            }))
            {
                var client = Helper.CreateClient();
                SipPeer.UpdateTerminationSettings(client, siteId, sipPeerId, SipPeerTerminationSettings).Wait();
                if (server.Error != null) throw server.Error;

            }
        }

        [Fact]
        public void GetSMSSettingTest()
        {
            string siteId = "1";
            string sipPeerId = "test";
            using (var server = new HttpServer(new[]
            {
                new RequestHandler
                {
                    EstimatedMethod = "GET",
                    EstimatedPathAndQuery = $"/v1.0/accounts/{Helper.AccountId}/sites/{siteId}/sippeers/{sipPeerId}/products/messaging/features/sms",
                    ContentToSend = new StringContent(TestXmlStrings.SipPeerSmsFeatureResponse, Encoding.UTF8, "application/xml")
                }
            }))
            {
                var client = Helper.CreateClient();
                var r = SipPeer.GetSMSSetting(siteId, sipPeerId).Result;
                if (server.Error != null) throw server.Error;

                Assert.NotNull(r.SipPeerSmsFeature);
                Assert.True(r.SipPeerSmsFeature.SipPeerSmsFeatureSettings.TollFree);
                Assert.True(r.SipPeerSmsFeature.SipPeerSmsFeatureSettings.ShortCode);
                Assert.Equal("DefaultOff", r.SipPeerSmsFeature.SipPeerSmsFeatureSettings.A2pLongCode);
                Assert.Equal("SomeMessageClass", r.SipPeerSmsFeature.SipPeerSmsFeatureSettings.A2pMessageClass);
                Assert.Equal("SomeCampaignId", r.SipPeerSmsFeature.SipPeerSmsFeatureSettings.A2pCampaignId);
                Assert.Equal("SMPP", r.SipPeerSmsFeature.SipPeerSmsFeatureSettings.Protocol);
                Assert.True(r.SipPeerSmsFeature.SipPeerSmsFeatureSettings.Zone1);
                Assert.True(r.SipPeerSmsFeature.SipPeerSmsFeatureSettings.Zone2);
                Assert.True(r.SipPeerSmsFeature.SipPeerSmsFeatureSettings.Zone3);
                Assert.True(r.SipPeerSmsFeature.SipPeerSmsFeatureSettings.Zone4);
                Assert.True(r.SipPeerSmsFeature.SipPeerSmsFeatureSettings.Zone5);

                Assert.Equal(2, r.SipPeerSmsFeature.SmppHosts.Length);
                Assert.Equal("RECEIVER_ONLY", r.SipPeerSmsFeature.SmppHosts[0].ConnectionType);
                Assert.Equal("54.10.88.146", r.SipPeerSmsFeature.SmppHosts[0].HostName);
                Assert.Equal(0, r.SipPeerSmsFeature.SmppHosts[0].Priority);

            }
        }

        [Fact]
        public void SetSMSSettingTest()
        {
            string siteId = "1";
            string sipPeerId = "test";

            var SipPeerSmsFeature = new SipPeerSmsFeature
            {
                SipPeerSmsFeatureSettings = new SipPeerSmsFeatureSettings
                {
                    TollFree = true
                },
                SmppHosts = new SmppHost[]
                {
                    new SmppHost
                    {
                        HostName = "Host"
                    }
                }
            };

            using (var server = new HttpServer(new[]
            {
                new RequestHandler
                {
                    EstimatedMethod = "POST",
                    EstimatedPathAndQuery = $"/v1.0/accounts/{Helper.AccountId}/sites/{siteId}/sippeers/{sipPeerId}/products/messaging/features/sms",
                    ContentToSend = new StringContent(TestXmlStrings.SipPeerSmsFeatureResponse, Encoding.UTF8, "application/xml")
                }
            }))
            {
                var client = Helper.CreateClient();
                var r = SipPeer.CreateSMSSettings(siteId, sipPeerId, SipPeerSmsFeature).Result;
                if (server.Error != null) throw server.Error;

                Assert.NotNull(r.SipPeerSmsFeature);
                Assert.True(r.SipPeerSmsFeature.SipPeerSmsFeatureSettings.TollFree);
                Assert.True(r.SipPeerSmsFeature.SipPeerSmsFeatureSettings.ShortCode);
                Assert.Equal("DefaultOff", r.SipPeerSmsFeature.SipPeerSmsFeatureSettings.A2pLongCode);
                Assert.Equal("SomeMessageClass", r.SipPeerSmsFeature.SipPeerSmsFeatureSettings.A2pMessageClass);
                Assert.Equal("SomeCampaignId", r.SipPeerSmsFeature.SipPeerSmsFeatureSettings.A2pCampaignId);
                Assert.Equal("SMPP", r.SipPeerSmsFeature.SipPeerSmsFeatureSettings.Protocol);
                Assert.True(r.SipPeerSmsFeature.SipPeerSmsFeatureSettings.Zone1);
                Assert.True(r.SipPeerSmsFeature.SipPeerSmsFeatureSettings.Zone2);
                Assert.True(r.SipPeerSmsFeature.SipPeerSmsFeatureSettings.Zone3);
                Assert.True(r.SipPeerSmsFeature.SipPeerSmsFeatureSettings.Zone4);
                Assert.True(r.SipPeerSmsFeature.SipPeerSmsFeatureSettings.Zone5);

                Assert.Equal(2, r.SipPeerSmsFeature.SmppHosts.Length);
                Assert.Equal("RECEIVER_ONLY", r.SipPeerSmsFeature.SmppHosts[0].ConnectionType);
                Assert.Equal("54.10.88.146", r.SipPeerSmsFeature.SmppHosts[0].HostName);
                Assert.Equal(0, r.SipPeerSmsFeature.SmppHosts[0].Priority);

            }
        }

        [Fact]
        public void UpdateSMSSettingTest()
        {
            string siteId = "1";
            string sipPeerId = "test";

            var SipPeerSmsFeature = new SipPeerSmsFeature
            {
                SipPeerSmsFeatureSettings = new SipPeerSmsFeatureSettings
                {
                    TollFree = true
                },
                SmppHosts = new SmppHost[]
                {
                    new SmppHost
                    {
                        HostName = "Host"
                    }
                }
            };

            using (var server = new HttpServer(new[]
            {
                new RequestHandler
                {
                    EstimatedMethod = "PUT",
                    EstimatedPathAndQuery = $"/v1.0/accounts/{Helper.AccountId}/sites/{siteId}/sippeers/{sipPeerId}/products/messaging/features/sms",
                    ContentToSend = new StringContent(TestXmlStrings.SipPeerSmsFeatureResponse, Encoding.UTF8, "application/xml")
                }
            }))
            {
                var client = Helper.CreateClient();
                var r = SipPeer.UpdateSMSSettings(siteId, sipPeerId, SipPeerSmsFeature).Result;
                if (server.Error != null) throw server.Error;

                Assert.NotNull(r.SipPeerSmsFeature);
                Assert.True(r.SipPeerSmsFeature.SipPeerSmsFeatureSettings.TollFree);
                Assert.True(r.SipPeerSmsFeature.SipPeerSmsFeatureSettings.ShortCode);
                Assert.Equal("DefaultOff", r.SipPeerSmsFeature.SipPeerSmsFeatureSettings.A2pLongCode);
                Assert.Equal("SomeMessageClass", r.SipPeerSmsFeature.SipPeerSmsFeatureSettings.A2pMessageClass);
                Assert.Equal("SomeCampaignId", r.SipPeerSmsFeature.SipPeerSmsFeatureSettings.A2pCampaignId);
                Assert.Equal("SMPP", r.SipPeerSmsFeature.SipPeerSmsFeatureSettings.Protocol);
                Assert.True(r.SipPeerSmsFeature.SipPeerSmsFeatureSettings.Zone1);
                Assert.True(r.SipPeerSmsFeature.SipPeerSmsFeatureSettings.Zone2);
                Assert.True(r.SipPeerSmsFeature.SipPeerSmsFeatureSettings.Zone3);
                Assert.True(r.SipPeerSmsFeature.SipPeerSmsFeatureSettings.Zone4);
                Assert.True(r.SipPeerSmsFeature.SipPeerSmsFeatureSettings.Zone5);

                Assert.Equal(2, r.SipPeerSmsFeature.SmppHosts.Length);
                Assert.Equal("RECEIVER_ONLY", r.SipPeerSmsFeature.SmppHosts[0].ConnectionType);
                Assert.Equal("54.10.88.146", r.SipPeerSmsFeature.SmppHosts[0].HostName);
                Assert.Equal(0, r.SipPeerSmsFeature.SmppHosts[0].Priority);

            }
        }

        [Fact]
        public void DeleteSMSSettingTest()
        {
            string siteId = "1";
            string sipPeerId = "test";
            using (var server = new HttpServer(new[]
            {
                new RequestHandler
                {
                    EstimatedMethod = "DELETE",
                    EstimatedPathAndQuery = $"/v1.0/accounts/{Helper.AccountId}/sites/{siteId}/sippeers/{sipPeerId}/products/messaging/features/sms",
                }
            }))
            {
                var client = Helper.CreateClient();
                SipPeer.DeleteSMSSettings(siteId, sipPeerId).Wait();
                if (server.Error != null) throw server.Error;

            }
        }


        [Fact]
        public void GetMMSSettingTest()
        {
            string siteId = "1";
            string sipPeerId = "test";
            using (var server = new HttpServer(new[]
            {
                new RequestHandler
                {
                    EstimatedMethod = "GET",
                    EstimatedPathAndQuery = $"/v1.0/accounts/{Helper.AccountId}/sites/{siteId}/sippeers/{sipPeerId}/products/messaging/features/mms",
                    ContentToSend = new StringContent(TestXmlStrings.MmsFeatureResponse, Encoding.UTF8, "application/xml")
                }
            }))
            {
                var client = Helper.CreateClient();
                var r = SipPeer.GetMMSSetting(siteId, sipPeerId).Result;
                if (server.Error != null) throw server.Error;

                Assert.NotNull(r.MmsFeature);
                Assert.Equal("OFF", r.MmsFeature.Protocols.MM4.Tls);
                Assert.Single(r.MmsFeature.Protocols.MM4.MmsMM4TermHosts.TermHosts);
                Assert.Equal("206.107.248.58", r.MmsFeature.Protocols.MM4.MmsMM4TermHosts.TermHosts[0].HostName);

                Assert.Equal(2, r.MmsFeature.Protocols.MM4.MmsMM4OrigHosts.OrigHosts.Length);
                Assert.Equal("30.239.72.55", r.MmsFeature.Protocols.MM4.MmsMM4OrigHosts.OrigHosts[0].HostName);
                Assert.Equal(8726, r.MmsFeature.Protocols.MM4.MmsMM4OrigHosts.OrigHosts[0].Port);
                Assert.Equal(0, r.MmsFeature.Protocols.MM4.MmsMM4OrigHosts.OrigHosts[0].Priority);

            }
        }

        [Fact]
        public void SetMMSSettingTest()
        {
            string siteId = "1";
            string sipPeerId = "test";

            var MmsFeature = new MmsFeature
            {
                Protocols = new Protocols
                {
                    MM4 = new MM4
                    {
                        Tls = "OFF"
                    }
                }
            };

            using (var server = new HttpServer(new[]
            {
                new RequestHandler
                {
                    EstimatedMethod = "POST",
                    EstimatedPathAndQuery = $"/v1.0/accounts/{Helper.AccountId}/sites/{siteId}/sippeers/{sipPeerId}/products/messaging/features/mms",
                    ContentToSend = new StringContent(TestXmlStrings.MmsFeatureResponse, Encoding.UTF8, "application/xml")
                }
            }))
            {
                var client = Helper.CreateClient();
                var r = SipPeer.CreateMMSSettings(siteId, sipPeerId, MmsFeature).Result;
                if (server.Error != null) throw server.Error;

                Assert.NotNull(r.MmsFeature);
                Assert.Equal("OFF", r.MmsFeature.Protocols.MM4.Tls);
                Assert.Single(r.MmsFeature.Protocols.MM4.MmsMM4TermHosts.TermHosts);
                Assert.Equal("206.107.248.58", r.MmsFeature.Protocols.MM4.MmsMM4TermHosts.TermHosts[0].HostName);

                Assert.Equal(2, r.MmsFeature.Protocols.MM4.MmsMM4OrigHosts.OrigHosts.Length);
                Assert.Equal("30.239.72.55", r.MmsFeature.Protocols.MM4.MmsMM4OrigHosts.OrigHosts[0].HostName);
                Assert.Equal(8726, r.MmsFeature.Protocols.MM4.MmsMM4OrigHosts.OrigHosts[0].Port);
                Assert.Equal(0, r.MmsFeature.Protocols.MM4.MmsMM4OrigHosts.OrigHosts[0].Priority);

            }
        }

        [Fact]
        public void UpdateMMSSettingTest()
        {
            string siteId = "1";
            string sipPeerId = "test";

            var MmsFeature = new MmsFeature
            {
                Protocols = new Protocols
                {
                    MM4 = new MM4
                    {
                        Tls = "OFF"
                    }
                }
            };

            using (var server = new HttpServer(new[]
            {
                new RequestHandler
                {
                    EstimatedMethod = "PUT",
                    EstimatedPathAndQuery = $"/v1.0/accounts/{Helper.AccountId}/sites/{siteId}/sippeers/{sipPeerId}/products/messaging/features/mms",
                    ContentToSend = new StringContent(TestXmlStrings.SipPeerSmsFeatureResponse, Encoding.UTF8, "application/xml")
                }
            }))
            {
                var client = Helper.CreateClient();
                SipPeer.UpdateMMSSettings(siteId, sipPeerId, MmsFeature).Wait();
                if (server.Error != null) throw server.Error;



            }
        }

        [Fact]
        public void DeleteMMSSettingTest()
        {
            string siteId = "1";
            string sipPeerId = "test";
            using (var server = new HttpServer(new[]
            {
                new RequestHandler
                {
                    EstimatedMethod = "DELETE",
                    EstimatedPathAndQuery = $"/v1.0/accounts/{Helper.AccountId}/sites/{siteId}/sippeers/{sipPeerId}/products/messaging/features/mms",
                }
            }))
            {
                var client = Helper.CreateClient();
                SipPeer.DeleteMMSSettings(siteId, sipPeerId).Wait();
                if (server.Error != null) throw server.Error;

            }
        }


        [Fact]
        public void GetApplicationSettingTest()
        {
            string siteId = "1";
            string sipPeerId = "test";
            using (var server = new HttpServer(new[]
            {
                new RequestHandler
                {
                    EstimatedMethod = "GET",
                    EstimatedPathAndQuery = $"/v1.0/accounts/{Helper.AccountId}/sites/{siteId}/sippeers/{sipPeerId}/products/messaging/applicationSettings",
                    ContentToSend = new StringContent(TestXmlStrings.ApplicationSettingsResponse, Encoding.UTF8, "application/xml")
                }
            }))
            {
                var client = Helper.CreateClient();
                var r = SipPeer.GetApplicationSetting(siteId, sipPeerId).Result;
                if (server.Error != null) throw server.Error;

                Assert.NotNull(r.ApplicationsSettings);
                Assert.Equal("4a4ca6c1-156b-4fca-84e9-34e35e2afc32", r.ApplicationsSettings.HttpMessagingV2AppId);


            }
        }

        [Fact]
        public void UpdateApplicationSettingTest()
        {
            string siteId = "1";
            string sipPeerId = "test";

            var ApplicationSettings = new ApplicationsSettings
            {
                HttpMessagingV2AppId = "c3b0f805-06ab-4d36-8bf4-8baff7623398"
            };

            using (var server = new HttpServer(new[]
            {
                new RequestHandler
                {
                    EstimatedMethod = "PUT",
                    EstimatedPathAndQuery = $"/v1.0/accounts/{Helper.AccountId}/sites/{siteId}/sippeers/{sipPeerId}/products/messaging/applicationSettings",
                    ContentToSend = new StringContent(TestXmlStrings.ApplicationSettingsResponse, Encoding.UTF8, "application/xml")
                }
            }))
            {
                var client = Helper.CreateClient();
                SipPeer.UpdateApplicationSettings(siteId, sipPeerId, ApplicationSettings).Wait();
                if (server.Error != null) throw server.Error;
            }
        }

        [Fact]
        public void RemoveApplicationSettingsTest()
        {
            string siteId = "1";
            string sipPeerId = "test";

            using (var server = new HttpServer(new[]
            {
                new RequestHandler
                {
                    EstimatedMethod = "PUT",
                    EstimatedPathAndQuery = $"/v1.0/accounts/{Helper.AccountId}/sites/{siteId}/sippeers/{sipPeerId}/products/messaging/applicationSettings",
                    EstimatedContent = TestXmlStrings.RemoveApplicationResponse
                }
            }))
            {
                var client = Helper.CreateClient();
                SipPeer.RemoveApplicationSettings(siteId, sipPeerId).Wait();
                if (server.Error != null) throw server.Error;
            }
        }

    }

}
