using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Bandwidth.Iris.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bandwidth.Iris.Tests.Models
{
    [TestClass]
    public class AeuiTests
    {
        [TestInitialize]
        public void Setup()
        {
            Helper.SetEnvironmetVariables();
        }

        [TestMethod]
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

                Assert.IsNotNull(response);

                Assert.AreEqual("8042105760", response.AlternateEndUserIdentifier.CallbackNumber);
                Assert.AreEqual("DavidAcid", response.AlternateEndUserIdentifier.Identifier);
                Assert.AreEqual("David", response.AlternateEndUserIdentifier.E911.CallerName);
                Assert.IsNotNull(response.AlternateEndUserIdentifier.E911.Address);
                Assert.IsNotNull(response.AlternateEndUserIdentifier.E911.EmergencyNotificationGroup);



            }
        }

        [TestMethod]
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

                Assert.IsNotNull(response);

                Assert.AreEqual(2, response.TotalCount);

                Assert.IsNotNull(response.Links);
                Assert.IsNotNull(response.AlternateEndUserIdentifiers);
                Assert.AreEqual(2, response.AlternateEndUserIdentifiers.Length);

            }
        }
    }
}
