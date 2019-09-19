using System;
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
                       Country = "County"
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
                        Country = "County"
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
                        Country = "County"
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

        [TestMethod]
        public void PutFileMetadataTest()
        {
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "PUT",
                EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/portins/1/loas/test/metadata", Helper.AccountId),
                EstimatedContent = TestXmlStrings.FileMetadataPut
            }))
            {

                var fileMetadata = new FileMetadata {
                    DocumentType = "INVOICE",
                    DocumentName = "docName"
                };
                var client = Helper.CreateClient();
                var portIn = new PortIn { Id = "1" };
                portIn.SetClient(client);
                var r = portIn.PutFileMetadata("test", fileMetadata).Result;
                if (server.Error != null) throw server.Error;
                Assert.AreEqual("OK", r.StatusCode.ToString());
            }
        }

        [TestMethod]
        public void DeleteTest()
        {
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "DELETE",
                EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/portins/1", Helper.AccountId)
            }))
            {
                var client = Helper.CreateClient();
                var portIn = new PortIn { Id = "1" };
                portIn.SetClient(client);
                portIn.Delete().Wait();
                if (server.Error != null) throw server.Error;
            }
        }

        [TestMethod]
        public void UpdateTest()
        {
            var data = new LnpOrderSupp
            {
                RequestedFocDate = DateTime.Parse("2014-11-18T00:00:00.000Z"),
                WirelessInfo = new []{new WirelessInfo
                {
                    AccountNumber = "77129766500001",
                    PinNumber = "0000"
                }}
            };
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "PUT",
                EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/portins/1", Helper.AccountId),
                EstimatedContent = Helper.ToXmlString(data)
            }))
            {
                var client = Helper.CreateClient();
                var portIn = new PortIn { Id = "1" };
                portIn.SetClient(client);
                portIn.Update(data).Wait();
                if (server.Error != null) throw server.Error;
            }
        }

        [TestMethod]
        public void GetNotesTest()
        {
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/portins/1/notes", Helper.AccountId),
                ContentToSend = new StringContent(TestXmlStrings.NotesResponse, Encoding.UTF8, "application/xml")
            }))
            {
                var client = Helper.CreateClient();
                var portIn = new PortIn { Id = "1" };
                portIn.SetClient(client);
                var list = portIn.GetNotes().Result;
                if (server.Error != null) throw server.Error;
                Assert.AreEqual(2, list.Length);
                Assert.AreEqual("11299", list[0].Id);
                Assert.AreEqual("customer", list[0].UserId);
                Assert.AreEqual("Test", list[0].Description);
                Assert.AreEqual("11301", list[1].Id);
                Assert.AreEqual("customer", list[1].UserId);
                Assert.AreEqual("Test1", list[1].Description);
            }
        }

        [TestMethod]
        public void AddNoteTest()
        {
            var item = new Note
            {
                UserId = "customer",
                Description = "Test"
            };
            using (var server = new HttpServer(new []{
                new RequestHandler
                {
                    EstimatedMethod = "POST",
                    EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/portins/1/notes", Helper.AccountId),
                    EstimatedContent = Helper.ToXmlString(item),
                    HeadersToSend = new Dictionary<string, string> {
                        {"Location", string.Format("/v1.0/accounts/{0}/portins/1/notes/11299", Helper.AccountId)} 
                    }
                },
                new RequestHandler
                {
                    EstimatedMethod = "GET",
                    EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/portins/1/notes", Helper.AccountId),
                    ContentToSend = new StringContent(TestXmlStrings.NotesResponse, Encoding.UTF8, "application/xml")
                }
            }))
            {
                var client = Helper.CreateClient();
                var portIn = new PortIn { Id = "1" };
                portIn.SetClient(client);
                var r = portIn.AddNote(item).Result;
                if (server.Error != null) throw server.Error;
                Assert.AreEqual("11299", r.Id);
                Assert.AreEqual("customer", r.UserId);
                Assert.AreEqual("Test", r.Description);
            }
        }

        [TestMethod]
        public void LnpOrderResponseTest()
        {
            string xmlLnpOrderResponse = "<LnpOrderResponse><Errors><Code>4000</Code><Description>generic error</Description></Errors><Errors><Code>7205</Code><Description>Telephonenumberisalreadybeingprocessedonanotherorder</Description></Errors><ProcessingStatus>CANCELLED</ProcessingStatus><RequestedFocDate>2016-03-25T21:15:00.000Z</RequestedFocDate><CustomerOrderId>SJM00002</CustomerOrderId><LoaAuthorizingPerson>TheAuthguy</LoaAuthorizingPerson><Subscriber><SubscriberType>BUSINESS</SubscriberType><FirstName>First</FirstName><LastName>Last</LastName><ServiceAddress><HouseNumber>11235</HouseNumber><StreetName>Back</StreetName><City>Denver</City><StateCode>CO</StateCode><Zip>27541</Zip><County>Canyon</County><Country>UnitedStates</Country><AddressType>Service</AddressType></ServiceAddress></Subscriber><WirelessInfo><AccountNumber>771297665AABC</AccountNumber><PinNumber>1234</PinNumber></WirelessInfo><TnAttributes><TnAttribute>Protected</TnAttribute></TnAttributes><BillingTelephoneNumber>9195551234</BillingTelephoneNumber><NewBillingTelephoneNumber>9175131245</NewBillingTelephoneNumber><ListOfPhoneNumbers><PhoneNumber>9194809871</PhoneNumber></ListOfPhoneNumbers><AlternateSpid>Foo</AlternateSpid><AccountId>20</AccountId><SiteId>2857</SiteId><PeerId>317771</PeerId><LosingCarrierName>MockCarrier</LosingCarrierName><VendorName>BandwidthCLEC</VendorName><OrderCreateDate>2014-08-04T13:37:06.323Z</OrderCreateDate><LastModifiedDate>2014-08-04T13:37:08.676Z</LastModifiedDate><userId>jbm</userId><LastModifiedBy>jbm</LastModifiedBy><PartialPort>false</PartialPort><Triggered>false</Triggered><PortType>AUTOMATED</PortType></LnpOrderResponse>";

            LnpOrderResponse lnpOrderResponse = Helper.ParseXml<LnpOrderResponse>(xmlLnpOrderResponse);

            Assert.AreEqual("SJM00002", lnpOrderResponse.CustomerOrderId);
            Assert.AreEqual("SJM00002", lnpOrderResponse.ProcessingStatus);
            Assert.AreEqual(DateTime.Parse("2016-03-25T21:15:00.000Z"), lnpOrderResponse.RequestedFocDate);
            Assert.AreEqual("The Authguy", lnpOrderResponse.LoaAuthorizingPerson);
            Assert.AreEqual("9195551234", lnpOrderResponse.BillingTelephoneNumber);
            Assert.AreEqual("9175131245", lnpOrderResponse.NewBillingTelephoneNumber);
            Assert.AreEqual("Foo", lnpOrderResponse.AlternateSpid);
            Assert.AreEqual("SJM00002", lnpOrderResponse.AccountId);
            Assert.AreEqual("SJM00002", lnpOrderResponse.SiteId);
            Assert.AreEqual("SJM00002", lnpOrderResponse.PeerId);
            Assert.AreEqual("SJM00002", lnpOrderResponse.LosingCarrierName);
            Assert.AreEqual("SJM00002", lnpOrderResponse.VendorName);
            Assert.AreEqual("SJM00002", lnpOrderResponse.OrderCreateDate);
            Assert.AreEqual("SJM00002", lnpOrderResponse.LastModifiedDate);
            Assert.AreEqual("SJM00002", lnpOrderResponse.UserId);
            Assert.AreEqual("SJM00002", lnpOrderResponse.LastModifiedBy);
            Assert.AreEqual("SJM00002", lnpOrderResponse.PartialPort);
            Assert.AreEqual("SJM00002", lnpOrderResponse.Triggered);
            Assert.AreEqual("SJM00002", lnpOrderResponse.PortType);
        }
    }
}
