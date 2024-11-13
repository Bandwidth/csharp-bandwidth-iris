using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Bandwidth.Iris.Model;
using Xunit;

namespace Bandwidth.Iris.Tests.Models
{

    public class SubscriptionTests
    {
        // [TestInitialize]
        public SubscriptionTests()
        {
            Helper.SetEnvironmentVariables();
        }

        [Fact]
        public void GetTest()
        {
            var item = new Subscription
            {
                Id = "1",
                OrderType = "orders",
                OrderId = "100",
                EmailSubscription = new EmailSubscription
                {
                    Email = "test@test",
                    DigestRequested = "NONE"
                }
            };
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/subscriptions/1", Helper.AccountId),
                ContentToSend = Helper.CreateXmlContent(new SubscriptionsResponse { Subscriptions = new[] { item } })
            }))
            {
                var client = Helper.CreateClient();
                var result = Subscription.Get(client, "1").Result;
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
                EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/subscriptions/1", Helper.AccountId),
                ContentToSend = new StringContent(TestXmlStrings.SubscriptionResponse, Encoding.UTF8, "application/xml")
            }))
            {
                var client = Helper.CreateClient();
                var result = Subscription.Get(client, "1").Result;
                if (server.Error != null) throw server.Error;
                Assert.Equal("1", result.Id);
                Assert.Equal("orders", result.OrderType);
                Assert.Equal("8684b1c8-7d41-4877-bfc2-6bd8ea4dc89f", result.OrderId);
                Assert.Equal("test@test", result.EmailSubscription.Email);
                Assert.Equal("NONE", result.EmailSubscription.DigestRequested);
            }
        }

        [Fact]
        public void GetWithDefaultClientTest()
        {
            var item = new Subscription
            {
                Id = "1",
                OrderType = "orders",
                OrderId = "100",
                EmailSubscription = new EmailSubscription
                {
                    Email = "test@test",
                    DigestRequested = "NONE"
                }
            };
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/subscriptions/1", Helper.AccountId),
                ContentToSend = Helper.CreateXmlContent(new SubscriptionsResponse { Subscriptions = new[] { item } })
            }))
            {
                var result = Subscription.Get("1").Result;
                if (server.Error != null) throw server.Error;
                Helper.AssertObjects(item, result);
            }
        }

        [Fact]
        public void ListTest()
        {
            var items = new[]
            {
                new Subscription
                {
                    Id = "1",
                    OrderType = "orders",
                    OrderId = "100",
                    EmailSubscription = new EmailSubscription
                    {
                        Email = "test@test",
                        DigestRequested = "NONE"
                    }
                },
                new Subscription
                {
                    Id = "2",
                    OrderType = "orders",
                    OrderId = "101",
                    EmailSubscription = new EmailSubscription
                    {
                        Email = "test1@test",
                        DigestRequested = "NONE"
                    }
                }
            };
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/subscriptions", Helper.AccountId),
                ContentToSend = Helper.CreateXmlContent(new SubscriptionsResponse { Subscriptions = items })
            }))
            {
                var client = Helper.CreateClient();
                var result = Subscription.List(client).Result;
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
                EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/subscriptions", Helper.AccountId),
                ContentToSend = new StringContent(TestXmlStrings.SubscriptionResponse, Encoding.UTF8, "application/xml")
            }))
            {
                var client = Helper.CreateClient();
                var result = Subscription.List(client).Result;
                if (server.Error != null) throw server.Error;
                Assert.Single(result);
                Assert.Equal("1", result[0].Id);
                Assert.Equal("orders", result[0].OrderType);
                Assert.Equal("8684b1c8-7d41-4877-bfc2-6bd8ea4dc89f", result[0].OrderId);
                Assert.Equal("test@test", result[0].EmailSubscription.Email);
                Assert.Equal("NONE", result[0].EmailSubscription.DigestRequested);
            }
        }

        [Fact]
        public void ListWithDefaultClientTest()
        {
            var items = new[]
            {
                new Subscription
                {
                    Id = "1",
                    OrderType = "orders",
                    OrderId = "100",
                    EmailSubscription = new EmailSubscription
                    {
                        Email = "test@test",
                        DigestRequested = "NONE"
                    }
                },
                new Subscription
                {
                    Id = "2",
                    OrderType = "orders",
                    OrderId = "101",
                    EmailSubscription = new EmailSubscription
                    {
                        Email = "test1@test",
                        DigestRequested = "NONE"
                    }
                }
            };
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/subscriptions", Helper.AccountId),
                ContentToSend = Helper.CreateXmlContent(new SubscriptionsResponse { Subscriptions = items })
            }))
            {
                var result = Subscription.List().Result;
                if (server.Error != null) throw server.Error;
                Helper.AssertObjects(items[0], result[0]);
                Helper.AssertObjects(items[1], result[1]);
            }
        }

        [Fact]
        public void CreateTest()
        {
            var item = new Subscription
            {
                OrderType = "orders",
                OrderId = "100",
                EmailSubscription = new EmailSubscription
                {
                    Email = "test@test",
                    DigestRequested = "NONE"
                }
            };

            using (var server = new HttpServer(new[]
            {
                new RequestHandler
                {
                    EstimatedMethod = "POST",
                    EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/subscriptions", Helper.AccountId),
                    EstimatedContent = Helper.ToXmlString(item),
                    HeadersToSend =
                        new Dictionary<string, string>
                        {
                            {"Location", string.Format("/v1.0/accounts/{0}/subscriptions/1", Helper.AccountId)}
                        }
                },
                new RequestHandler
                {
                    EstimatedMethod = "GET",
                    EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/subscriptions/1", Helper.AccountId),
                    ContentToSend = Helper.CreateXmlContent(new SubscriptionsResponse{Subscriptions = new []{new Subscription {Id = "1"}}})
                }
            }))
            {
                var client = Helper.CreateClient();
                var i = Subscription.Create(client, item).Result;
                if (server.Error != null) throw server.Error;
                Assert.Equal("1", i.Id);
            }
        }

        [Fact]
        public void CreateWithDefaultClientTest()
        {
            var item = new Subscription
            {
                OrderType = "orders",
                OrderId = "100",
                EmailSubscription = new EmailSubscription
                {
                    Email = "test@test",
                    DigestRequested = "NONE"
                }
            };

            using (var server = new HttpServer(new[]
            {
                new RequestHandler
                {
                    EstimatedMethod = "POST",
                    EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/subscriptions", Helper.AccountId),
                    EstimatedContent = Helper.ToXmlString(item),
                    HeadersToSend =
                        new Dictionary<string, string>
                        {
                            {"Location", string.Format("/v1.0/accounts/{0}/subscriptions/1", Helper.AccountId)}
                        }
                },
                new RequestHandler
                {
                    EstimatedMethod = "GET",
                    EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/subscriptions/1", Helper.AccountId),
                    ContentToSend = Helper.CreateXmlContent(new SubscriptionsResponse{Subscriptions = new []{new Subscription {Id = "1"}}})
                }
            }))
            {
                var i = Subscription.Create(item).Result;
                if (server.Error != null) throw server.Error;
                Assert.Equal("1", i.Id);
            }
        }

        [Fact]
        public void UpdateTest()
        {
            var item = new Subscription
            {
                OrderType = "orders",
                OrderId = "100",
                EmailSubscription = new EmailSubscription
                {
                    Email = "test@test",
                    DigestRequested = "NONE"
                }
            };

            using (var server = new HttpServer(new[]
            {
                new RequestHandler
                {
                    EstimatedMethod = "PUT",
                    EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/subscriptions/1", Helper.AccountId),
                    EstimatedContent = Helper.ToXmlString(item)
                }
            }))
            {
                var client = Helper.CreateClient();
                var i = new Subscription { Id = "1" };
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
                    EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/subscriptions/1", Helper.AccountId),
                }
            }))
            {
                var client = Helper.CreateClient();
                var i = new Subscription { Id = "1" };
                i.SetClient(client);
                i.Delete().Wait();
                if (server.Error != null) throw server.Error;
            }
        }


    }
}
