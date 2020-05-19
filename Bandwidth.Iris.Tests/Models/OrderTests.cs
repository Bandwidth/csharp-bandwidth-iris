using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Bandwidth.Iris.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bandwidth.Iris.Tests.Models
{
    [TestClass]
    public class OrderTests
    {
        [TestInitialize]
        public void Setup()
        {
            Helper.SetEnvironmetVariables();
        }

        [TestMethod]
        public void CreateTest()
        {
            var order = new Order
            {
                Name = "Test",
                SiteId = "10",
                PeerId = "12",
                CustomerOrderId = "11",
                LataSearchAndOrderType = new LataSearchAndOrderType
                {
                    Lata = "224",
                    Quantity = 1
                }
            };
            var orderResult = new OrderResult
            {
                CompletedQuantity = 1,
                CreatedByUser = "test",
                Order = new Order
                {
                    Name = "Test",
                    SiteId = "10",
                    CustomerOrderId = "11",
                    Id = "101",
                    OrderCreateDate = DateTime.Now
                }
            };
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "POST",
                EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/orders", Helper.AccountId),
                EstimatedContent = Helper.ToXmlString(order),
                ContentToSend = Helper.CreateXmlContent(orderResult)
            }))
            {
                var client = Helper.CreateClient();
                var result = Order.Create(client, order).Result;
                if (server.Error != null) throw server.Error;
                Helper.AssertObjects(orderResult, result);
            }
        }

        [TestMethod]
        public void CreateWithXmlTest()
        {
            var order = new Order
            {
                Name = "Test",
                SiteId = "10",
                CustomerOrderId = "11",
                LataSearchAndOrderType = new LataSearchAndOrderType
                {
                    Lata = "224",
                    Quantity = 1
                }
            };
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "POST",
                EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/orders", Helper.AccountId),
                EstimatedContent = Helper.ToXmlString(order),
                ContentToSend = new StringContent(TestXmlStrings.ValidOrderResponseXml, Encoding.UTF8, "application/xml")
            }))
            {
                var client = Helper.CreateClient();
                var result = Order.Create(client, order).Result;
                if (server.Error != null) throw server.Error;
                var o = result.Order;
                Assert.AreEqual("1", o.Id);
                Assert.AreEqual("2858", o.SiteId);
                Assert.AreEqual("A New Order", o.Name);
                Assert.AreEqual(DateTime.Parse("2014-10-14T17:58:15.299Z").ToUniversalTime(), o.OrderCreateDate);
                Assert.IsFalse(o.BackOrderRequested);
                Assert.AreEqual("2052865046", o.ExistingTelephoneNumberOrderType.TelephoneNumberList[0]);
                Assert.IsFalse(o.PartialAllowed);
            }
        }

        [TestMethod]
        public void CreateWithDefaultClientTest()
        {
            var order = new Order
            {
                Name = "Test",
                SiteId = "10",
                CustomerOrderId = "11",
                LataSearchAndOrderType = new LataSearchAndOrderType
                {
                    Lata = "224",
                    Quantity = 1
                }
            };
            var orderResult = new OrderResult
            {
                CompletedQuantity = 1,
                CreatedByUser = "test",
                Order = new Order
                {
                    Name = "Test",
                    SiteId = "10",
                    CustomerOrderId = "11",
                    Id = "101",
                    OrderCreateDate = DateTime.Now
                }
            };
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "POST",
                EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/orders", Helper.AccountId),
                EstimatedContent = Helper.ToXmlString(order),
                ContentToSend = Helper.CreateXmlContent(orderResult)
            }))
            {
                var result = Order.Create(order).Result;
                if (server.Error != null) throw server.Error;
                Helper.AssertObjects(orderResult, result);
            }
        }

        [TestMethod]
        public void GetTest()
        {
            var orderResult = new OrderResult
            {
                CompletedQuantity = 1,
                CreatedByUser = "test",
                Order = new Order
                {
                    Name = "Test",
                    SiteId = "10",
                    CustomerOrderId = "11",
                    Id = "101",
                    OrderCreateDate = DateTime.Now
                }
            };
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/orders/101", Helper.AccountId),
                ContentToSend = Helper.CreateXmlContent(orderResult)
            }))
            {
                var client = Helper.CreateClient();
                var result = Order.Get(client, "101").Result;
                if (server.Error != null) throw server.Error;
                Helper.AssertObjects(orderResult, result);
            }
        }

        [TestMethod]
        public void ListTest()
        {
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/orders", Helper.AccountId),
                ContentToSend = new StringContent(TestXmlStrings.listOrders, Encoding.UTF8, "application/xml")
            }))
            {
                var client = Helper.CreateClient();
                var result = Order.List(client).Result;
                if (server.Error != null) throw server.Error;


                Assert.AreEqual(122, result.Orders.TotalCount);
                Assert.AreEqual(3, result.Orders.OrderDetails.Count);
                Assert.AreEqual(1, result.Orders.OrderDetails[0].CountOfTns);
                Assert.AreEqual("COMPLETE", result.Orders.OrderDetails[0].OrderStatus);
                Assert.AreEqual("c5d8d076-345c-45d7-87b3-803a35cca89b", result.Orders.OrderDetails[0].OrderId);
                Assert.AreEqual("2013-12-20T06", result.Orders.OrderDetails[0].LastModifiedDate);
                Assert.AreEqual("2013-12-20T06", result.Orders.OrderDetails[0].OrderDate);
                Assert.AreEqual("bwc_user", result.Orders.OrderDetails[0].UserId);
                Assert.AreEqual(1, result.Orders.OrderDetails[0].TelephoneNumberDetailsWithCount.States.Count);
                Assert.AreEqual(1, result.Orders.OrderDetails[0].TelephoneNumberDetailsWithCount.States[0].Count);
                Assert.AreEqual("VA", result.Orders.OrderDetails[0].TelephoneNumberDetailsWithCount.States[0].State);
                Assert.AreEqual(1, result.Orders.OrderDetails[0].TelephoneNumberDetailsWithCount.RateCenters.Count);
                Assert.AreEqual(1, result.Orders.OrderDetails[0].TelephoneNumberDetailsWithCount.RateCenters[0].Count);
                Assert.AreEqual("LADYSMITH", result.Orders.OrderDetails[0].TelephoneNumberDetailsWithCount.RateCenters[0].RateCenter);
                Assert.AreEqual(1, result.Orders.OrderDetails[0].TelephoneNumberDetailsWithCount.Cities.Count);
                Assert.AreEqual(1, result.Orders.OrderDetails[0].TelephoneNumberDetailsWithCount.Cities[0].Count);
                Assert.AreEqual("LADYSMITH", result.Orders.OrderDetails[0].TelephoneNumberDetailsWithCount.Cities[0].City);
                Assert.AreEqual(1, result.Orders.OrderDetails[0].TelephoneNumberDetailsWithCount.Tiers.Count);
                Assert.AreEqual(1, result.Orders.OrderDetails[0].TelephoneNumberDetailsWithCount.Tiers[0].Count);
                Assert.AreEqual(0, result.Orders.OrderDetails[0].TelephoneNumberDetailsWithCount.Tiers[0].Tier);
                Assert.AreEqual(1, result.Orders.OrderDetails[0].TelephoneNumberDetailsWithCount.Vendors.Count);
                Assert.AreEqual(1, result.Orders.OrderDetails[0].TelephoneNumberDetailsWithCount.Vendors[0].Count);
                Assert.AreEqual(49, result.Orders.OrderDetails[0].TelephoneNumberDetailsWithCount.Vendors[0].VendorId);
                Assert.AreEqual("Bandwidth CLEC", result.Orders.OrderDetails[0].TelephoneNumberDetailsWithCount.Vendors[0].VendorName);
            }
        }

        [TestMethod]
        public void GetWithDefaultClientTest()
        {
            var orderResult = new OrderResult
            {
                CompletedQuantity = 1,
                CreatedByUser = "test",
                Order = new Order
                {
                    Name = "Test",
                    SiteId = "10",
                    CustomerOrderId = "11",
                    Id = "101",
                    OrderCreateDate = DateTime.Now
                }
            };
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/orders/101", Helper.AccountId),
                ContentToSend = Helper.CreateXmlContent(orderResult)
            }))
            {
                var result = Order.Get("101").Result;
                if (server.Error != null) throw server.Error;
                Helper.AssertObjects(orderResult, result);
            }
        }
        [TestMethod]
        public void UpdateTest()
        {
            var item = new Order {Id = "101"};
            var data = new Order
            {
                Name = "Test",
                CloseOrder = true
            };
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "PUT",
                EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/orders/101", Helper.AccountId),
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
                EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/orders/1/notes", Helper.AccountId),
                ContentToSend = new StringContent(TestXmlStrings.NotesResponse, Encoding.UTF8, "application/xml")
            }))
            {
                var client = Helper.CreateClient();
                var order = new Order { Id = "1" };
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
                    EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/orders/1/notes", Helper.AccountId),
                    EstimatedContent = Helper.ToXmlString(item),
                    HeadersToSend = new Dictionary<string, string> {
                        {"Location", string.Format("/v1.0/accounts/{0}/portins/1/notes/11299", Helper.AccountId)} 
                    }
                },
                new RequestHandler
                {
                    EstimatedMethod = "GET",
                    EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/orders/1/notes", Helper.AccountId),
                    ContentToSend = new StringContent(TestXmlStrings.NotesResponse, Encoding.UTF8, "application/xml")
                }
            }))
            {
                var client = Helper.CreateClient();
                var order = new Order { Id = "1" };
                order.SetClient(client);
                var r = order.AddNote(item).Result;
                if (server.Error != null) throw server.Error;
                Assert.AreEqual("11299", r.Id);
                Assert.AreEqual("customer", r.UserId);
                Assert.AreEqual("Test", r.Description);
            }
        }

        [TestMethod]
        public void GetAreaCodesTest()
        {
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/orders/1/areaCodes", Helper.AccountId),
                ContentToSend = new StringContent(TestXmlStrings.OrderAreaCodes, Encoding.UTF8, "application/xml")
            }))
            {
                var client = Helper.CreateClient();
                var order = new Order { Id = "1" };
                order.SetClient(client);
                var list = order.GetAreaCodes().Result;
                if (server.Error != null) throw server.Error;
                Assert.AreEqual(1, list.Length);
                Assert.AreEqual("888", list[0].Code);
            }
        }

        [TestMethod]
        public void GetNpaNxxTest()
        {
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/orders/1/npaNxx", Helper.AccountId),
                ContentToSend = new StringContent(TestXmlStrings.OrderNpaNxx, Encoding.UTF8, "application/xml")
            }))
            {
                var client = Helper.CreateClient();
                var order = new Order { Id = "1" };
                order.SetClient(client);
                var list = order.GetNpaNxx().Result;
                if (server.Error != null) throw server.Error;
                Assert.AreEqual(1, list.Length);
            }
        }

        [TestMethod]
        public void GetTotalsTest()
        {
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/orders/1/totals", Helper.AccountId),
                ContentToSend = new StringContent(TestXmlStrings.OrderTotals, Encoding.UTF8, "application/xml")
            }))
            {
                var client = Helper.CreateClient();
                var order = new Order { Id = "1" };
                order.SetClient(client);
                var list = order.GetTotals().Result;
                if (server.Error != null) throw server.Error;
                Assert.AreEqual(1, list.Length);
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
                    EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/orders/1/history", Helper.AccountId),
                    ContentToSend = new StringContent(TestXmlStrings.OrderHistory, Encoding.UTF8, "application/xml")
                }
            }))
            {
                var client = Helper.CreateClient();
                var i = new Order { Id = "1" };
                i.SetClient(client);
                var result = i.GetHistory().Result;
                if (server.Error != null) throw server.Error;
                Assert.IsTrue(result.Length > 0);
            }
        }
    }
}
