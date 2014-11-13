﻿using System.Collections.Generic;
using Bandwidth.Iris.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bandwidth.Iris.Tests.Models
{
    [TestClass]
    public class AvailableNpaNxxTests
    {
        [TestInitialize]
        public void Setup()
        {
            Helper.SetEnvironmetVariables();
        }

        [TestMethod]
        public void ListTest()
        {
            var list = new[]
            {
                new AvailableNpaNxx
                {
                    City = "City1",
                    State = "State1",
                    Npa = "Npa1",
                    Nxx = "Nxx1",
                    Quantity = 10
                },
                new AvailableNpaNxx
                {
                    City = "City2",
                    State = "State2",
                    Npa = "Npa2",
                    Nxx = "Nxx2",
                    Quantity = 20
                }
            };
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/availableNpaNxx?areaCode=919", Helper.AccountId),
                ContentToSend = Helper.CreateXmlContent(new AvailableNpaNxxResult
                {
                    AvailableNpaNxxList = list
                })
            }))
            {
                var client = Helper.CreateClient();
                var result = AvailableNpaNxx.List(client, new Dictionary<string, object>
                {
                    {"areaCode", 919}
                }).Result;
                if (server.Error != null) throw server.Error;
                Assert.AreEqual(2, result.Length);
                Helper.AssertObjects(list[0], result[0]);
                Helper.AssertObjects(list[1], result[1]);
            }
        }

        [TestMethod]
        public void ListWithDefaultClientTest()
        {
            var list = new[]
            {
                new AvailableNpaNxx
                {
                    City = "City1",
                    State = "State1",
                    Npa = "Npa1",
                    Nxx = "Nxx1",
                    Quantity = 10
                },
                new AvailableNpaNxx
                {
                    City = "City2",
                    State = "State2",
                    Npa = "Npa2",
                    Nxx = "Nxx2",
                    Quantity = 20
                }
            };
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/availableNpaNxx?areaCode=919", Helper.AccountId),
                ContentToSend = Helper.CreateXmlContent(new AvailableNpaNxxResult
                {
                    AvailableNpaNxxList = list
                })
            }))
            {
                var result = AvailableNpaNxx.List(new Dictionary<string, object>
                {
                    {"areaCode", 919}
                }).Result;
                if (server.Error != null) throw server.Error;
                Assert.AreEqual(2, result.Length);
                Helper.AssertObjects(list[0], result[0]);
                Helper.AssertObjects(list[1], result[1]);
            }
        }
    }
}
