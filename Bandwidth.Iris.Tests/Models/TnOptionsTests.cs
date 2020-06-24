using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Bandwidth.Iris.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bandwidth.Iris.Tests.Models
{
    [TestClass]
    public class TnOptionsTests
    {
        [TestInitialize]
        public void Setup()
        {
            Helper.SetEnvironmetVariables();
        }

        [TestMethod]
        public void TestGet()
        {

            string orderId = "1234";

            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = $"/v1.0/accounts/{Helper.AccountId}/tnoptions/{orderId}",
                ContentToSend = new StringContent(TestXmlStrings.getTnOptions, Encoding.UTF8, "application/xml")
                
            }))
            {
                var client = Helper.CreateClient();
                var result = TnOptions.Get(client, orderId).Result;
                if (server.Error != null) throw server.Error;

                Assert.IsNotNull(result);
                Assert.AreEqual("14", result.AccountId);
                Assert.AreEqual("2016-01-15T11:22:58.789Z", result.OrderCreateDate);
                Assert.AreEqual("jbm", result.CreatedByUser);
                Assert.AreEqual("409033ee-88ec-43e3-85f3-538f30733963", result.OrderId);
                Assert.AreEqual("2016-01-15T11:22:58.969Z", result.LastModifiedDate);
                Assert.AreEqual("COMPLETE", result.ProcessingStatus);
                Assert.AreEqual(3, result.TnOptionGroups.Count);
                Assert.AreEqual("on", result.TnOptionGroups[0].CallingNameDisplay);
                Assert.AreEqual("on", result.TnOptionGroups[0].Sms);
                Assert.AreEqual("2174101601", result.TnOptionGroups[0].TelephoneNumbers[0]);
                Assert.AreEqual("sip:+12345678901@1.2.3.4:5060", result.TnOptionGroups[2].FinalDestinationURI);
                Assert.AreEqual("2174101601", result.Warnings[0].TelephoneNumber);
                Assert.AreEqual("SMS is already Enabled or number is in processing.", result.Warnings[0].Description);

            }
        }

        [TestMethod]
        public void TestList()
        {
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = $"/v1.0/accounts/{Helper.AccountId}/tnoptions?status=9199918388",
                ContentToSend = new StringContent(TestXmlStrings.listTnOptions, Encoding.UTF8, "application/xml")
            }))
            {
                var client = Helper.CreateClient();
                TnOptionOrders result = null;
                try
                {
                    result = TnOptions.List(client, new Dictionary<string, Object>
                {
                    {"status", "9199918388" }
                }).Result;
                    throw new Exception("Should have found error");
                } catch(Exception ex)
                {
                    Assert.IsNotNull(ex);
                    if (ex.InnerException is BandwidthIrisException)
                    {
                        var exInner = (BandwidthIrisException)ex.InnerException;
                        Console.WriteLine(exInner.Message); //"Telephone number is not available"
                        Console.WriteLine(exInner.Body);
                    }
                }
                if (server.Error != null) throw server.Error;

                Assert.IsNull(result);
                
            }
        }

        [TestMethod]
        public void TestListNoError()
        {
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = $"/v1.0/accounts/{Helper.AccountId}/tnoptions?status=9199918388",
                ContentToSend = new StringContent(TestXmlStrings.listTnOptionsNoError, Encoding.UTF8, "application/xml")
            }))
            {
                var client = Helper.CreateClient();
                TnOptionOrders result = null;
                try
                {
                    result = TnOptions.List(client, new Dictionary<string, Object>
                {
                    {"status", "9199918388" }
                }).Result;
                }
                catch (Exception ex)
                {
                    Assert.IsNull(ex, "No Error should be thrown");
                }
                if (server.Error != null) throw server.Error;

                Assert.IsNotNull(result);
                Assert.AreEqual(2, result.TotalCount);
                Assert.AreEqual(2, result.TnOptionOrderList.Count);
                Assert.AreEqual("14", result.TnOptionOrderList[0].AccountId);
                Assert.AreEqual("2016-01-15T12:01:14.324Z", result.TnOptionOrderList[0].OrderCreateDate);
                Assert.AreEqual("jbm", result.TnOptionOrderList[0].CreatedByUser);
                Assert.AreEqual("ddbdc72e-dc27-490c-904e-d0c11291b095", result.TnOptionOrderList[0].OrderId);
                Assert.AreEqual("2016-01-15T12:01:14.363Z", result.TnOptionOrderList[0].LastModifiedDate);
                Assert.AreEqual("FAILED", result.TnOptionOrderList[0].ProcessingStatus);
                Assert.AreEqual(2, result.TnOptionOrderList[0].TnOptionGroups.Count);
            }
        }

        [TestMethod]
        public void TestListWithSummary()
        {
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = $"/v1.0/accounts/{Helper.AccountId}/tnoptions?status=9199918388",
                ContentToSend = new StringContent(TestXmlStrings.listTnOptionsSummary, Encoding.UTF8, "application/xml")
            }))
            {
                var client = Helper.CreateClient();
                var result = TnOptions.List(client, new Dictionary<string, Object>
                {
                    {"status", "9199918388" }
                }).Result;
                if (server.Error != null) throw server.Error;

                Assert.IsNotNull(result);
                Assert.AreEqual(2, result.TotalCount);
                Assert.AreEqual(2, result.TnOptionOrderSummaryList.Count);
                Assert.AreEqual("14", result.TnOptionOrderSummaryList[0].AccountId);
                Assert.AreEqual(2, result.TnOptionOrderSummaryList[0].CountOfTNs);
                Assert.AreEqual("jbm", result.TnOptionOrderSummaryList[0].UserId);
                Assert.AreEqual("2016-01-15T12:01:14.363Z", result.TnOptionOrderSummaryList[0].LastModifiedDate);
                Assert.AreEqual("2016-01-15T12:01:14.324Z", result.TnOptionOrderSummaryList[0].OrderDate);
                Assert.AreEqual("tn_option", result.TnOptionOrderSummaryList[0].OrderType);
                Assert.AreEqual("FAILED", result.TnOptionOrderSummaryList[0].OrderStatus);
                Assert.AreEqual("ddbdc72e-dc27-490c-904e-d0c11291b095", result.TnOptionOrderSummaryList[0].OrderId);
            }
        }

        [TestMethod]
        public void TestCreate()
        {

            var order = new TnOptionOrder
            {
                CustomerOrderId = "customerOrderId",
                TnOptionGroups = new List<TnOptionGroup>
                {
                    new TnOptionGroup {
                        PortOutPasscode = "a1b2c3",
                        TelephoneNumbers = new List<string>
                        {
                            "2018551020",
                            "2018551025"
                        }
                    },
                    new TnOptionGroup {
                        Sms = "on",
                        TelephoneNumbers = new List<string>
                        {
                            "2018551020",
                            "2018551025"
                        }
                    },
                    new TnOptionGroup {
                        CallForward = "6042661720",
                        TelephoneNumbers = new List<string>
                        {
                            "2018551020",
                            "2018551025"
                        }
                    }
                }
            };

            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "POST",
                EstimatedPathAndQuery = $"/v1.0/accounts/{Helper.AccountId}/tnoptions",
                ContentToSend = new StringContent(TestXmlStrings.createTnOptionsResponse, Encoding.UTF8, "application/xml"),
                EstimatedContent = @"<?xml version=""1.0"" encoding=""utf-8""?>
<TnOptionOrder xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <CustomerOrderId>customerOrderId</CustomerOrderId>
  <TnOptionGroups>
    <TnOptionGroup>
      <PortOutPasscode>a1b2c3</PortOutPasscode>
      <TelephoneNumbers>
        <TelephoneNumber>2018551020</TelephoneNumber>
        <TelephoneNumber>2018551025</TelephoneNumber>
      </TelephoneNumbers>
    </TnOptionGroup>
    <TnOptionGroup>
      <Sms>on</Sms>
      <TelephoneNumbers>
        <TelephoneNumber>2018551020</TelephoneNumber>
        <TelephoneNumber>2018551025</TelephoneNumber>
      </TelephoneNumbers>
    </TnOptionGroup>
    <TnOptionGroup>
      <CallForward>6042661720</CallForward>
      <TelephoneNumbers>
        <TelephoneNumber>2018551020</TelephoneNumber>
        <TelephoneNumber>2018551025</TelephoneNumber>
      </TelephoneNumbers>
    </TnOptionGroup>
  </TnOptionGroups>
</TnOptionOrder>"
            }))
            {
                var client = Helper.CreateClient();
                var result = TnOptions.Create(client, order).Result;
                if (server.Error != null) throw server.Error;

                Assert.IsNotNull(result.TnOptionOrder);
                Assert.AreEqual("2016-01-15T12:01:14.324Z", result.TnOptionOrder.OrderCreateDate);
                Assert.AreEqual("14", result.TnOptionOrder.AccountId);
                Assert.AreEqual("jbm", result.TnOptionOrder.CreatedByUser);
                Assert.AreEqual("ddbdc72e-dc27-490c-904e-d0c11291b095", result.TnOptionOrder.OrderId);
                Assert.AreEqual("2016-01-15T12:01:14.324Z", result.TnOptionOrder.LastModifiedDate);
                Assert.AreEqual("RECEIVED", result.TnOptionOrder.ProcessingStatus);
                Assert.IsNotNull(result.TnOptionOrder.TnOptionGroups);
                Assert.AreEqual(2, result.TnOptionOrder.TnOptionGroups.Count);

            }
        }

        

    }

}
