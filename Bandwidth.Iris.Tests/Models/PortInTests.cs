﻿using System;
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
        public void GetPortInTest()
        {

            var portIn = new PortIn { Id = "1" };

            using (var server = new HttpServer(new[]{
                new RequestHandler
                {
                    EstimatedMethod = "GET",
                    EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/portins/{1}", Helper.AccountId, portIn.Id),
                    EstimatedContent = "",
                    ContentToSend = new StringContent(TestXmlStrings.xmlLnpOrderResponseNoErrors, Encoding.UTF8, "application/xml")
                }                
            }))
            {
                var client = Helper.CreateClient();
                
                portIn.SetClient(client);
                var r = portIn.GetOrder().Result;
                Assert.AreEqual("SJM00002", r.CustomerOrderId);
                Assert.AreEqual("CANCELLED", r.ProcessingStatus);
                Assert.AreEqual(DateTime.Parse("2014-08-04T13:37:06.323"), r.OrderCreateDate);
                Assert.AreEqual(DateTime.Parse("2014-08-04T13:37:08.676"), r.LastModifiedDate);
                Assert.AreEqual(DateTime.Parse("2014-08-04T13:37:08.676"), r.RequestedFocDate);
                Assert.AreEqual("The Authguy", r.LoaAuthorizingPerson);
                Assert.AreEqual("9195551234", r.BillingTelephoneNumber);
                Assert.AreEqual("9175131245", r.NewBillingTelephoneNumber);
                Assert.AreEqual("Foo", r.AlternateSpid);
                Assert.AreEqual("20", r.AccountId);
                Assert.AreEqual("2857", r.SiteId);
                Assert.AreEqual("317771", r.PeerId);
                Assert.AreEqual("Mock Carrier", r.LosingCarrierName);
                Assert.AreEqual("Bandwidth CLEC", r.VendorName);

                Assert.AreEqual("jbm", r.UserId);
                Assert.AreEqual("jbm", r.LastModifiedBy);
                Assert.AreEqual(false, r.PartialPort);
                Assert.AreEqual(false, r.Triggered);
                Assert.AreEqual(PortType.AUTOMATED, r.PortType);

                //TnAttributes
                Assert.AreEqual(1, r.TnAttributes.Length);
                Assert.AreEqual("Protected", r.TnAttributes[0]);

                //Suscriber
                Assert.AreEqual("BUSINESS", r.Subscriber.SubscriberType);
                Assert.AreEqual("First", r.Subscriber.FirstName);
                Assert.AreEqual("Last", r.Subscriber.LastName);
                Assert.AreEqual("11235", r.Subscriber.ServiceAddress.HouseNumber);
                Assert.AreEqual("Back", r.Subscriber.ServiceAddress.StreetName);
                Assert.AreEqual("Denver", r.Subscriber.ServiceAddress.City);
                Assert.AreEqual("CO", r.Subscriber.ServiceAddress.StateCode);
                Assert.AreEqual("27541", r.Subscriber.ServiceAddress.Zip);
                Assert.AreEqual("Canyon", r.Subscriber.ServiceAddress.County);
                Assert.AreEqual("United States", r.Subscriber.ServiceAddress.Country);
                Assert.AreEqual("Service", r.Subscriber.ServiceAddress.AddressType);
            }
        }

        [TestMethod]
        public void LnpOrderResponseTest()
        {
            string xmlLnpOrderResponse = TestXmlStrings.xmlLnpOrderResponse;

            LnpOrderResponse lnpOrderResponse = Helper.ParseXml<LnpOrderResponse>(xmlLnpOrderResponse);

            Assert.AreEqual("SJM00002", lnpOrderResponse.CustomerOrderId);
            Assert.AreEqual("CANCELLED", lnpOrderResponse.ProcessingStatus);
            Assert.AreEqual(DateTime.Parse("2014-08-04T13:37:06.323"), lnpOrderResponse.OrderCreateDate);
            Assert.AreEqual(DateTime.Parse("2014-08-04T13:37:08.676"), lnpOrderResponse.LastModifiedDate);
            Assert.AreEqual(DateTime.Parse("2014-08-04T13:37:08.676"), lnpOrderResponse.RequestedFocDate);
            Assert.AreEqual("The Authguy", lnpOrderResponse.LoaAuthorizingPerson);
            Assert.AreEqual("9195551234", lnpOrderResponse.BillingTelephoneNumber);
            Assert.AreEqual("9175131245", lnpOrderResponse.NewBillingTelephoneNumber);
            Assert.AreEqual("Foo", lnpOrderResponse.AlternateSpid);
            Assert.AreEqual("20", lnpOrderResponse.AccountId);
            Assert.AreEqual("2857", lnpOrderResponse.SiteId);
            Assert.AreEqual("317771", lnpOrderResponse.PeerId);
            Assert.AreEqual("Mock Carrier", lnpOrderResponse.LosingCarrierName);
            Assert.AreEqual("Bandwidth CLEC", lnpOrderResponse.VendorName);
            
            Assert.AreEqual("jbm", lnpOrderResponse.UserId);
            Assert.AreEqual("jbm", lnpOrderResponse.LastModifiedBy);
            Assert.AreEqual(false, lnpOrderResponse.PartialPort);
            Assert.AreEqual(false, lnpOrderResponse.Triggered);
            Assert.AreEqual(PortType.AUTOMATED, lnpOrderResponse.PortType);

            //Errors
            Assert.AreEqual(2, lnpOrderResponse.Errors.Length);
            Assert.AreEqual("7205", lnpOrderResponse.Errors[1].Code);
            Assert.AreEqual("Telephone number is already being processed on another order", lnpOrderResponse.Errors[1].Description);

            //TnAttributes
            Assert.AreEqual(1, lnpOrderResponse.TnAttributes.Length);
            Assert.AreEqual("Protected", lnpOrderResponse.TnAttributes[0]);

            //Suscriber
            Assert.AreEqual("BUSINESS", lnpOrderResponse.Subscriber.SubscriberType);
            Assert.AreEqual("First", lnpOrderResponse.Subscriber.FirstName);
            Assert.AreEqual("Last", lnpOrderResponse.Subscriber.LastName);
            Assert.AreEqual("11235", lnpOrderResponse.Subscriber.ServiceAddress.HouseNumber);
            Assert.AreEqual("Back", lnpOrderResponse.Subscriber.ServiceAddress.StreetName);
            Assert.AreEqual("Denver", lnpOrderResponse.Subscriber.ServiceAddress.City);
            Assert.AreEqual("CO", lnpOrderResponse.Subscriber.ServiceAddress.StateCode);
            Assert.AreEqual("27541", lnpOrderResponse.Subscriber.ServiceAddress.Zip);
            Assert.AreEqual("Canyon", lnpOrderResponse.Subscriber.ServiceAddress.County);
            Assert.AreEqual("United States", lnpOrderResponse.Subscriber.ServiceAddress.Country);
            Assert.AreEqual("Service", lnpOrderResponse.Subscriber.ServiceAddress.AddressType);






        }

        [TestMethod]
        public void GetPortInsTest()
        {

            var portIn = new PortIn();

            DateTime startdate = DateTime.Parse("2014-11-21T14:00:33.836Z");
            DateTime enddate = DateTime.Parse("2014-11-21T14:00:33.835Z");
            DateTime date = DateTime.Parse("2014-11-21T14:00:33.834Z");

            using (var server = new HttpServer(new[]{
                new RequestHandler
                {
                    EstimatedMethod = "GET",
                    EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/portins/?page={1}&size={2}&date={3}&enddate={4}&startdate={5}&pon={6}&status={7}&tn={8}", 
                        Helper.AccountId, 1, 300,
                        Uri.EscapeDataString(date.ToString()),
                        Uri.EscapeDataString(enddate.ToString()),
                        Uri.EscapeDataString(startdate.ToString()),
                        "ponstr", "completed",  "9199199191"),
                    EstimatedContent = "",
                    ContentToSend = new StringContent(TestXmlStrings.xmlLNPResponseWrapper, Encoding.UTF8, "application/xml")
                }
            }))
            {
                var client = Helper.CreateClient();

                portIn.SetClient(client);
                var r = portIn.GetPortIns(Helper.AccountId, date, enddate, startdate, "ponstr", "completed", "9199199191").Result;

                Assert.AreEqual(" -- link -- ", r.Links.First);
                Assert.AreEqual(" -- link -- ", r.Links.Next);
                Assert.AreEqual(3176, r.TotalCount);
                Assert.AreEqual(1, r.lnpPortInfoForGivenStatuses[0].CountOfTNs);
                Assert.AreEqual("Neustar", r.lnpPortInfoForGivenStatuses[0].UserId);
                Assert.AreEqual(DateTime.Parse("2014-11-21T14:00:33.836"), r.lnpPortInfoForGivenStatuses[0].LastModifiedDate);
                Assert.AreEqual(DateTime.Parse("2014-11-05T19:34:53.176"), r.lnpPortInfoForGivenStatuses[0].OrderDate);
                Assert.AreEqual("982e3c10-3840-4251-abdd-505cd8610788", r.lnpPortInfoForGivenStatuses[0].OrderId);
                Assert.AreEqual("port_out", r.lnpPortInfoForGivenStatuses[0].OrderType);
                Assert.AreEqual(200, r.lnpPortInfoForGivenStatuses[0].ErrorCode);
                Assert.AreEqual("Port out successful.", r.lnpPortInfoForGivenStatuses[0].ErrorMessage);
                Assert.AreEqual("9727717577", r.lnpPortInfoForGivenStatuses[0].FullNumber);
                Assert.AreEqual("COMPLETE", r.lnpPortInfoForGivenStatuses[0].ProcessingStatus);
                Assert.AreEqual(DateTime.Parse("2014-11-20T00:00:00.000"), r.lnpPortInfoForGivenStatuses[0].RequestedFOCDate);
                Assert.AreEqual("512E", r.lnpPortInfoForGivenStatuses[0].VendorId);


            }

        }
    }
}
