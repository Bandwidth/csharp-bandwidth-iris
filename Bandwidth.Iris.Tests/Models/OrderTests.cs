using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Bandwidth.Iris.Model;
using Xunit;

namespace Bandwidth.Iris.Tests.Models
{

    public class OrderTests
    {
        // [TestInitialize]
        public OrderTests()
        {
            Helper.SetEnvironmentVariables();
        }

        [Fact]
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

        [Fact]
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
                Assert.Equal("1", o.Id);
                Assert.Equal("2858", o.SiteId);
                Assert.Equal("A New Order", o.Name);
                Assert.Equal(DateTime.Parse("2014-10-14T17:58:15.299Z").ToUniversalTime(), o.OrderCreateDate);
                Assert.False(o.BackOrderRequested);
                Assert.Equal("2052865046", o.ExistingTelephoneNumberOrderType.TelephoneNumberList[0]);
                Assert.False(o.PartialAllowed);
            }
        }

        [Fact]
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

        [Fact]
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

        [Fact]
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


                Assert.Equal(122, result.Orders.TotalCount);
                Assert.Equal(3, result.Orders.OrderDetails.Count);
                Assert.Equal(1, result.Orders.OrderDetails[0].CountOfTns);
                Assert.Equal("COMPLETE", result.Orders.OrderDetails[0].OrderStatus);
                Assert.Equal("c5d8d076-345c-45d7-87b3-803a35cca89b", result.Orders.OrderDetails[0].OrderId);
                Assert.Equal("2013-12-20T06", result.Orders.OrderDetails[0].LastModifiedDate);
                Assert.Equal("2013-12-20T06", result.Orders.OrderDetails[0].OrderDate);
                Assert.Equal("bwc_user", result.Orders.OrderDetails[0].UserId);
                Assert.Single(result.Orders.OrderDetails[0].TelephoneNumberDetailsWithCount.States);
                Assert.Equal(1, result.Orders.OrderDetails[0].TelephoneNumberDetailsWithCount.States[0].Count);
                Assert.Equal("VA", result.Orders.OrderDetails[0].TelephoneNumberDetailsWithCount.States[0].State);
                Assert.Single(result.Orders.OrderDetails[0].TelephoneNumberDetailsWithCount.RateCenters);
                Assert.Equal(1, result.Orders.OrderDetails[0].TelephoneNumberDetailsWithCount.RateCenters[0].Count);
                Assert.Equal("LADYSMITH", result.Orders.OrderDetails[0].TelephoneNumberDetailsWithCount.RateCenters[0].RateCenter);
                Assert.Single(result.Orders.OrderDetails[0].TelephoneNumberDetailsWithCount.Cities);
                Assert.Equal(1, result.Orders.OrderDetails[0].TelephoneNumberDetailsWithCount.Cities[0].Count);
                Assert.Equal("LADYSMITH", result.Orders.OrderDetails[0].TelephoneNumberDetailsWithCount.Cities[0].City);
                Assert.Single(result.Orders.OrderDetails[0].TelephoneNumberDetailsWithCount.Tiers);
                Assert.Equal(1, result.Orders.OrderDetails[0].TelephoneNumberDetailsWithCount.Tiers[0].Count);
                Assert.Equal(0, result.Orders.OrderDetails[0].TelephoneNumberDetailsWithCount.Tiers[0].Tier);
                Assert.Single(result.Orders.OrderDetails[0].TelephoneNumberDetailsWithCount.Vendors);
                Assert.Equal(1, result.Orders.OrderDetails[0].TelephoneNumberDetailsWithCount.Vendors[0].Count);
                Assert.Equal(49, result.Orders.OrderDetails[0].TelephoneNumberDetailsWithCount.Vendors[0].VendorId);
                Assert.Equal("Bandwidth CLEC", result.Orders.OrderDetails[0].TelephoneNumberDetailsWithCount.Vendors[0].VendorName);
            }
        }

        [Fact]
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
        [Fact]
        public void UpdateTest()
        {
            var item = new Order { Id = "101" };
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

        [Fact]
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
                Assert.Equal("11299", r.Id);
                Assert.Equal("customer", r.UserId);
                Assert.Equal("Test", r.Description);
            }
        }

        [Fact]
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
                Assert.Single(list);
                Assert.Equal("888", list[0].Code);
            }
        }

        [Fact]
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
                Assert.Single(list);
            }
        }

        [Fact]
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
                Assert.Single(list);
            }
        }

        [Fact]
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
                Assert.True(result.Length > 0);
            }
        }
    }
}
