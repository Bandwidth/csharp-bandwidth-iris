using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Bandwidth.Iris.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bandwidth.Iris.Tests.Models
{
    [TestClass]
    public class LsrOrderTests
    {
        [TestInitialize]
        public void Setup()
        {
            Helper.SetEnvironmetVariables();
        }

        [TestMethod]
        public void GetTest()
        {
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/lsrorders/1", Helper.AccountId),
                ContentToSend = new StringContent(TestXmlStrings.LsrOrder, Encoding.UTF8, "application/xml")
            }))
            {
                var client = Helper.CreateClient();
                var result = LsrOrder.Get(client, "1").Result;
                if (server.Error != null) throw server.Error;
                Assert.AreEqual("00cf7e08-cab0-4515-9a77-2d0a7da09415", result.Id);
            }
        }
               

        [TestMethod]
        public void GetWithDefaultClientTest()
        {
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/lsrorders/1", Helper.AccountId),
                ContentToSend = new StringContent(TestXmlStrings.LsrOrder, Encoding.UTF8, "application/xml")
            }))
            {
                var result = LsrOrder.Get("1").Result;
                if (server.Error != null) throw server.Error;
                Assert.AreEqual("00cf7e08-cab0-4515-9a77-2d0a7da09415", result.Id);
            }
        }

        [TestMethod]
        public void ListTest()
        {
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/lsrorders", Helper.AccountId),
                ContentToSend = new StringContent(TestXmlStrings.LsrOrders, Encoding.UTF8, "application/xml")
            }))
            {
                var client = Helper.CreateClient();
                var result = LsrOrder.List(client).Result;
                if (server.Error != null) throw server.Error;
                Assert.AreEqual(2, result.Length);
            }
        }


        [TestMethod]
        public void ListWithDefaultClientTest()
        {
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/lsrorders", Helper.AccountId),
                ContentToSend = new StringContent(TestXmlStrings.LsrOrders, Encoding.UTF8, "application/xml")
            }))
            {
                var result = LsrOrder.List().Result;
                if (server.Error != null) throw server.Error;
                Assert.AreEqual(2, result.Length);
            }
        }

        [TestMethod]
        public void CreateTest()
        {
            var item = new LsrOrder
            {
               Pon = "Some Pon",
               CustomerOrderId = "MyId5",
               Spid = "123C",
               BillingTelephoneNumber = "9192381468",
               AuthorizingPerson = "Jim Hopkins",
               Subscriber = new Subscriber{
                   SubscriberType = "BUSINESS",
                   BusinessName = "BusinessName",
                   ServiceAddress = new Address {
                    HouseNumber ="11",
                    StreetName = "Park",
                    StreetSuffix = "Ave",
                    City = "New York",
                    StateCode = "NY",
                    Zip = "90025"
                  },
                  AccountNumber = "123463",
                  PinNumber = "1231"
               },
               ListOfTelephoneNumbers = new[]{"9192381848", "9192381467"}
            };

            using (var server = new HttpServer(new[]
            {
                new RequestHandler
                {
                    EstimatedMethod = "POST",
                    EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/lsrorders", Helper.AccountId),
                    EstimatedContent = Helper.ToXmlString(item),
                    HeadersToSend =
                        new Dictionary<string, string>
                        {
                            {"Location", string.Format("/v1.0/accounts/{0}/lsrorders/1", Helper.AccountId)}
                        }
                },
                new RequestHandler
                {
                    EstimatedMethod = "GET",
                    EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/lsrorders/1", Helper.AccountId),
                    ContentToSend = new StringContent(TestXmlStrings.LsrOrder, Encoding.UTF8, "application/xml")
                }
            }))
            {
                var client = Helper.CreateClient();
                var i = LsrOrder.Create(client, item).Result;
                if (server.Error != null) throw server.Error;
                Assert.AreEqual("00cf7e08-cab0-4515-9a77-2d0a7da09415", i.Id);
            }
        }

        [TestMethod]
        public void CreateTestWithNames()
        {
            var item = new LsrOrder
            {
                Pon = "Some Pon",
                CustomerOrderId = "MyId5",
                Spid = "123C",
                BillingTelephoneNumber = "9192381468",
                AuthorizingPerson = "Jim Hopkins",
                Subscriber = new Subscriber
                {
                    SubscriberType = "BUSINESS",
                    BusinessName = "BusinessName",
                    ServiceAddress = new Address
                    {
                        HouseNumber = "11",
                        StreetName = "Park",
                        StreetSuffix = "Ave",
                        City = "New York",
                        StateCode = "NY",
                        Zip = "90025"
                    },
                    AccountNumber = "123463",
                    PinNumber = "1231",
                    FirstName = "John",
                    LastName = "Doe"
                },
                ListOfTelephoneNumbers = new[] { "9192381848", "9192381467" }
            };

            using (var server = new HttpServer(new[]
            {
                new RequestHandler
                {
                    EstimatedMethod = "POST",
                    EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/lsrorders", Helper.AccountId),
                    EstimatedContent = Helper.ToXmlString(item),
                    HeadersToSend =
                        new Dictionary<string, string>
                        {
                            {"Location", string.Format("/v1.0/accounts/{0}/lsrorders/1", Helper.AccountId)}
                        }
                },
                new RequestHandler
                {
                    EstimatedMethod = "GET",
                    EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/lsrorders/1", Helper.AccountId),
                    ContentToSend = new StringContent(TestXmlStrings.LsrOrder, Encoding.UTF8, "application/xml")
                }
            }))
            {
                var client = Helper.CreateClient();
                var i = LsrOrder.Create(client, item).Result;
                if (server.Error != null) throw server.Error;
                Assert.AreEqual("00cf7e08-cab0-4515-9a77-2d0a7da09415", i.Id);
            }
        }

        [TestMethod]
        public void CreateWithDefaultClientTest()
        {
            var item = new LsrOrder
            {
                Pon = "Some Pon",
                CustomerOrderId = "MyId5",
                Spid = "123C",
                BillingTelephoneNumber = "9192381468",
                AuthorizingPerson = "Jim Hopkins",
                Subscriber = new Subscriber
                {
                    SubscriberType = "BUSINESS",
                    BusinessName = "BusinessName",
                    ServiceAddress = new Address
                    {
                        HouseNumber = "11",
                        StreetName = "Park",
                        StreetSuffix = "Ave",
                        City = "New York",
                        StateCode = "NY",
                        Zip = "90025"
                    },
                    AccountNumber = "123463",
                    PinNumber = "1231"
                },
                ListOfTelephoneNumbers = new[] { "9192381848", "9192381467" }
            };

            using (var server = new HttpServer(new[]
            {
                new RequestHandler
                {
                    EstimatedMethod = "POST",
                    EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/lsrorders", Helper.AccountId),
                    EstimatedContent = Helper.ToXmlString(item),
                    HeadersToSend =
                        new Dictionary<string, string>
                        {
                            {"Location", string.Format("/v1.0/accounts/{0}/lsrorders/1", Helper.AccountId)}
                        }
                },
                new RequestHandler
                {
                    EstimatedMethod = "GET",
                    EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/lsrorders/1", Helper.AccountId),
                    ContentToSend = new StringContent(TestXmlStrings.LsrOrder, Encoding.UTF8, "application/xml")
                }
            }))
            {
                var i = LsrOrder.Create(item).Result;
                if (server.Error != null) throw server.Error;
                Assert.AreEqual("00cf7e08-cab0-4515-9a77-2d0a7da09415", i.Id);
            }
        }

        [TestMethod]
        public void UpdateTest()
        {
            var item = new LsrOrder { Id = "101" };
            var data = new LsrOrder
            {
                BillingTelephoneNumber = "12345"
            };
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "PUT",
                EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/lsrorders/101", Helper.AccountId),
                EstimatedContent = Helper.ToXmlString(data),
            }))
            {
                item.Update(data).Wait();
                if (server.Error != null) throw server.Error;
            }
        }

        [TestMethod]
        public void GetNotesTest()
        {
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/lsrorders/1/notes", Helper.AccountId),
                ContentToSend = new StringContent(TestXmlStrings.NotesResponse, Encoding.UTF8, "application/xml")
            }))
            {
                var client = Helper.CreateClient();
                var order = new LsrOrder { Id = "1" };
                order.SetClient(client);
                var list = order.GetNotes().Result;
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
            using (var server = new HttpServer(new[]{
                new RequestHandler
                {
                    EstimatedMethod = "POST",
                    EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/lsrorders/1/notes", Helper.AccountId),
                    EstimatedContent = Helper.ToXmlString(item),
                    HeadersToSend = new Dictionary<string, string> {
                        {"Location", string.Format("/v1.0/accounts/{0}/lsrorders/1/notes/11299", Helper.AccountId)} 
                    }
                },
                new RequestHandler
                {
                    EstimatedMethod = "GET",
                    EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/lsrorders/1/notes", Helper.AccountId),
                    ContentToSend = new StringContent(TestXmlStrings.NotesResponse, Encoding.UTF8, "application/xml")
                }
            }))
            {
                var client = Helper.CreateClient();
                var order = new LsrOrder { Id = "1" };
                order.SetClient(client);
                var r = order.AddNote(item).Result;
                if (server.Error != null) throw server.Error;
                Assert.AreEqual("11299", r.Id);
                Assert.AreEqual("customer", r.UserId);
                Assert.AreEqual("Test", r.Description);
            }
        }

        [TestMethod]
        public void GetHistoryTest()
        {
            using (var server = new HttpServer(new[]
            {
                new RequestHandler
                {
                    EstimatedMethod = "GET",
                    EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/lsrorders/1/history", Helper.AccountId),
                    ContentToSend = new StringContent(TestXmlStrings.OrderHistory, Encoding.UTF8, "application/xml")
                }
            }))
            {
                var client = Helper.CreateClient();
                var i = new LsrOrder { Id = "1" };
                i.SetClient(client);
                var result = i.GetHistory().Result;
                if (server.Error != null) throw server.Error;
                Assert.IsTrue(result.Length > 0);
            }
        }
    }
}
