using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Bandwidth.Iris.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bandwidth.Iris.Tests.Models
{
    [TestClass]
    public class PortInTests
    {
        [TestInitialize]
        public void Setup()
        {
            Helper.SetEnvironmetVariables();
        }

        [TestMethod]
        public void CreateTest()
        {
            var order = new LnpOrderResponse
            {
                BillingTelephoneNumber = "1111",
                Subscriber = new Subscriber
                {
                   SubscriberType = "BUSINESS",
                   BusinessName = "Company",
                   ServiceAddress = new Address
                   {
                       City = "City",
                       StateCode = "State",
                       Country = "Country"
                   }
                },
                SiteId = "1"                 
            };
            
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "POST",
                EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/portins", Helper.AccountId),
                EstimatedContent = Helper.ToXmlString(order),
                ContentToSend = Helper.CreateXmlContent(order)
            }))
            {
                var client = Helper.CreateClient();
                var r = PortIn.Create(client, order).Result;
                if (server.Error != null) throw server.Error;
                Helper.AssertObjects(order, r);
            }
        }

        [TestMethod]
        public void CreateWithXmlTest()
        {
            var order = new PortIn
            {
                BillingTelephoneNumber = "1111",
                Subscriber = new Subscriber
                {
                    SubscriberType = "BUSINESS",
                    BusinessName = "Company",
                    ServiceAddress = new Address
                    {
                        City = "City",
                        StateCode = "State",
                        Country = "Country"
                    }
                },
                SiteId = "1"
            };

            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "POST",
                EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/portins", Helper.AccountId),
                EstimatedContent = Helper.ToXmlString(order),
                ContentToSend = new StringContent(TestXmlStrings.ValidCreatePostInResponse, Encoding.UTF8, "application/xml")
            }))
            {
                var client = Helper.CreateClient();
                var r = PortIn.Create(client, order).Result;
                if (server.Error != null) throw server.Error;
                Assert.AreEqual("d28b36f7-fa96-49eb-9556-a40fca49f7c6", r.Id);
                Assert.AreEqual("201", r.Status.Code);
                Assert.AreEqual("Order request received. Please use the order id to check the status of your order later.", r.Status.Description);
                Assert.AreEqual("PENDING_DOCUMENTS", r.ProcessingStatus);
                Assert.AreEqual("John Doe", r.LoaAuthorizingPerson);
                Assert.AreEqual("6882015002", r.BillingTelephoneNumber);
                CollectionAssert.AreEqual(new[] { "6882015025", "6882015026" }, r.ListOfPhoneNumbers);
                Assert.IsFalse(r.Triggered);
                Assert.AreEqual("PORTIN", r.BillingType);
                
            }
        }
        [TestMethod]
        public void CreateWithDefaultClientTest()
        {
            var order = new LnpOrderResponse
            {
                BillingTelephoneNumber = "1111",
                Subscriber = new Subscriber
                {
                    SubscriberType = "BUSINESS",
                    BusinessName = "Company",
                    ServiceAddress = new Address
                    {
                        City = "City",
                        StateCode = "State",
                        Country = "Country"
                    }
                },
                SiteId = "1"
            };

            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "POST",
                EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/portins", Helper.AccountId),
                EstimatedContent = Helper.ToXmlString(order),
                ContentToSend = Helper.CreateXmlContent(order)
            }))
            {
                var r = PortIn.Create(order).Result;
                if (server.Error != null) throw server.Error;
                Helper.AssertObjects(order, r);
            }
        }

        public class FileResult
        {
            [XmlElementAttribute("filename")]
            public string FileName { get; set; }
        }
        [TestMethod]
        public void CreateFileTest()
        {
            const string data = "hello";
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "POST",
                EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/portins/1/loas", Helper.AccountId),
                EstimatedContent = data,
                EstimatedHeaders = new Dictionary<string, string> { { "Content-Type", "media/type" } },
                ContentToSend = Helper.CreateXmlContent(new FileResult{FileName = "test"})
            }))
            {
                var client = Helper.CreateClient();
                var portIn = new PortIn{Id = "1"};
                portIn.SetClient(client);
                using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(data)))
                {
                    var fileName = portIn.CreateFile(stream, "media/type").Result;
                    if (server.Error != null) throw server.Error;
                    Assert.AreEqual("test", fileName);
                }
            }
        }

        [TestMethod]
        public void CreateFile2Test()
        {
            const string data = "hello";
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "POST",
                EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/portins/1/loas", Helper.AccountId),
                EstimatedContent = data,
                EstimatedHeaders = new Dictionary<string, string> { { "Content-Type", "media/type" } },
                ContentToSend = Helper.CreateXmlContent(new FileResult { FileName = "test" })
            }))
            {
                var client = Helper.CreateClient();
                var portIn = new PortIn { Id = "1" };
                portIn.SetClient(client);
                var fileName = portIn.CreateFile(Encoding.UTF8.GetBytes(data), "media/type").Result;
                if (server.Error != null) throw server.Error;
                Assert.AreEqual("test", fileName);
            }
        }

        [TestMethod]
        public void UpdateFileTest()
        {
            const string data = "hello";
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "PUT",
                EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/portins/1/loas/test", Helper.AccountId),
                EstimatedContent = data,
                EstimatedHeaders = new Dictionary<string, string> { { "Content-Type", "media/type" } }
            }))
            {
                var client = Helper.CreateClient();
                var portIn = new PortIn { Id = "1" };
                portIn.SetClient(client);
                using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(data)))
                {
                    portIn.UpdateFile("test", stream, "media/type").Wait();
                    if (server.Error != null) throw server.Error;
                }
            }
        }

        [TestMethod]
        public void UpdateFile2Test()
        {
            const string data = "hello";
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "PUT",
                EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/portins/1/loas/test", Helper.AccountId),
                EstimatedContent = data,
                EstimatedHeaders = new Dictionary<string, string> { { "Content-Type", "media/type" } }
            }))
            {
                var client = Helper.CreateClient();
                var portIn = new PortIn { Id = "1" };
                portIn.SetClient(client);
                portIn.UpdateFile("test", Encoding.UTF8.GetBytes(data), "media/type").Wait();
                if (server.Error != null) throw server.Error;
            }
        }

        [TestMethod]
        public void DeleteFileTest()
        {
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "DELETE",
                EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/portins/1/loas/test", Helper.AccountId)
            }))
            {
                var client = Helper.CreateClient();
                var portIn = new PortIn { Id = "1" };
                portIn.SetClient(client);
                portIn.DeleteFile("test").Wait();
                if (server.Error != null) throw server.Error;
            }
        }

        [TestMethod]
        public void GetFileTest()
        {
            const string data = "hello";
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/portins/1/loas/test", Helper.AccountId),
                ContentToSend = new StringContent(data, Encoding.UTF8, "media/type")
            }))
            {
                var client = Helper.CreateClient();
                var portIn = new PortIn { Id = "1" };
                portIn.SetClient(client);
                using (var r = portIn.GetFile("test").Result)
                {
                    Assert.AreEqual("media/type", r.MediaType);
                    Assert.AreEqual(data, Encoding.UTF8.GetString(r.Buffer));
                }
                if (server.Error != null) throw server.Error;
            }
        }

        [TestMethod]
        public void GetFile2Test()
        {
            const string data = "hello";
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/portins/1/loas/test", Helper.AccountId),
                ContentToSend = new StringContent(data, Encoding.UTF8, "media/type")
            }))
            {
                var client = Helper.CreateClient();
                var portIn = new PortIn { Id = "1" };
                portIn.SetClient(client);
                using (var r = portIn.GetFile("test", true).Result)
                using(var reader = new StreamReader(r.Stream, Encoding.UTF8))
                {
                    Assert.AreEqual("media/type", r.MediaType);
                    Assert.AreEqual(data, reader.ReadToEnd());
                }
                if (server.Error != null) throw server.Error;
            }
        }

        [TestMethod]
        public void GetFilesTest()
        {
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/portins/1/loas?metadata=true", Helper.AccountId),
                ContentToSend = new StringContent(TestXmlStrings.FileListResponse, Encoding.UTF8, "application/xml")
            }))
            {
                var client = Helper.CreateClient();
                var portIn = new PortIn { Id = "1" };
                portIn.SetClient(client);
                var list = portIn.GetFiles(true).Result;
                if (server.Error != null) throw server.Error;
                Assert.AreEqual(6, list.Length);
                Assert.AreEqual("d28b36f7-fa96-49eb-9556-a40fca49f7c6-1416231534986.txt", list[0].FileName);
                Assert.AreEqual("LOA", list[0].FileMetadata.DocumentType);
            }
        }

        [TestMethod]
        public void GetFileMetadataTest()
        {
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/portins/1/loas/test/metadata", Helper.AccountId),
                ContentToSend = new StringContent(TestXmlStrings.FileMetadataResponse, Encoding.UTF8, "application/xml")
            }))
            {
                var client = Helper.CreateClient();
                var portIn = new PortIn { Id = "1" };
                portIn.SetClient(client);
                var r = portIn.GetFileMetadata("test").Result;
                if (server.Error != null) throw server.Error;
                Assert.AreEqual("LOA", r.DocumentType);
            }
        }
    }
}
