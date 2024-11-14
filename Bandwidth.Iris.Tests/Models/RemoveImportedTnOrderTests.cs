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
        public RemoveImportedTnOrderTests()
        {
            Helper.SetEnvironmentVariables();
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

                Assert.Equal("SJM000001", result.RemoveImportedTnOrder.CustomerOrderId);
                Assert.Equal("2018-01-20T02:59:54.000Z", result.RemoveImportedTnOrder.OrderCreateDate);
                Assert.Equal("9900012", result.RemoveImportedTnOrder.AccountId);
                Assert.Equal("smckinnon", result.RemoveImportedTnOrder.CreatedByUser);
                Assert.Equal("b05de7e6-0cab-4c83-81bb-9379cba8efd0", result.RemoveImportedTnOrder.OrderId);
                Assert.Equal("2018-01-20T02:59:54.000Z", result.RemoveImportedTnOrder.LastModifiedDate);
                Assert.Equal("PROCESSING", result.RemoveImportedTnOrder.ProcessingStatus);

                Assert.Equal(4, result.RemoveImportedTnOrder.TelephoneNumbers.Length);

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

                Assert.Equal(7, result.TotalCount);
                Assert.Equal(7, result.Items.Length);

                Assert.Equal("custom string", result.Items[0].CustomerOrderId);
                Assert.Equal("jmulford-api", result.Items[0].userId);
                Assert.Equal("2020-02-03T18:08:44.256Z", result.Items[0].lastModifiedDate);
                Assert.Equal("2020-02-03T18:08:44.199Z", result.Items[0].OrderDate);
                Assert.Equal("remove_imported_tn_orders", result.Items[0].OrderType);
                Assert.Equal("FAILED", result.Items[0].OrderStatus);
                Assert.Equal("5bb3b642-cbbb-4438-9a44-56069550d603", result.Items[0].OrderId);

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

                Assert.Equal("SJM000001", result.CustomerOrderId);
                Assert.Equal("2018-01-20T02:59:54.000Z", result.OrderCreateDate);
                Assert.Equal("9900012", result.AccountId);
                Assert.Equal("smckinnon", result.CreatedByUser);
                Assert.Equal("b05de7e6-0cab-4c83-81bb-9379cba8efd0", result.OrderId);
                Assert.Equal("2018-01-20T02:59:54.000Z", result.LastModifiedDate);
                Assert.Equal("PROCESSING", result.ProcessingStatus);

                Assert.Equal(4, result.TelephoneNumbers.Length);

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
