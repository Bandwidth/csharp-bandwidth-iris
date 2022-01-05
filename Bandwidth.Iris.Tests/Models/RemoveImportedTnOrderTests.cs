using System;
using System.Net.Http;
using System.Text;
using Bandwidth.Iris.Model;
using Xunit;

namespace Bandwidth.Iris.Tests.Models
{
    
    public class RemoveImportedTnOrderTests
    {

        // [TestInitialize]
        public void Setup()
        {
            Helper.SetEnvironmetVariables();
        }

        [Fact]
        public void TestCreate()
        {
            var order = new RemoveImportedTnOrder
            {
                
            };

            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "POST",
                EstimatedPathAndQuery = $"/v1.0/accounts/{Helper.AccountId}/removeImportedTnOrders",
                ContentToSend = new StringContent(TestXmlStrings.RemoveImportedOrderResponse, Encoding.UTF8, "application/xml")
            }))
            {
                var client = Helper.CreateClient();
                var result = RemoveImportedTnOrder.Create(client, order).Result;
                if (server.Error != null) throw server.Error;

                Assert.Equal(result.RemoveImportedTnOrder.CustomerOrderId, "SJM000001");
                Assert.Equal(result.RemoveImportedTnOrder.OrderCreateDate, "2018-01-20T02:59:54.000Z");
                Assert.Equal(result.RemoveImportedTnOrder.AccountId, "9900012");
                Assert.Equal(result.RemoveImportedTnOrder.CreatedByUser, "smckinnon");
                Assert.Equal(result.RemoveImportedTnOrder.OrderId, "b05de7e6-0cab-4c83-81bb-9379cba8efd0");
                Assert.Equal(result.RemoveImportedTnOrder.LastModifiedDate, "2018-01-20T02:59:54.000Z");
                Assert.Equal(result.RemoveImportedTnOrder.ProcessingStatus, "PROCESSING");

                Assert.Equal(result.RemoveImportedTnOrder.TelephoneNumbers.Length, 4);

            }

        }

        [Fact]
        public void TestList()
        {
            var order = new RemoveImportedTnOrder
            {

            };

            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = $"/v1.0/accounts/{Helper.AccountId}/removeImportedTnOrders",
                ContentToSend = new StringContent(TestXmlStrings.RemoveImportedTnOrders, Encoding.UTF8, "application/xml")
            }))
            {
                var client = Helper.CreateClient();
                var result = RemoveImportedTnOrder.List(client, null).Result;
                if (server.Error != null) throw server.Error;

                Assert.Equal(result.TotalCount, 7);
                Assert.Equal(result.Items.Length, 7);

                Assert.Equal(result.Items[0].CustomerOrderId, "custom string");
                Assert.Equal(result.Items[0].userId, "jmulford-api");
                Assert.Equal(result.Items[0].lastModifiedDate, "2020-02-03T18:08:44.256Z");
                Assert.Equal(result.Items[0].OrderDate, "2020-02-03T18:08:44.199Z");
                Assert.Equal(result.Items[0].OrderType, "remove_imported_tn_orders");
                Assert.Equal(result.Items[0].OrderStatus, "FAILED");
                Assert.Equal(result.Items[0].OrderId, "5bb3b642-cbbb-4438-9a44-56069550d603");

            }

        }

        [Fact]
        public void TestGet()
        {
            var order = new RemoveImportedTnOrder
            {
                OrderId = "id"
            };

            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = $"/v1.0/accounts/{Helper.AccountId}/removeImportedTnOrders/{order.OrderId}",
                ContentToSend = new StringContent(TestXmlStrings.RemoveImportedOrder, Encoding.UTF8, "application/xml")
            }))
            {
                var client = Helper.CreateClient();
                var result = RemoveImportedTnOrder.Get(client, order.OrderId).Result;
                if (server.Error != null) throw server.Error;

                Assert.Equal(result.CustomerOrderId, "SJM000001");
                Assert.Equal(result.OrderCreateDate, "2018-01-20T02:59:54.000Z");
                Assert.Equal(result.AccountId, "9900012");
                Assert.Equal(result.CreatedByUser, "smckinnon");
                Assert.Equal(result.OrderId, "b05de7e6-0cab-4c83-81bb-9379cba8efd0");
                Assert.Equal(result.LastModifiedDate, "2018-01-20T02:59:54.000Z");
                Assert.Equal(result.ProcessingStatus, "PROCESSING");

                Assert.Equal(result.TelephoneNumbers.Length, 4);

            }

        }

        [Fact]
        public void TestGetHistory()
        {
            var order = new RemoveImportedTnOrder
            {
                OrderId = "id"
            };

            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = $"/v1.0/accounts/{Helper.AccountId}/removeImportedTnOrders/{order.OrderId}/history",
                ContentToSend = new StringContent(TestXmlStrings.OrderHistoryWrapper, Encoding.UTF8, "application/xml")
            }))
            {
                var client = Helper.CreateClient();
                var result = RemoveImportedTnOrder.GetHistory(client, order.OrderId).Result;
                if (server.Error != null) throw server.Error;

                

            }

        }

    }
}
