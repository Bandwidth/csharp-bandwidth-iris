using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Bandwidth.Iris.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bandwidth.Iris.Tests.Models
{
    [TestClass]
    public class ImportTnOrderTests
    {
        [TestInitialize]
        public void Setup()
        {
            Helper.SetEnvironmetVariables();
        }

        [TestMethod]
        public void TestImportTnOrderCreate()
        {
            var order = new ImportTnOrder
            {
                AccountId = "account",
                SipPeerId = 1,
                SiteId = 2
            };

            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "POST",
                EstimatedPathAndQuery = $"/v1.0/accounts/{Helper.AccountId}/importTnOrders",
                ContentToSend = new StringContent(TestXmlStrings.ImportTnOrderResponse, Encoding.UTF8, "application/xml")
            }))
            {
                var client = Helper.CreateClient();
                var result = ImportTnOrder.Create(client, order).Result;
                if (server.Error != null) throw server.Error;

                Assert.AreEqual(result.ImportTnOrder.CustomerOrderId, "SJM000001");
                Assert.AreEqual(result.ImportTnOrder.OrderCreateDate, "2018-01-20T02:59:54.000Z");
                Assert.AreEqual(result.ImportTnOrder.AccountId, "9900012");
                Assert.AreEqual(result.ImportTnOrder.CreatedByUser, "smckinnon");
                Assert.AreEqual(result.ImportTnOrder.OrderId, "b05de7e6-0cab-4c83-81bb-9379cba8efd0");
                Assert.AreEqual(result.ImportTnOrder.LastModifiedDate, "2018-01-20T02:59:54.000Z");
                Assert.AreEqual(result.ImportTnOrder.SiteId, 202);
                Assert.AreEqual(result.ImportTnOrder.SipPeerId, 520565);
                Assert.AreEqual(result.ImportTnOrder.ProcessingStatus, "PROCESSING");

                Assert.IsNotNull(result.ImportTnOrder.Subscriber);

                Assert.AreEqual(result.ImportTnOrder.TelephoneNumbers.Length, 4);

            }
        }

        [TestMethod]
        public void TestImportTnOrderGet()
        {
            var order = new ImportTnOrder
            {
                OrderId = "1",
                AccountId = "account",
                SipPeerId = 1,
                SiteId = 2
            };

            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = $"/v1.0/accounts/{Helper.AccountId}/importTnOrders/{order.OrderId}",
                ContentToSend = new StringContent(TestXmlStrings.ImportTnOrder, Encoding.UTF8, "application/xml")
            }))
            {
                var client = Helper.CreateClient();
                var result = ImportTnOrder.Get(client, order.OrderId).Result;
                if (server.Error != null) throw server.Error;

                Assert.AreEqual(result.CustomerOrderId, "SJM000001");
                Assert.AreEqual(result.OrderCreateDate, "2018-01-20T02:59:54.000Z");
                Assert.AreEqual(result.AccountId, "9900012");
                Assert.AreEqual(result.CreatedByUser, "smckinnon");
                Assert.AreEqual(result.OrderId, "b05de7e6-0cab-4c83-81bb-9379cba8efd0");
                Assert.AreEqual(result.LastModifiedDate, "2018-01-20T02:59:54.000Z");
                Assert.AreEqual(result.SiteId, 202);
                Assert.AreEqual(result.SipPeerId, 520565);
                Assert.AreEqual(result.ProcessingStatus, "PROCESSING");

                Assert.IsNotNull(result.Subscriber);

                Assert.AreEqual(result.TelephoneNumbers.Length, 4);

            }
        }

        [TestMethod]
        public void TestImportTnOrderList()
        {
            var order = new ImportTnOrder
            {
                OrderId = "fbd17609-be44-48e7-a301-90bd6cf42248",
                AccountId = "account",
                SipPeerId = 1,
                SiteId = 2
            };

            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = $"/v1.0/accounts/{Helper.AccountId}/importTnOrders?accountId=1",
                ContentToSend = new StringContent(TestXmlStrings.ImportTnOrders, Encoding.UTF8, "application/xml")
            }))
            {
                var client = Helper.CreateClient();
                var result = ImportTnOrder.List(client, new Dictionary<string, object> { { "accountId", "1" } }).Result;
                if (server.Error != null) throw server.Error;

                Assert.AreEqual(result.TotalCount, 14);
                Assert.AreEqual(result.ImportTnOrderSummarys.Length, 14);
                Assert.AreEqual(result.ImportTnOrderSummarys[0].accountId, 9900778);
                Assert.AreEqual(result.ImportTnOrderSummarys[0].CountOfTNs, 1);
                Assert.AreEqual(result.ImportTnOrderSummarys[0].CustomerOrderId, "id");
                Assert.AreEqual(result.ImportTnOrderSummarys[0].userId, "jmulford-api");
                Assert.AreEqual(result.ImportTnOrderSummarys[0].lastModifiedDate, "2020-02-04T14:09:08.937Z");
                Assert.AreEqual(result.ImportTnOrderSummarys[0].OrderDate, "2020-02-04T14:09:07.824Z");
                Assert.AreEqual(result.ImportTnOrderSummarys[0].OrderType, "import_tn_orders");
                Assert.AreEqual(result.ImportTnOrderSummarys[0].OrderStatus, "FAILED");
                Assert.AreEqual(result.ImportTnOrderSummarys[0].OrderId, "fbd17609-be44-48e7-a301-90bd6cf42248");

            }
        }

        [TestMethod]
        public void TestImportTnOrderHistory()
        {
            var order = new ImportTnOrder
            {
                OrderId = "fbd17609-be44-48e7-a301-90bd6cf42248",
                AccountId = "account",
                SipPeerId = 1,
                SiteId = 2
            };

            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = $"/v1.0/accounts/{Helper.AccountId}/importTnOrders/{order.OrderId}/history",
                ContentToSend = new StringContent(TestXmlStrings.OrderHistoryWrapper, Encoding.UTF8, "application/xml")
            }))
            {
                var client = Helper.CreateClient();
                var result = ImportTnOrder.GetHistory(client, order.OrderId).Result;
                if (server.Error != null) throw server.Error;

                Assert.AreEqual(result.Items.Length, 2);
                Assert.AreEqual(result.Items[0].OrderDate, DateTime.Parse("2020-02-04T14:09:07.824"));
                Assert.AreEqual(result.Items[0].Note, "Import TN order has been received by the system.");
                Assert.AreEqual(result.Items[0].Author, "jmulford-api");
                Assert.AreEqual(result.Items[0].Status, "received");

            }
        }
    }
}
