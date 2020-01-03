using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Bandwidth.Iris.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bandwidth.Iris.Tests.Models
{
    [TestClass]
    public class SiteTests
    {
        [TestInitialize]
        public void Setup()
        {
            Helper.SetEnvironmetVariables();
        }

        [TestMethod]
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
                ContentToSend = Helper.CreateXmlContent(new SiteResponse{Site = item})
            }))
            {
                var client = Helper.CreateClient();
                var result = Site.Get(client, "1").Result;
                if (server.Error != null) throw server.Error;
                Helper.AssertObjects(item, result);
            }
        }

        [TestMethod]
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
                Assert.AreEqual("1", result.Id);
                Assert.AreEqual("Test Site", result.Name);
                Assert.AreEqual("A Site Description", result.Description);
                Assert.AreEqual("900", result.Address.HouseNumber);
                Assert.AreEqual("Main Campus Drive", result.Address.StreetName);
                Assert.AreEqual("Raleigh", result.Address.City);
                Assert.AreEqual("NC", result.Address.StateCode);
                Assert.AreEqual("27615", result.Address.Zip);
                Assert.AreEqual("United States", result.Address.Country);
                Assert.AreEqual("Service", result.Address.AddressType);
            }
        }

        [TestMethod]
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

        [TestMethod]
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
                ContentToSend = Helper.CreateXmlContent(new SitesResponse{Sites = items})
            }))
            {
                var client = Helper.CreateClient();
                var result = Site.List(client).Result;
                if (server.Error != null) throw server.Error;
                Helper.AssertObjects(items[0], result[0]);
                Helper.AssertObjects(items[1], result[1]);
            }
        }

        [TestMethod]
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
                Assert.AreEqual(1, result.Length);
                Assert.AreEqual("1", result[0].Id);
                Assert.AreEqual("Test Site", result[0].Name);
                Assert.AreEqual("A site description", result[0].Description);
                
            }
        }

        [TestMethod]
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

        [TestMethod]
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
                Assert.AreEqual("1", i.Id);

            
            }

        }

        [TestMethod]
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
                Assert.AreEqual("1", i.Id);
            }
        }

        [TestMethod]
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
                var i = new Site {Id = "1"};
                i.SetClient(client);
                i.Update(item).Wait();
                if (server.Error != null) throw server.Error;
            }
        }

        [TestMethod]
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

        [TestMethod]
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

        [TestMethod]
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

        [TestMethod]
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
                Assert.AreEqual("10", r.Id);
                Assert.AreEqual("SIP Peer 1", r.Name);
                Assert.AreEqual("Sip Peer 1 description", r.Description);
                Assert.IsTrue(r.IsDefaultPeer);
                Assert.AreEqual("SIP", r.ShortMessagingProtocol);
            }
        }

        [TestMethod]
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

        [TestMethod]
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
                Assert.AreEqual("12345", r.Id);
                Assert.AreEqual("SIP Peer 1", r.Name);
                Assert.AreEqual("Sip Peer 1 description", r.Description);
                Assert.IsTrue(r.IsDefaultPeer);
                Assert.AreEqual("SIP", r.ShortMessagingProtocol);
                Assert.AreEqual("70.62.112.156", r.VoiceHosts[0].HostName);
                Assert.AreEqual("70.62.112.156", r.SmsHosts[0].HostName);
                Assert.AreEqual("70.62.112.156", r.TerminationHosts[0].HostName);
                Assert.AreEqual(5060, r.TerminationHosts[0].Port);
                Assert.AreEqual("DOMESTIC", r.TerminationHosts[0].CustomerTrafficAllowed);
                Assert.IsTrue(r.TerminationHosts[0].DataAllowed);
                Assert.IsFalse(r.CallingName.Enforced);
                Assert.IsTrue(r.CallingName.Display);
            }
        }

        [TestMethod]
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

                bool error = false;
                try
                {
                    var i = Site.Get("1").Result;
                } catch (AggregateException e)
                {
                    e.Handle((x) =>
                    {
                        if(x is BandwidthIrisException)
                        {
                            error = true; 
                            return true;
                        }

                        return false;
                    });

                }
                Assert.AreEqual(true, error);


            }
        }
    }

}
