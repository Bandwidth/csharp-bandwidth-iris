using System.Collections.Generic;
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
                    Country = "Country"
                }
            };
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/sites/1", Helper.AccountId),
                ContentToSend = Helper.CreateXmlContent(item)
            }))
            {
                var client = Helper.CreateClient();
                var result = Site.Get(client, "1").Result;
                if (server.Error != null) throw server.Error;
                Helper.AssertObjects(item, result);
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
                    Country = "Country"
                }
            };
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/sites/1", Helper.AccountId),
                ContentToSend = Helper.CreateXmlContent(item)
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
                ContentToSend = Helper.CreateXmlContent(items)
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
                ContentToSend = Helper.CreateXmlContent(items)
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
                    ContentToSend = Helper.CreateXmlContent(new Site {Id = "1"})
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
                    ContentToSend = Helper.CreateXmlContent(new Site {Id = "1"})
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
    }

}
