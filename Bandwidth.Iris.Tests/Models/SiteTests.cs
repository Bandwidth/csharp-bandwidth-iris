using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Bandwidth.Iris.Model;
using Xunit;

namespace Bandwidth.Iris.Tests.Models
{

    public class SiteTests
    {
        // [TestInitialize]
        public SiteTests()
        {
            Helper.SetEnvironmentVariables();
        }

        [Fact]
        public void GetTest()
        {
            var item = new Site
            {
                Id = "1",
                Name = "Name",
                Address = new Address
                {
                    City = "City",
                    Country = "County"
                }
            };
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/sites/1", Helper.AccountId),
                ContentToSend = Helper.CreateXmlContent(new SiteResponse { Site = item })
            }))
            {
                var client = Helper.CreateClient();
                var result = Site.Get(client, "1").Result;
                if (server.Error != null) throw server.Error;
                Helper.AssertObjects(item, result);
            }
        }

        [Fact]
        public void GetWithXmlTest()
        {

            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/sites/1", Helper.AccountId),
                ContentToSend = new StringContent(TestXmlStrings.ValidSiteResponseXml, Encoding.UTF8, "application/xml")
            }))
            {
                var client = Helper.CreateClient();
                var result = Site.Get(client, "1").Result;
                if (server.Error != null) throw server.Error;
                Assert.Equal("1", result.Id);
                Assert.Equal("Test Site", result.Name);
                Assert.Equal("A Site Description", result.Description);
                Assert.Equal("900", result.Address.HouseNumber);
                Assert.Equal("Main Campus Drive", result.Address.StreetName);
                Assert.Equal("Raleigh", result.Address.City);
                Assert.Equal("NC", result.Address.StateCode);
                Assert.Equal("27615", result.Address.Zip);
                Assert.Equal("United States", result.Address.Country);
                Assert.Equal("Service", result.Address.AddressType);
            }
        }

        [Fact]
        public void PostDirectionalAddressTest()
        {
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/sites/1", Helper.AccountId),
                ContentToSend = new StringContent(TestXmlStrings.SiteWithAddressPostDirectional, Encoding.UTF8, "application/xml")
            }))
            {
                var client = Helper.CreateClient();
                var result = Site.Get(client, "1").Result;
                if (server.Error != null) throw server.Error;
                Assert.Equal("Raleigh", result.Name);
                Assert.Equal("NW", result.Address.PostDirectional);

            }
        }

        [Fact]
        public void GetWithDefaultClientTest()
        {
            var item = new Site
            {
                Id = "1",
                Name = "Name",
                Address = new Address
                {
                    City = "City",
                    Country = "County"
                }
            };
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/sites/1", Helper.AccountId),
                ContentToSend = Helper.CreateXmlContent(new SiteResponse { Site = item })
            }))
            {
                var result = Site.Get("1").Result;
                if (server.Error != null) throw server.Error;
                Helper.AssertObjects(item, result);
            }
        }

        [Fact]
        public void ListTest()
        {
            var items = new[]
            {
                new Site
                {
                    Id = "1",
                    Name = "Name1",
                    Address = new Address
                    {
                        City = "City1",
                        Country = "Country1"
                    }
                },
                new Site
                {
                    Id = "2",
                    Name = "Name2",
                    Address = new Address
                    {
                        City = "City2",
                        Country = "Country2"
                    }
                }
            };
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/sites", Helper.AccountId),
                ContentToSend = Helper.CreateXmlContent(new SitesResponse { Sites = items })
            }))
            {
                var client = Helper.CreateClient();
                var result = Site.List(client).Result;
                if (server.Error != null) throw server.Error;
                Helper.AssertObjects(items[0], result[0]);
                Helper.AssertObjects(items[1], result[1]);
            }
        }

        [Fact]
        public void ListWithXmlTest()
        {
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/sites", Helper.AccountId),
                ContentToSend = new StringContent(TestXmlStrings.ValidSitesResponseXml, Encoding.UTF8, "application/xml")
            }))
            {
                var client = Helper.CreateClient();
                var result = Site.List(client).Result;
                if (server.Error != null) throw server.Error;
                Assert.Single(result);
                Assert.Equal("1", result[0].Id);
                Assert.Equal("Test Site", result[0].Name);
                Assert.Equal("A site description", result[0].Description);

            }
        }

        [Fact]
        public void ListWithDefaultClientTest()
        {
            var items = new[]
            {
                new Site
                {
                    Id = "1",
                    Name = "Name1",
                    Address = new Address
                    {
                        City = "City1",
                        Country = "Country1"
                    }
                },
                new Site
                {
                    Id = "2",
                    Name = "Name2",
                    Address = new Address
                    {
                        City = "City2",
                        Country = "Country2"
                    }
                }
            };
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/sites", Helper.AccountId),
                ContentToSend = Helper.CreateXmlContent(new SitesResponse { Sites = items })
            }))
            {
                var result = Site.List().Result;
                if (server.Error != null) throw server.Error;
                Helper.AssertObjects(items[0], result[0]);
                Helper.AssertObjects(items[1], result[1]);
            }
        }

        [Fact]
        public void CreateTest()
        {
            var item = new Site
            {
                Name = "Name",
                Address = new Address
                {
                    City = "City",
                    StateCode = "State"
                }
            };

            using (var server = new HttpServer(new[]
            {
                new RequestHandler
                {
                    EstimatedMethod = "POST",
                    EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/sites", Helper.AccountId),
                    EstimatedContent = Helper.ToXmlString(item),
                    HeadersToSend =
                        new Dictionary<string, string>
                        {
                            {"Location", string.Format("/v1.0/accounts/{0}/sites/1", Helper.AccountId)}
                        }
                },
                new RequestHandler
                {
                    EstimatedMethod = "GET",
                    EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/sites/1", Helper.AccountId),
                    ContentToSend = Helper.CreateXmlContent(new SiteResponse{Site = new Site {Id = "1"}})
                }
            }))
            {
                var client = Helper.CreateClient();
                var i = Site.Create(client, item).Result;
                if (server.Error != null) throw server.Error;
                Assert.Equal("1", i.Id);


            }

        }

        [Fact]
        public void CreateWithDefaultClientTest()
        {
            var item = new Site
            {
                Name = "Name",
                Address = new Address
                {
                    City = "City",
                    StateCode = "State"
                }
            };

            using (var server = new HttpServer(new[]
            {
                new RequestHandler
                {
                    EstimatedMethod = "POST",
                    EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/sites", Helper.AccountId),
                    EstimatedContent = Helper.ToXmlString(item),
                    HeadersToSend =
                        new Dictionary<string, string>
                        {
                            {"Location", string.Format("/v1.0/accounts/{0}/sites/1", Helper.AccountId)}
                        }
                },
                new RequestHandler
                {
                    EstimatedMethod = "GET",
                    EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/sites/1", Helper.AccountId),
                    ContentToSend = Helper.CreateXmlContent(new SiteResponse{Site = new Site {Id = "1"}})
                }
            }))
            {
                var i = Site.Create(item).Result;
                if (server.Error != null) throw server.Error;
                Assert.Equal("1", i.Id);
            }
        }

        [Fact]
        public void UpdateTest()
        {
            var item = new Site
            {
                Name = "Name",
                Address = new Address
                {
                    City = "City",
                    StateCode = "State"
                }
            };

            using (var server = new HttpServer(new[]
            {
                new RequestHandler
                {
                    EstimatedMethod = "PUT",
                    EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/sites/1", Helper.AccountId),
                    EstimatedContent = Helper.ToXmlString(item)
                }
            }))
            {
                var client = Helper.CreateClient();
                var i = new Site { Id = "1" };
                i.SetClient(client);
                i.Update(item).Wait();
                if (server.Error != null) throw server.Error;
            }
        }

        [Fact]
        public void DeleteTest()
        {
            using (var server = new HttpServer(new[]
            {
                new RequestHandler
                {
                    EstimatedMethod = "DELETE",
                    EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/sites/1", Helper.AccountId),
                }
            }))
            {
                var client = Helper.CreateClient();
                var i = new Site { Id = "1" };
                i.SetClient(client);
                i.Delete().Wait();
                if (server.Error != null) throw server.Error;
            }
        }

        [Fact]
        public void CreateSipPeerTest()
        {
            var item = new SipPeer
            {
                Name = "test",
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
                var i = new Site { Id = "1" };
                i.SetClient(client);
                var r = i.CreateSipPeer(item).Result;
                Helper.AssertObjects(createdItem, r);
                if (server.Error != null) throw server.Error;
            }
        }

        [Fact]
        public void GetSipPeerTest()
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
                var i = new Site { Id = "1" };
                i.SetClient(client);
                var r = i.GetSipPeer("10").Result;
                if (server.Error != null) throw server.Error;
                Helper.AssertObjects(item, r);

            }
        }

        [Fact]
        public void GetSipPeerWithXmlTest()
        {
            using (var server = new HttpServer(new[]
            {
                new RequestHandler
                {
                    EstimatedMethod = "GET",
                    EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/sites/1/sippeers/10", Helper.AccountId),
                    ContentToSend = new StringContent(TestXmlStrings.ValidSipPeerResponseXml, Encoding.UTF8, "application/xml")
                }
            }))
            {
                var client = Helper.CreateClient();
                var i = new Site { Id = "1" };
                i.SetClient(client);
                var r = i.GetSipPeer("10").Result;
                if (server.Error != null) throw server.Error;
                Assert.Equal("10", r.Id);
                Assert.Equal("SIP Peer 1", r.Name);
                Assert.Equal("Sip Peer 1 description", r.Description);
                Assert.True(r.IsDefaultPeer);
                Assert.Equal("SIP", r.ShortMessagingProtocol);
            }
        }

        [Fact]
        public void GetSipPeersTest()
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
                var i = new Site { Id = "1" };
                i.SetClient(client);
                var r = i.GetSipPeers().Result;
                if (server.Error != null) throw server.Error;
                Helper.AssertObjects(items[0], r[0]);
                Helper.AssertObjects(items[1], r[1]);
            }
        }

        [Fact]
        public void GetSipPeersWithXmlTest()
        {
            using (var server = new HttpServer(new[]
            {
                new RequestHandler
                {
                    EstimatedMethod = "GET",
                    EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/sites/1/sippeers", Helper.AccountId),
                    ContentToSend = new StringContent(TestXmlStrings.ValidSipPeersResponseXml, Encoding.UTF8, "application/xml")
                }
            }))
            {
                var client = Helper.CreateClient();
                var i = new Site { Id = "1" };
                i.SetClient(client);
                var r = i.GetSipPeers().Result[0];
                if (server.Error != null) throw server.Error;
                Assert.Equal("12345", r.Id);
                Assert.Equal("SIP Peer 1", r.Name);
                Assert.Equal("Sip Peer 1 description", r.Description);
                Assert.True(r.IsDefaultPeer);
                Assert.Equal("SIP", r.ShortMessagingProtocol);
                Assert.Equal("70.62.112.156", r.VoiceHosts[0].HostName);
                Assert.Equal("70.62.112.156", r.SmsHosts[0].HostName);
                Assert.Equal("70.62.112.156", r.TerminationHosts[0].HostName);
                Assert.Equal(5060, r.TerminationHosts[0].Port);
                Assert.Equal("DOMESTIC", r.TerminationHosts[0].CustomerTrafficAllowed);
                Assert.True(r.TerminationHosts[0].DataAllowed);
                Assert.False(r.CallingName.Enforced);
                Assert.True(r.CallingName.Display);
            }
        }

        [Fact]
        public void BandwidthIrisExcpetionTest()
        {


            var item = new Site
            {
                Name = "Name",
                Address = new Address
                {
                    City = "City",
                    StateCode = "State"
                }
            };

            using (var server = new HttpServer(new[]
            {
                new RequestHandler
                {
                    EstimatedMethod = "GET",
                    EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/sites/1", Helper.AccountId),
                    ContentToSend = new StringContent(TestXmlStrings.xmlError, Encoding.UTF8, "application/xml")
                }
            }))
            {
                var client = Helper.CreateClient();
                var i = Site.Get("1").Result;
                Assert.Equal("Csharp Test Site", i.Name);
            }
        }
    }

}
