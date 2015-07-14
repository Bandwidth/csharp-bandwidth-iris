using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Bandwidth.Iris.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bandwidth.Iris.Tests.Models
{
    [TestClass]
    public class LineOptionTests
    {
        [TestInitialize]
        public void Setup()
        {
            Helper.SetEnvironmetVariables();
        }

        [TestMethod]
        public void CreateTest()
        {
            var item = new TnLineOptions
            {
                TelephoneNumber = "5209072451<",
                CallingNameDisplay = "off"
            };
           
            using (var server = new HttpServer(new[]
            {
                new RequestHandler
                {
                    EstimatedMethod = "POST",
                    EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/lineOptionOrders", Helper.AccountId),
                    EstimatedContent = Helper.ToXmlString(new LineOptionOrderRequest{TnLineOptions = new []{item}}),
                    ContentToSend = new StringContent(TestXmlStrings.LineOption, Encoding.UTF8, "application/xml"),
                }
            }))
            {
                var client = Helper.CreateClient();
                var i = LineOptionOrder.Create(client, item).Result;
                if (server.Error != null) throw server.Error;
                Assert.AreEqual(1, i.Length);
                Assert.AreEqual("2013223685", i[0]);
            }

        }

        [TestMethod]
        public void CreateWithDefaultClientTest()
        {
            var item = new TnLineOptions
            {
                TelephoneNumber = "5209072451<",
                CallingNameDisplay = "off"
            };

            using (var server = new HttpServer(new[]
            {
                new RequestHandler
                {
                    EstimatedMethod = "POST",
                    EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/lineOptionOrders", Helper.AccountId),
                    EstimatedContent = Helper.ToXmlString(new LineOptionOrderRequest{TnLineOptions = new []{item}}),
                    ContentToSend = new StringContent(TestXmlStrings.LineOption, Encoding.UTF8, "application/xml"),
                }
            }))
            {
                var i = LineOptionOrder.Create(item).Result;
                if (server.Error != null) throw server.Error;
                Assert.AreEqual(1, i.Length);
                Assert.AreEqual("2013223685", i[0]);
            }
        }
    }
}
