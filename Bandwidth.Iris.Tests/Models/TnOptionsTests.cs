using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Bandwidth.Iris.Model;
using Xunit;

namespace Bandwidth.Iris.Tests.Models
{

    public class TnOptionsTests
    {
        // [TestInitialize]
        public TnOptionsTests()
        {
            Helper.SetEnvironmetVariables();
        }

        [Fact]
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

                Assert.NotNull(result);
                Assert.Equal("14", result.AccountId);
                Assert.Equal("2016-01-15T11:22:58.789Z", result.OrderCreateDate);
                Assert.Equal("jbm", result.CreatedByUser);
                Assert.Equal("409033ee-88ec-43e3-85f3-538f30733963", result.OrderId);
                Assert.Equal("2016-01-15T11:22:58.969Z", result.LastModifiedDate);
                Assert.Equal("COMPLETE", result.ProcessingStatus);
                Assert.Equal(3, result.TnOptionGroups.Count);
                Assert.Equal("on", result.TnOptionGroups[0].CallingNameDisplay);
                Assert.Equal("on", result.TnOptionGroups[0].Sms);
                Assert.Equal("2174101601", result.TnOptionGroups[0].TelephoneNumbers[0]);
                Assert.Equal("sip:+12345678901@1.2.3.4:5060", result.TnOptionGroups[2].FinalDestinationURI);
                Assert.Equal("2174101601", result.Warnings[0].TelephoneNumber);
                Assert.Equal("SMS is already Enabled or number is in processing.", result.Warnings[0].Description);

            }
        }

        [Fact]
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
                    Assert.NotNull(ex);
                    if (ex.InnerException is BandwidthIrisException)
                    {
                        var exInner = (BandwidthIrisException)ex.InnerException;
                        Console.WriteLine(exInner.Message); //"Telephone number is not available"
                        Console.WriteLine(exInner.Body);
                    }
                }
                if (server.Error != null) throw server.Error;

                Assert.Null(result);

            }
        }

        [Fact]
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
                    Assert.Null(ex);
                }
                if (server.Error != null) throw server.Error;

                Assert.NotNull(result);
                Assert.Equal(2, result.TotalCount);
                Assert.Equal(2, result.TnOptionOrderList.Count);
                Assert.Equal("14", result.TnOptionOrderList[0].AccountId);
                Assert.Equal("2016-01-15T12:01:14.324Z", result.TnOptionOrderList[0].OrderCreateDate);
                Assert.Equal("jbm", result.TnOptionOrderList[0].CreatedByUser);
                Assert.Equal("ddbdc72e-dc27-490c-904e-d0c11291b095", result.TnOptionOrderList[0].OrderId);
                Assert.Equal("2016-01-15T12:01:14.363Z", result.TnOptionOrderList[0].LastModifiedDate);
                Assert.Equal("FAILED", result.TnOptionOrderList[0].ProcessingStatus);
                Assert.Equal(2, result.TnOptionOrderList[0].TnOptionGroups.Count);
            }
        }

        [Fact]
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

                Assert.NotNull(result);
                Assert.Equal(2, result.TotalCount);
                Assert.Equal(2, result.TnOptionOrderSummaryList.Count);
                Assert.Equal("14", result.TnOptionOrderSummaryList[0].AccountId);
                Assert.Equal(2, result.TnOptionOrderSummaryList[0].CountOfTNs);
                Assert.Equal("jbm", result.TnOptionOrderSummaryList[0].UserId);
                Assert.Equal("2016-01-15T12:01:14.363Z", result.TnOptionOrderSummaryList[0].LastModifiedDate);
                Assert.Equal("2016-01-15T12:01:14.324Z", result.TnOptionOrderSummaryList[0].OrderDate);
                Assert.Equal("tn_option", result.TnOptionOrderSummaryList[0].OrderType);
                Assert.Equal("FAILED", result.TnOptionOrderSummaryList[0].OrderStatus);
                Assert.Equal("ddbdc72e-dc27-490c-904e-d0c11291b095", result.TnOptionOrderSummaryList[0].OrderId);
            }
        }

        [Fact]
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

                Assert.NotNull(result.TnOptionOrder);
                Assert.Equal("2016-01-15T12:01:14.324Z", result.TnOptionOrder.OrderCreateDate);
                Assert.Equal("14", result.TnOptionOrder.AccountId);
                Assert.Equal("jbm", result.TnOptionOrder.CreatedByUser);
                Assert.Equal("ddbdc72e-dc27-490c-904e-d0c11291b095", result.TnOptionOrder.OrderId);
                Assert.Equal("2016-01-15T12:01:14.324Z", result.TnOptionOrder.LastModifiedDate);
                Assert.Equal("RECEIVED", result.TnOptionOrder.ProcessingStatus);
                Assert.NotNull(result.TnOptionOrder.TnOptionGroups);
                Assert.Equal(2, result.TnOptionOrder.TnOptionGroups.Count);

            }
        }



    }

}
