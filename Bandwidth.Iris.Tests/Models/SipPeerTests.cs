﻿using System.Collections.Generic;
using Bandwidth.Iris.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bandwidth.Iris.Tests.Models
{
    [TestClass]
    public class SipPeerTests
    {
        [TestInitialize]
        public void Setup()
        {
            Helper.SetEnvironmetVariables();
        }
        [TestMethod]
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

        [TestMethod]
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

        [TestMethod]
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

        [TestMethod]
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

        [TestMethod]
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

        [TestMethod]
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

        [TestMethod]
        public void MoveTnsTest()
        {
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "POST",
                EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/sites/1/sippeers/10/movetns", Helper.AccountId),
                EstimatedContent = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n<SipPeerTelephoneNumbers>\r\n  <FullNumber>111</FullNumber>\r\n  <FullNumber>222</FullNumber>\r\n</SipPeerTelephoneNumbers>"
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
    }

}
