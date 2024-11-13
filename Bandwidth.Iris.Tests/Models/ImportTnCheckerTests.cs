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

                Assert.Single(result.ImportTnCheckerPayload.TelephoneNumbers);
                Assert.Single(result.ImportTnCheckerPayload.ImportTnErrors);

                Assert.Equal(19006, result.ImportTnCheckerPayload.ImportTnErrors[0].Code);
                Assert.Equal("Bandwidth numbers cannot be imported by this account at this time.", result.ImportTnCheckerPayload.ImportTnErrors[0].Description);
                Assert.Equal(2, result.ImportTnCheckerPayload.ImportTnErrors[0].TelephoneNumbers.Length);

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
                xmlStringResult = writer.ToString();

                var strippedContent = Regex.Replace(xmlStringResult, ">[ \r\n]+<", "><");
                var strippedEstimated = Regex.Replace(TestXmlStrings.ImportTnCheckerSampleSerialization, ">[ \r\n]+<", "><");
                Assert.Equal(strippedEstimated, strippedContent);
            }
        }
    }
}
