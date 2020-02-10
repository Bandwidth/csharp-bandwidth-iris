using System;
using System.Net.Http;
using System.Text;
using Bandwidth.Iris.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bandwidth.Iris.Tests.Models
{
    [TestClass]
    public class ImportTnCheckerTests
    {
        [TestMethod]
        public void TestCreate()
        {

            var order = new ImportTnCheckerPayload
            {
                TelephoneNumbers = new TelephoneNumber[]
               {
                   new TelephoneNumber
                   {
                       FullNumber = "3032281000"
                   }
               }
            };

            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "POST",
                EstimatedPathAndQuery = $"/v1.0/accounts/{Helper.AccountId}/importTnChecker",
                ContentToSend = new StringContent(TestXmlStrings.ImportTnCheckerResponse, Encoding.UTF8, "application/xml")
            }))
            {
                var client = Helper.CreateClient();
                var result = ImportTnChecker.Create(client, order).Result;
                if (server.Error != null) throw server.Error;

                Assert.AreEqual(result.ImportTnCheckerPayload.TelephoneNumbers.Length, 1);
                Assert.AreEqual(result.ImportTnCheckerPayload.ImportTnErrors.Length, 1);

                Assert.AreEqual(result.ImportTnCheckerPayload.ImportTnErrors[0].Code, 19006);
                Assert.AreEqual(result.ImportTnCheckerPayload.ImportTnErrors[0].Description, "Bandwidth numbers cannot be imported by this account at this time.");
                Assert.AreEqual(result.ImportTnCheckerPayload.ImportTnErrors[0].TelephoneNumbers.Length, 2);

            }
        }
    }
}
