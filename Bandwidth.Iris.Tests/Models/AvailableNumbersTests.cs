using System.Collections.Generic;
using Bandwidth.Iris.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bandwidth.Iris.Tests.Models
{
    [TestClass]
    public class AvailableNumbersTests
    {
        [TestInitialize]
        public void Setup()
        {
            Helper.SetEnvironmetVariables();
        }

        [TestMethod]
        public void ListTest()
        {
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/availableNumbers?areaCode=866&quantity=5", Helper.AccountId),
                ContentToSend = Helper.CreateXmlContent(new AvailableNumbersResult
                {
                    ResultCount = 2,
                    TelephoneNumberList = new[] { "1111", "2222"}
                })
            }))
            {
                var client = Helper.CreateClient();
                var result = AvailableNumbers.List(client, new Dictionary<string, object>
                {
                    {"areaCode", 866},
                    {"quantity", 5}
                }).Result;
                if (server.Error != null) throw server.Error;
                Assert.AreEqual(2, result.ResultCount);
                CollectionAssert.AreEqual(new[] { "1111", "2222" }, result.TelephoneNumberList);
            }
        }
    }
}
