using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Bandwidth.Iris.Model;
using Xunit;

namespace Bandwidth.Iris.Tests.Models
{

    public class LineOptionTests
    {
        // [TestInitialize]
        public LineOptionTests()
        {
            Helper.SetEnvironmentVariables();
        }

        [Fact]
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
                Assert.Equal(1, i.Length);
                Assert.Equal("2013223685", i[0]);
            }

        }

        [Fact]
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
                Assert.Equal(1, i.Length);
                Assert.Equal("2013223685", i[0]);
            }
        }
    }
}
