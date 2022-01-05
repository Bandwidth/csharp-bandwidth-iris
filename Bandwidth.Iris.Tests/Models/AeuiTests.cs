using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Bandwidth.Iris.Model;
using Xunit;

namespace Bandwidth.Iris.Tests.Models
{
    
    public class AeuiTests
    {
        // [TestInitialize]
        public void Setup()
        {
            Helper.SetEnvironmetVariables();
        }

        [Fact]
        public void TestGet()
        {
            string id = "12345";

            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = $"/v1.0/accounts/{Helper.AccountId}/aeuis/{id}",
                ContentToSend = new StringContent(TestXmlStrings.getAeui, Encoding.UTF8, "application/xml")
            }))
            {
                var client = Helper.CreateClient();

                var response = Aeui.Get(client, id).Result;

                Assert.NotNull(response);

                Assert.Equal("8042105760", response.AlternateEndUserIdentifier.CallbackNumber);
                Assert.Equal("DavidAcid", response.AlternateEndUserIdentifier.Identifier);
                Assert.Equal("David", response.AlternateEndUserIdentifier.E911.CallerName);
                Assert.NotNull(response.AlternateEndUserIdentifier.E911.Address);
                Assert.NotNull(response.AlternateEndUserIdentifier.E911.EmergencyNotificationGroup);



            }
        }

        [Fact]
        public void TestList()
        {
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = $"/v1.0/accounts/{Helper.AccountId}/aeuis",
                ContentToSend = new StringContent(TestXmlStrings.listAeui, Encoding.UTF8, "application/xml")
            }))
            {
                var client = Helper.CreateClient();

                var response = Aeui.List(client).Result;

                Assert.NotNull(response);

                Assert.Equal(2, response.TotalCount);

                Assert.NotNull(response.Links);
                Assert.NotNull(response.AlternateEndUserIdentifiers);
                Assert.Equal(2, response.AlternateEndUserIdentifiers.Length);

            }
        }
    }
}
