using Bandwidth.Iris.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bandwidth.Iris.Tests.Models
{
    [TestClass]
    public class DisconnectTests
    {
        [TestInitialize]
        public void Setup()
        {
            Helper.SetEnvironmetVariables();
        }

        [TestMethod]
        public void DisconnectNumbersTest()
        {
            var data = new DisconnectTelephoneNumberOrder
            {
                Name = "order",
                DisconnectTelephoneNumberOrderType = new DisconnectTelephoneNumberOrderType
                {
                    TelephoneNumberList = new[] {"111", "222"}
                }
            };
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "POST",
                EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/disconnects", Helper.AccountId),
                EstimatedContent = Helper.ToXmlString(data)
            }))
            {
                var client = Helper.CreateClient();
                Disconnect.DisconnectNumbers(client, "order", "111", "222").Wait();
                if (server.Error != null) throw server.Error;
            }
        }

        public void DisconnectNumbersWithDefaultClientTest()
        {
            var data = new DisconnectTelephoneNumberOrder
            {
                Name = "order",
                DisconnectTelephoneNumberOrderType = new DisconnectTelephoneNumberOrderType
                {
                    TelephoneNumberList = new[] { "111", "222" }
                }
            };
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "POST",
                EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/disconnects", Helper.AccountId),
                EstimatedContent = Helper.ToXmlString(data)
            }))
            {
                Disconnect.DisconnectNumbers("order", "111", "222").Wait();
                if (server.Error != null) throw server.Error;
            }
        }
      
    }
}
