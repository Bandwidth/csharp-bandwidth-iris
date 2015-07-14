﻿using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Bandwidth.Iris.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bandwidth.Iris.Tests.Models
{
    [TestClass]
    public class InServiceNumberTests
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
                EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/inserviceNumbers", Helper.AccountId),
                ContentToSend = new  StringContent(TestXmlStrings.InServiceNumbers, Encoding.UTF8, "application/xml")
            }))
            {
                var client = Helper.CreateClient();
                var result = InServiceNumber.List(client).Result;
                if (server.Error != null) throw server.Error;
                Assert.AreEqual(15, result.Length);
            }
        }

        [TestMethod]
        public void ListWithDefaultClientTest()
        {
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/inserviceNumbers", Helper.AccountId),
                ContentToSend = new StringContent(TestXmlStrings.InServiceNumbers, Encoding.UTF8, "application/xml")
            }))
            {
                var result = InServiceNumber.List().Result;
                if (server.Error != null) throw server.Error;
                Assert.AreEqual(15, result.Length);
            }
        }

        [TestMethod]
        public void GetTotalsTest()
        {
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/inserviceNumbers/totals", Helper.AccountId),
                ContentToSend = new StringContent(TestXmlStrings.InServiceNumbersTotals, Encoding.UTF8, "application/xml")
            }))
            {
                var client = Helper.CreateClient();
                var result = InServiceNumber.GetTotals(client).Result;
                if (server.Error != null) throw server.Error;
                Assert.AreEqual(3, result.Count);
            }
        }

        [TestMethod]
        public void GetTotalsWithDefaultClientTest()
        {
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/inserviceNumbers/totals", Helper.AccountId),
                ContentToSend = new StringContent(TestXmlStrings.InServiceNumbersTotals, Encoding.UTF8, "application/xml")
            }))
            {
                var result = InServiceNumber.GetTotals().Result;
                if (server.Error != null) throw server.Error;
                Assert.AreEqual(3, result.Count);
            }
        }
    }
}
