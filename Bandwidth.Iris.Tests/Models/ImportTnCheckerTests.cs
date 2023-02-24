using System;
using System.Net.Http;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using Bandwidth.Iris.Model;
using System.Text.RegularExpressions;
using Xunit;

namespace Bandwidth.Iris.Tests.Models
{

    public class ImportTnCheckerTests
    {
        [Fact]
        public void TestCreate()
        {

            var order = new ImportTnCheckerPayload
            {
                SiteId = "486",
                SipPeerId = "500025",
                TelephoneNumbers = new string[]
                {
                    "3032281000"
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

                Assert.Equal(result.ImportTnCheckerPayload.TelephoneNumbers.Length, 1);
                Assert.Equal(result.ImportTnCheckerPayload.ImportTnErrors.Length, 1);

                Assert.Equal(result.ImportTnCheckerPayload.ImportTnErrors[0].Code, 19006);
                Assert.Equal(result.ImportTnCheckerPayload.ImportTnErrors[0].Description, "Bandwidth numbers cannot be imported by this account at this time.");
                Assert.Equal(result.ImportTnCheckerPayload.ImportTnErrors[0].TelephoneNumbers.Length, 2);

            }
        }

        [Fact]
        public void TestSerialize()
        {
            var order = new ImportTnCheckerPayload
            {
                SiteId = "486",
                SipPeerId = "500025",
                TelephoneNumbers = new string[]
                {
                    "3032281000"
                }
            };

            XmlSerializer xs = new XmlSerializer(typeof(ImportTnCheckerPayload));

            string xmlStringResult = null;
            using (StringWriter writer = new StringWriter())
            {
                xs.Serialize(writer, order);
                xmlStringResult = writer.ToString().Replace("\r", "").Replace("\n", "");


                var strippedContent = Regex.Replace(xmlStringResult, ">[ \r\n]+<", "><");
                var strippedEstimated = Regex.Replace(TestXmlStrings.ImportTnCheckerSampleSerialization, ">[ \r\n]+<", "><");
                Assert.Equal(strippedEstimated, strippedContent);
            }
        }
    }
}
