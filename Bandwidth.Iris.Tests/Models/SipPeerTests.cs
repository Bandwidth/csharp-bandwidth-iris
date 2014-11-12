using System.Collections.Generic;
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
                EstimatedMethod = "PUT",
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
