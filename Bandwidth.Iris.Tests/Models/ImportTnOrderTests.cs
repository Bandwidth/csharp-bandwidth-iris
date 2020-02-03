using Bandwidth.Iris.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bandwidth.Iris.Tests.Models
{
    [TestClass]
    class ImportTnOrderTests
    {

        [TestInitialize]
        public void Setup()
        {
            Helper.SetEnvironmetVariables();
        }

        [TestMethod]
        public void CreateTest()
        {

            string orderId = "id";

            var order = new ImportTnOrder
            {

            };



            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "POST",
                EstimatedPathAndQuery = $"/v1.0/accounts/{Helper.AccountId}/importTnOrders",
                ContentToSend = Helper.CreateXmlContent(TestXmlStrings.ImportTnOrderResponse)
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
                Assert.AreEqual(result.ImportTnOrder.SiteId, "202");
                Assert.AreEqual(result.ImportTnOrder.SipPeerId, "520565");
                Assert.AreEqual(result.ImportTnOrder.ProcessingStatus, "PROCESSING");

                Assert.IsNotNull(result.ImportTnOrder.Subscriber);

                Assert.AreEqual(result.ImportTnOrder.TelephoneNumbers.Length, 4);

            }
        }
    }
}
