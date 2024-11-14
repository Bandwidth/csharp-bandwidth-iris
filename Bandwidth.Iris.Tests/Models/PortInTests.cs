using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Bandwidth.Iris.Model;
using Xunit;

namespace Bandwidth.Iris.Tests.Models
{

    public class PortInTests
    {
        // [TestInitialize]
        public PortInTests()
        {
            Helper.SetEnvironmentVariables();
        }

        [Fact]
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

        [Fact]
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
                Assert.Equal("201", r.Status.Code);
                Assert.Equal("Order request received. Please use the order id to check the status of your order later.", r.Status.Description);
                Assert.Equal("PENDING_DOCUMENTS", r.ProcessingStatus);
                Assert.Equal("John Doe", r.LoaAuthorizingPerson);
                Assert.Equal("6882015002", r.BillingTelephoneNumber);
                Assert.Equal(new[] { "6882015025", "6882015026" }, r.ListOfPhoneNumbers);
                Assert.False(r.Triggered);
                Assert.Equal("PORTIN", r.BillingType);

            }
        }
        [Fact]
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
        [Fact]
        public void CreateFileTest()
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
                using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(data)))
                {
                    var fileName = portIn.CreateFile(stream, "media/type").Result;
                    if (server.Error != null) throw server.Error;
                    Assert.Equal("test", fileName);
                }
            }
        }

        [Fact]
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
                Assert.Equal("test", fileName);
            }
        }

        [Fact]
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

        [Fact]
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

        [Fact]
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

        [Fact]
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
                    Assert.Equal("media/type", r.MediaType);
                    Assert.Equal(data, Encoding.UTF8.GetString(r.Buffer));
                }
                if (server.Error != null) throw server.Error;
            }
        }

        [Fact]
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
                using (var reader = new StreamReader(r.Stream, Encoding.UTF8))
                {
                    Assert.Equal("media/type", r.MediaType);
                    Assert.Equal(data, reader.ReadToEnd());
                }
                if (server.Error != null) throw server.Error;
            }
        }

        [Fact]
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
                Assert.Equal(6, list.Length);
                Assert.Equal("d28b36f7-fa96-49eb-9556-a40fca49f7c6-1416231534986.txt", list[0].FileName);
                Assert.Equal("LOA", list[0].FileMetadata.DocumentType);
            }
        }

        [Fact]
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
                Assert.Equal("LOA", r.DocumentType);
            }
        }

        [Fact]
        public void PutFileMetadataTest()
        {
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "PUT",
                EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/portins/1/loas/test/metadata", Helper.AccountId),
                EstimatedContent = TestXmlStrings.FileMetadataPut
            }))
            {

                var fileMetadata = new FileMetadata
                {
                    DocumentType = "INVOICE",
                    DocumentName = "docName"
                };
                var client = Helper.CreateClient();
                var portIn = new PortIn { Id = "1" };
                portIn.SetClient(client);
                var r = portIn.PutFileMetadata("test", fileMetadata).Result;
                if (server.Error != null) throw server.Error;
                Assert.Equal("OK", r.StatusCode.ToString());
            }
        }

        [Fact]
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

        [Fact]
        public void UpdateTest()
        {
            var data = new LnpOrderSupp
            {
                RequestedFocDate = DateTime.Parse("2014-11-18T00:00:00.000Z"),
                WirelessInfo = new[]{new WirelessInfo
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

        [Fact]
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
                Assert.Equal(2, list.Length);
                Assert.Equal("11299", list[0].Id);
                Assert.Equal("customer", list[0].UserId);
                Assert.Equal("Test", list[0].Description);
                Assert.Equal("11301", list[1].Id);
                Assert.Equal("customer", list[1].UserId);
                Assert.Equal("Test1", list[1].Description);
            }
        }

        [Fact]
        public void AddNoteTest()
        {
            var item = new Note
            {
                UserId = "customer",
                Description = "Test"
            };
            using (var server = new HttpServer(new[]{
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
                Assert.Equal("11299", r.Id);
                Assert.Equal("customer", r.UserId);
                Assert.Equal("Test", r.Description);
            }
        }

        [Fact]
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
                Assert.Equal("SJM00002", r.CustomerOrderId);
                Assert.Equal("CANCELLED", r.ProcessingStatus);
                Assert.Equal(DateTime.Parse("2014-08-04T13:37:06.323"), r.OrderCreateDate);
                Assert.Equal(DateTime.Parse("2014-08-04T13:37:08.676"), r.LastModifiedDate);
                Assert.Equal(DateTime.Parse("2014-08-04T13:37:08.676"), r.RequestedFocDate);
                Assert.Equal("The Authguy", r.LoaAuthorizingPerson);
                Assert.Equal("9195551234", r.BillingTelephoneNumber);
                Assert.Equal("9175131245", r.NewBillingTelephoneNumber);
                Assert.Equal("Foo", r.AlternateSpid);
                Assert.Equal("20", r.AccountId);
                Assert.Equal("2857", r.SiteId);
                Assert.Equal("317771", r.PeerId);
                Assert.Equal("Mock Carrier", r.LosingCarrierName);
                Assert.Equal("Bandwidth CLEC", r.VendorName);

                Assert.Equal("jbm", r.UserId);
                Assert.Equal("jbm", r.LastModifiedBy);
                Assert.Equal(false, r.PartialPort);
                Assert.Equal(false, r.Triggered);
                Assert.Equal(PortType.AUTOMATED, r.PortType);

                //TnAttributes
                Assert.Single(r.TnAttributes);
                Assert.Equal("Protected", r.TnAttributes[0]);

                //Suscriber
                Assert.Equal("BUSINESS", r.Subscriber.SubscriberType);
                Assert.Equal("First", r.Subscriber.FirstName);
                Assert.Equal("Last", r.Subscriber.LastName);
                Assert.Equal("11235", r.Subscriber.ServiceAddress.HouseNumber);
                Assert.Equal("Back", r.Subscriber.ServiceAddress.StreetName);
                Assert.Equal("Denver", r.Subscriber.ServiceAddress.City);
                Assert.Equal("CO", r.Subscriber.ServiceAddress.StateCode);
                Assert.Equal("27541", r.Subscriber.ServiceAddress.Zip);
                Assert.Equal("Canyon", r.Subscriber.ServiceAddress.County);
                Assert.Equal("United States", r.Subscriber.ServiceAddress.Country);
                Assert.Equal("Service", r.Subscriber.ServiceAddress.AddressType);
            }
        }

        [Fact]
        public void LnpOrderResponseTest()
        {
            string xmlLnpOrderResponse = TestXmlStrings.xmlLnpOrderResponse;

            LnpOrderResponse lnpOrderResponse = Helper.ParseXml<LnpOrderResponse>(xmlLnpOrderResponse);

            Assert.Equal("SJM00002", lnpOrderResponse.CustomerOrderId);
            Assert.Equal("CANCELLED", lnpOrderResponse.ProcessingStatus);
            Assert.Equal(DateTime.Parse("2014-08-04T13:37:06.323"), lnpOrderResponse.OrderCreateDate);
            Assert.Equal(DateTime.Parse("2014-08-04T13:37:08.676"), lnpOrderResponse.LastModifiedDate);
            Assert.Equal(DateTime.Parse("2014-08-04T13:37:08.676"), lnpOrderResponse.RequestedFocDate);
            Assert.Equal("The Authguy", lnpOrderResponse.LoaAuthorizingPerson);
            Assert.Equal("9195551234", lnpOrderResponse.BillingTelephoneNumber);
            Assert.Equal("9175131245", lnpOrderResponse.NewBillingTelephoneNumber);
            Assert.Equal("Foo", lnpOrderResponse.AlternateSpid);
            Assert.Equal("20", lnpOrderResponse.AccountId);
            Assert.Equal("2857", lnpOrderResponse.SiteId);
            Assert.Equal("317771", lnpOrderResponse.PeerId);
            Assert.Equal("Mock Carrier", lnpOrderResponse.LosingCarrierName);
            Assert.Equal("Bandwidth CLEC", lnpOrderResponse.VendorName);

            Assert.Equal("jbm", lnpOrderResponse.UserId);
            Assert.Equal("jbm", lnpOrderResponse.LastModifiedBy);
            Assert.Equal(false, lnpOrderResponse.PartialPort);
            Assert.Equal(false, lnpOrderResponse.Triggered);
            Assert.Equal(PortType.AUTOMATED, lnpOrderResponse.PortType);

            //Errors
            Assert.Equal(2, lnpOrderResponse.Errors.Length);
            Assert.Equal("7205", lnpOrderResponse.Errors[1].Code);
            Assert.Equal("Telephone number is already being processed on another order", lnpOrderResponse.Errors[1].Description);

            //TnAttributes
            Assert.Single(lnpOrderResponse.TnAttributes);
            Assert.Equal("Protected", lnpOrderResponse.TnAttributes[0]);

            //Suscriber
            Assert.Equal("BUSINESS", lnpOrderResponse.Subscriber.SubscriberType);
            Assert.Equal("First", lnpOrderResponse.Subscriber.FirstName);
            Assert.Equal("Last", lnpOrderResponse.Subscriber.LastName);
            Assert.Equal("11235", lnpOrderResponse.Subscriber.ServiceAddress.HouseNumber);
            Assert.Equal("Back", lnpOrderResponse.Subscriber.ServiceAddress.StreetName);
            Assert.Equal("Denver", lnpOrderResponse.Subscriber.ServiceAddress.City);
            Assert.Equal("CO", lnpOrderResponse.Subscriber.ServiceAddress.StateCode);
            Assert.Equal("27541", lnpOrderResponse.Subscriber.ServiceAddress.Zip);
            Assert.Equal("Canyon", lnpOrderResponse.Subscriber.ServiceAddress.County);
            Assert.Equal("United States", lnpOrderResponse.Subscriber.ServiceAddress.Country);
            Assert.Equal("Service", lnpOrderResponse.Subscriber.ServiceAddress.AddressType);






        }

        [Fact]
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

                Assert.Equal(" -- link -- ", r.Links.First);
                Assert.Equal(" -- link -- ", r.Links.Next);
                Assert.Equal(3176, r.TotalCount);
                Assert.Equal(1, r.lnpPortInfoForGivenStatuses[0].CountOfTNs);
                Assert.Equal("Neustar", r.lnpPortInfoForGivenStatuses[0].UserId);
                Assert.Equal(DateTime.Parse("2014-11-21T14:00:33.836"), r.lnpPortInfoForGivenStatuses[0].LastModifiedDate);
                Assert.Equal(DateTime.Parse("2014-11-05T19:34:53.176"), r.lnpPortInfoForGivenStatuses[0].OrderDate);
                Assert.Equal("982e3c10-3840-4251-abdd-505cd8610788", r.lnpPortInfoForGivenStatuses[0].OrderId);
                Assert.Equal("port_out", r.lnpPortInfoForGivenStatuses[0].OrderType);
                Assert.Equal(200, r.lnpPortInfoForGivenStatuses[0].ErrorCode);
                Assert.Equal("Port out successful.", r.lnpPortInfoForGivenStatuses[0].ErrorMessage);
                Assert.Equal("9727717577", r.lnpPortInfoForGivenStatuses[0].FullNumber);
                Assert.Equal("COMPLETE", r.lnpPortInfoForGivenStatuses[0].ProcessingStatus);
                Assert.Equal(DateTime.Parse("2014-11-20T00:00:00.000"), r.lnpPortInfoForGivenStatuses[0].RequestedFOCDate);
                Assert.Equal("512E", r.lnpPortInfoForGivenStatuses[0].VendorId);


            }

        }
    }
}
