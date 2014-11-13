using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Bandwidth.Iris.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bandwidth.Iris.Tests
{
    [TestClass]
    public class ClientTests
    {
        [TestMethod]
        public void GetInstanceTest()
        {
            Environment.SetEnvironmentVariable(Client.BandwidthApiAccountId, "AccountId");
            Environment.SetEnvironmentVariable(Client.BandwidthApiUserName, "UserName");
            Environment.SetEnvironmentVariable(Client.BandwidthApiPassword, "Password");
            Environment.SetEnvironmentVariable(Client.BandwidthApiEndpoint, "EndPoint");
            Environment.SetEnvironmentVariable(Client.BandwidthApiVersion, "Version");
            Client.GetInstance();
            Client.GetInstance("accountId", "userName", "password", "endpoint", "version");
            Client.GetInstance("accountId", "userName", "password");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetInstanceTest2()
        {
            Environment.SetEnvironmentVariable(Client.BandwidthApiAccountId, null);
            Environment.SetEnvironmentVariable(Client.BandwidthApiUserName, null);
            Environment.SetEnvironmentVariable(Client.BandwidthApiPassword, null);
            Environment.SetEnvironmentVariable(Client.BandwidthApiEndpoint, null);
            Environment.SetEnvironmentVariable(Client.BandwidthApiVersion, null);
            Client.GetInstance();
        }
        
        [TestMethod]
        public void MakeGetRequestTest()
        {
            using (var server = new HttpServer(new RequestHandler { EstimatedMethod = "GET", EstimatedPathAndQuery = "/v1.0/test?test1=value1&test2=value2" }))
            {
                var client = Helper.CreateClient();
                client.MakeGetRequest("test",
                             new Dictionary<string, object> { { "test1", "value1" }, { "test2", "value2" } }, null, true)
                             .Wait();
                if (server.Error != null) throw server.Error;
            }
            
        }

        
        [TestMethod]
        public void MakeGetRequestWithIdTest()
        {
            using (var server = new HttpServer(new RequestHandler { EstimatedMethod = "GET", EstimatedPathAndQuery = "/v1.0/test/id?test1=value1&test2=value2" }))
            {
                var client = Helper.CreateClient();
                {
                    client.MakeGetRequest("test",
                             new Dictionary<string, object> { { "test1", "value1" }, { "test2", "value2" } }, "id", true)
                             .Wait();
                    if (server.Error != null) throw server.Error;
                }
            }
        }

        public class TestItem
        {
            public string Name { get; set; }
            public bool? Flag { get; set; }
        }
        [TestMethod]
        public void MakeGetRequestTTest()
        {
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET", 
                EstimatedPathAndQuery = "/v1.0/test?test1=value1&test2=value2",
                ContentToSend =  Helper.CreateXmlContent(new TestItem
                {
                    Name = "Name",
                    Flag = true
                })
            }))
            {
                var client = Helper.CreateClient();
                {
                    var result = client.MakeGetRequest<TestItem>("test",
                        new Dictionary<string, object> { { "test1", "value1" }, { "test2", "value2" } }).Result;
                    if (server.Error != null) throw server.Error;
                    Assert.AreEqual("Name", result.Name);
                    Assert.IsTrue(result.Flag != null && result.Flag.Value);
                }
            }
        }

        public class TestModel
        {
            public bool Test { get; set; }
        }

        [TestMethod]
        public void MakePostRequestTest()
        {
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "POST",
                EstimatedPathAndQuery = "/v1.0/test",
                EstimatedContent = Helper.ToXmlString(new TestModel { Test = true })
            }))
            {
                var client = Helper.CreateClient();
                {
                    client.MakePostRequest("test", new TestModel { Test = true }, true).Wait();
                    if (server.Error != null) throw server.Error;
                }
            }
        }

        [TestMethod]
        public void MakePutRequestTest()
        {
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "PUT",
                EstimatedPathAndQuery = "/v1.0/test",
                EstimatedContent = Helper.ToXmlString(new TestModel { Test = true })
            }))
            {
                var client = Helper.CreateClient();
                {
                    client.MakePutRequest("test", new TestModel { Test = true }, true).Wait();
                    if (server.Error != null) throw server.Error;
                }
            }
        }

        [TestMethod]
        public void MakePostRequestTTest()
        {
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "POST",
                EstimatedPathAndQuery = "/v1.0/test",
                EstimatedContent = Helper.ToXmlString(new TestModel{Test = true}),
                ContentToSend = Helper.CreateXmlContent(new TestItem
                {
                    Name = "Name",
                    Flag = true
                })
            }))
            {
                var client = Helper.CreateClient();
                {
                    var result = client.MakePostRequest<TestItem>("test", new TestModel{ Test = true }).Result;
                    if (server.Error != null) throw server.Error;
                    Assert.AreEqual("Name", result.Name);
                    Assert.IsTrue(result.Flag != null && result.Flag.Value);
                }
            }

        }

        [TestMethod]
        public void MakeDeleteRequestTest()
        {
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "DELETE",
                EstimatedPathAndQuery = "/v1.0/test"
            }))
            {
                var client = Helper.CreateClient();
                {
                    client.MakeDeleteRequest("test").Wait();
                    if (server.Error != null) throw server.Error;
                }
            }
        }

        [TestMethod]
        public void AuthHeaderTest()
        {
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedHeaders = new Dictionary<string, string> { { "Authorization", "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Format("{0}:{1}", Helper.UserName, Helper.Password)))} },
                ContentToSend = Helper.CreateXmlContent(new SitesResponse())
            }))
            {
                var client = Helper.CreateClient();
                {
                    Site.List(client).Wait();
                    if (server.Error != null) throw server.Error;
                }
            }
        }
        [TestMethod]
        public void PutDataWithStreamTest()
        {
            var data = Encoding.UTF8.GetBytes("hello");
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "PUT",
                EstimatedPathAndQuery = "/v1.0/test",
                EstimatedContent = "hello",
                EstimatedHeaders = new Dictionary<string, string> {{"Content-Type", "media/type"}}
            }))
            {
                var client = Helper.CreateClient();
                using (var stream = new MemoryStream(data))
                {
                    client.PutData("test", stream, "media/type", true).Wait();
                    if (server.Error != null) throw server.Error;
                }
            }

        }
        [TestMethod]
        public void PutDataWithByteArrayTest()
        {
            var data = Encoding.UTF8.GetBytes("hello");
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "PUT",
                EstimatedPathAndQuery = "/v1.0/test",
                EstimatedContent = "hello",
                EstimatedHeaders = new Dictionary<string, string> { { "Content-Type", "media/type" } }
            }))
            {
                var client = Helper.CreateClient();
                client.PutData("test", data, "media/type", true).Wait();
                if (server.Error != null) throw server.Error;
            }

        }
    }
}
