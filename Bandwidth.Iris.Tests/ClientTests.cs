using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;
using Bandwidth.Iris.Model;
using Xunit;

namespace Bandwidth.Iris.Tests
{
    public class ClientTests
    {
        [Fact]
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

        // [Fact]
        // public void GetInstanceTest2()
        // {
        //     Environment.SetEnvironmentVariable(Client.BandwidthApiAccountId, null);
        //     Environment.SetEnvironmentVariable(Client.BandwidthApiUserName, null);
        //     Environment.SetEnvironmentVariable(Client.BandwidthApiPassword, null);
        //     Environment.SetEnvironmentVariable(Client.BandwidthApiEndpoint, null);
        //     Environment.SetEnvironmentVariable(Client.BandwidthApiVersion, null);
        //     Client.GetInstance();
        //
        //     Assert.Throws<ArgumentNullException>;
        // }

        [Fact]
        public void MakeGetRequestTest()
        {
            using (var server = new HttpServer(new RequestHandler {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = "/v1.0/test?test1=value1&test2=value2",
                EstimatedHeaders = new Dictionary<string, string>
                {
                    {"User-Agent", Client.USER_AGENT }
                }
            }))
            {
                var client = Helper.CreateClient();
                client.MakeGetRequest("test",
                             new Dictionary<string, object> { { "test1", "value1" }, { "test2", "value2" } }, null, true)
                             .Wait();
                if (server.Error != null) throw server.Error;
            }

        }


        [Fact]
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
        [Fact]
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
                    Assert.Equal("Name", result.Name);
                    Assert.True(result.Flag != null && result.Flag.Value);
                }
            }
        }

        public class TestModel
        {
            public bool Test { get; set; }
        }

        [Fact]
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

        [Fact]
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

        [Fact]
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
                    Assert.Equal("Name", result.Name);
                    Assert.True(result.Flag != null && result.Flag.Value);
                }
            }

        }

        [Fact]
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

        [Fact]
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
        [Fact]
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
                    client.SendData("test",  stream, "media/type", "PUT",  true).Wait();
                    if (server.Error != null) throw server.Error;
                }
            }

        }
        [Fact]
        public void SendDataWithByteArrayTest()
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
                client.SendData("test", new MemoryStream( data), "media/type", "PUT", true).Wait();
                if (server.Error != null) throw server.Error;
            }

        }

        [XmlInclude(typeof(Error1))]
        [XmlInclude(typeof(Error))]
        public class ErrorResponse
        {
            public object Error { get; set; }
        }

        public class Error1
        {
            public string ErrorCode { get; set; }
            public string Description { get; set; }
        }

        public class Error
        {
            public string Code { get; set; }
            public string Description { get; set; }
        }

        public class UploadError
        {
            public string resultCode { get; set; }
            public string resultMessage { get; set; }
        }
        public class ErrorList
        {
            [XmlArrayItem("Errors")]
            public Error[] ErrorMessages { get; set; }
        }

        [Fact]
        public void ErrorTest()
        {
            using (new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                ContentToSend = Helper.CreateXmlContent(new ErrorResponse
                {
                    Error = new Error1
                    {
                        ErrorCode = "1000",
                        Description = "Error text"
                    }
                }),
                StatusCodeToSend = 400
            }))
            {
                var client = Helper.CreateClient();
                try
                {
                    Site.List(client).Wait();
                }
                catch (AggregateException ex)
                {
                    var err = (BandwidthIrisException)ex.InnerExceptions.First();
                    Assert.Equal("1000", err.Code);
                    Assert.Equal("Error text", err.Message);
                    Assert.Equal(HttpStatusCode.BadRequest, err.HttpStatusCode);
                }
            }
        }

        [Fact]
        public void Error2Test()
        {
            using (new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                ContentToSend = Helper.CreateXmlContent(new ErrorResponse
                {
                    Error = new Error
                    {
                        Code = "1001",
                        Description = "Error text"
                    }
                }),
                StatusCodeToSend = 400
            }))
            {
                var client = Helper.CreateClient();
                try
                {
                    Site.List(client).Wait();
                }
                catch (AggregateException ex)
                {
                    var err = (BandwidthIrisException)ex.InnerExceptions.First();
                    Assert.Equal("1001", err.Code);
                    Assert.Equal("Error text", err.Message);
                    Assert.Equal(HttpStatusCode.BadRequest, err.HttpStatusCode);
                }
            }
        }

        [Fact]
        public void Error3Test()
        {
            using (new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                ContentToSend = Helper.CreateXmlContent(new ErrorList
                {
                    ErrorMessages = new[]
                    {
                        new Error
                        {
                            Code = "101",
                            Description = "Description1"
                        },
                        new Error
                        {
                            Code = "102",
                            Description = "Description2"
                        }
                    }
                }),
                StatusCodeToSend = 400
            }))
            {
                var client = Helper.CreateClient();
                try
                {
                    Site.List(client).Wait();
                }
                catch (AggregateException ex)
                {
                    var err = (AggregateException)ex.InnerExceptions.First();
                    var list = (from e in err.InnerExceptions select (BandwidthIrisException) e).ToArray();
                    Assert.Equal("101", list[0].Code);
                    Assert.Equal("Description1", list[0].Message);
                    Assert.Equal("102", list[1].Code);
                    Assert.Equal("Description2", list[1].Message);
                }
            }
        }
        [Fact]
        public void Error4Test()
        {
            using (new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                ContentToSend = Helper.CreateXmlContent(new UploadError
                {
                    resultCode = "1000",
                    resultMessage = "Error text"
                }),
                StatusCodeToSend = 400
            }))
            {
                var client = Helper.CreateClient();
                try
                {
                    Site.List(client).Wait();
                }
                catch (AggregateException ex)
                {
                    var err = (BandwidthIrisException)ex.InnerExceptions.First();
                    Assert.Equal("1000", err.Code);
                    Assert.Equal("Error text", err.Message);
                    Assert.Equal(HttpStatusCode.BadRequest, err.HttpStatusCode);
                }
            }
        }
    }
}
