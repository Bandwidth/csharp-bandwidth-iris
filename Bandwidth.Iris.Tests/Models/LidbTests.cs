using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Bandwidth.Iris.Model;
using Xunit;

namespace Bandwidth.Iris.Tests.Models
{
    
    public class LidbTests
    {
        // [TestInitialize]
        public void Setup()
        {
            Helper.SetEnvironmetVariables();
        }

        [Fact]
        public void GetTest()
        {
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/lidbs/1", Helper.AccountId),
                ContentToSend = new StringContent(TestXmlStrings.Lidb, Encoding.UTF8, "application/xml")
            }))
            {
                var client = Helper.CreateClient();
                var result = Lidb.Get(client, "1").Result;
                if (server.Error != null) throw server.Error;
                Assert.Equal("255bda29-fc57-44e8-a6c2-59b45388c6d0", result.Id);
            }
        }
               

        [Fact]
        public void GetWithDefaultClientTest()
        {
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/lidbs/1", Helper.AccountId),
                ContentToSend = new StringContent(TestXmlStrings.Lidb, Encoding.UTF8, "application/xml")
            }))
            {
                var result = Lidb.Get("1").Result;
                if (server.Error != null) throw server.Error;
                Assert.Equal("255bda29-fc57-44e8-a6c2-59b45388c6d0", result.Id);
            }
        }

        [Fact]
        public void ListTest()
        {
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/lidbs", Helper.AccountId),
                ContentToSend = new StringContent(TestXmlStrings.Lidbs, Encoding.UTF8, "application/xml")
            }))
            {
                var client = Helper.CreateClient();
                var result = Lidb.List(client).Result;
                if (server.Error != null) throw server.Error;
                Assert.Equal(2, result.Length);
            }
        }


        [Fact]
        public void ListWithDefaultClientTest()
        {
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/lidbs", Helper.AccountId),
                ContentToSend = new StringContent(TestXmlStrings.Lidbs, Encoding.UTF8, "application/xml")
            }))
            {
                var result = Lidb.List().Result;
                if (server.Error != null) throw server.Error;
                Assert.Equal(2, result.Length);
            }
        }

        [Fact]
        public void CreateTest()
        {
            var item = new Lidb
            {
                CustomerOrderId = "A Test order",
                LidbTnGroups = new[] { 
                    new LidbTnGroup{
                        TelephoneNumbers = new []{"8048030097", "8045030098"},
                        SubscriberInformation = "Joes Grarage",
                        UseType = "RESIDENTIAL",
                        Visibility = "PUBLIC"
                    }
                } 
            };

            using (var server = new HttpServer(new[]
            {
                new RequestHandler
                {
                    EstimatedMethod = "POST",
                    EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/lidbs", Helper.AccountId),
                    EstimatedContent = Helper.ToXmlString(item),
                    HeadersToSend =
                        new Dictionary<string, string>
                        {
                            {"Location", string.Format("/v1.0/accounts/{0}/lidbs/1", Helper.AccountId)}
                        }
                },
                new RequestHandler
                {
                    EstimatedMethod = "GET",
                    EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/lidbs/1", Helper.AccountId),
                    ContentToSend = new StringContent(TestXmlStrings.Lidb, Encoding.UTF8, "application/xml")
                }
            }))
            {
                var client = Helper.CreateClient();
                var i = Lidb.Create(client, item).Result;
                if (server.Error != null) throw server.Error;
                Assert.Equal("255bda29-fc57-44e8-a6c2-59b45388c6d0", i.Id);
            }

        }

        [Fact]
        public void CreateWithDefaultClientTest()
        {
            var item = new Lidb
            {
                CustomerOrderId = "A Test order",
                LidbTnGroups = new[] { 
                    new LidbTnGroup{
                        TelephoneNumbers = new []{"8048030097", "8045030098"},
                        SubscriberInformation = "Joes Grarage",
                        UseType = "RESIDENTIAL",
                        Visibility = "PUBLIC"
                    }
                }
            };

            using (var server = new HttpServer(new[]
            {
                new RequestHandler
                {
                    EstimatedMethod = "POST",
                    EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/lidbs", Helper.AccountId),
                    EstimatedContent = Helper.ToXmlString(item),
                    HeadersToSend =
                        new Dictionary<string, string>
                        {
                            {"Location", string.Format("/v1.0/accounts/{0}/lidbs/1", Helper.AccountId)}
                        }
                },
                new RequestHandler
                {
                    EstimatedMethod = "GET",
                    EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/lidbs/1", Helper.AccountId),
                    ContentToSend = new StringContent(TestXmlStrings.Lidb, Encoding.UTF8, "application/xml")
                }
            }))
            {
                var i = Lidb.Create(item).Result;
                if (server.Error != null) throw server.Error;
                Assert.Equal("255bda29-fc57-44e8-a6c2-59b45388c6d0", i.Id);
            }
        }
    }
}
