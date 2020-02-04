using System;
using System.Net.Http;
using System.Text;
using Bandwidth.Iris.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bandwidth.Iris.Tests.Models
{
    [TestClass]
    public class RemoveImportedTnOrderTests
    {

        [TestInitialize]
        public void Setup()
        {
            Helper.SetEnvironmetVariables();
        }

        [TestMethod]
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

                Assert.AreEqual(result.RemoveImportedTnOrder.CustomerOrderId, "SJM000001");
                Assert.AreEqual(result.RemoveImportedTnOrder.OrderCreateDate, "2018-01-20T02:59:54.000Z");
                Assert.AreEqual(result.RemoveImportedTnOrder.AccountId, "9900012");
                Assert.AreEqual(result.RemoveImportedTnOrder.CreatedByUser, "smckinnon");
                Assert.AreEqual(result.RemoveImportedTnOrder.OrderId, "b05de7e6-0cab-4c83-81bb-9379cba8efd0");
                Assert.AreEqual(result.RemoveImportedTnOrder.LastModifiedDate, "2018-01-20T02:59:54.000Z");
                Assert.AreEqual(result.RemoveImportedTnOrder.ProcessingStatus, "PROCESSING");

                Assert.AreEqual(result.RemoveImportedTnOrder.TelephoneNumbers.Length, 4);

            }

        }

        [TestMethod]
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

                Assert.AreEqual(result.TotalCount, 7);
                Assert.AreEqual(result.Items.Length, 7);

                Assert.AreEqual(result.Items[0].CustomerOrderId, "custom string");
                Assert.AreEqual(result.Items[0].userId, "jmulford-api");
                Assert.AreEqual(result.Items[0].lastModifiedDate, "2020-02-03T18:08:44.256Z");
                Assert.AreEqual(result.Items[0].OrderDate, "2020-02-03T18:08:44.199Z");
                Assert.AreEqual(result.Items[0].OrderType, "remove_imported_tn_orders");
                Assert.AreEqual(result.Items[0].OrderStatus, "FAILED");
                Assert.AreEqual(result.Items[0].OrderId, "5bb3b642-cbbb-4438-9a44-56069550d603");

            }

        }

        [TestMethod]
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

                Assert.AreEqual(result.CustomerOrderId, "SJM000001");
                Assert.AreEqual(result.OrderCreateDate, "2018-01-20T02:59:54.000Z");
                Assert.AreEqual(result.AccountId, "9900012");
                Assert.AreEqual(result.CreatedByUser, "smckinnon");
                Assert.AreEqual(result.OrderId, "b05de7e6-0cab-4c83-81bb-9379cba8efd0");
                Assert.AreEqual(result.LastModifiedDate, "2018-01-20T02:59:54.000Z");
                Assert.AreEqual(result.ProcessingStatus, "PROCESSING");

                Assert.AreEqual(result.TelephoneNumbers.Length, 4);

            }

        }

        [TestMethod]
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
