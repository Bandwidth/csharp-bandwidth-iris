using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Bandwidth.Iris.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bandwidth.Iris.Tests.Models
{
    [TestClass]
    public class TnReservationTests
    {
        [TestInitialize]
        public void Setup()
        {
            Helper.SetEnvironmetVariables();
        }

        [TestMethod]
        public void GetTest()
        {
            var item = new TnReservation
            {
                Id = "1",
                AccountId = "111",
                ReservedTn = new[]{"000"},
                ReservationExpires = 0
            };
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/tnreservation/1", Helper.AccountId),
                ContentToSend = Helper.CreateXmlContent(new ReservationResponse { Reservation = item })
            }))
            {
                var client = Helper.CreateClient();
                var result = TnReservation.Get(client, "1").Result;
                if (server.Error != null) throw server.Error;
                Helper.AssertObjects(item, result);
            }
        }

        [TestMethod]
        public void GetWithXmlTest()
        {
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/tnreservation/1", Helper.AccountId),
                ContentToSend = new StringContent(TestXmlStrings.ValidReservationResponseXml, Encoding.UTF8, "application/xml")
            }))
            {
                var client = Helper.CreateClient();
                var result = TnReservation.Get(client, "1").Result;
                if (server.Error != null) throw server.Error;
                Assert.AreEqual("1", result.Id);
                Assert.AreEqual("accountId", result.AccountId);
                Assert.AreEqual(30, result.ReservationExpires);
                Assert.AreEqual("9195551212", result.ReservedTn[0]);
            }
        }

        [TestMethod]
        public void GetWithErrorXmlTest()
        {
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/tnreservation/1", Helper.AccountId),
                ContentToSend = new StringContent(TestXmlStrings.InvalidReservationResponseXml, Encoding.UTF8, "application/xml")
            }))
            {
                var client = Helper.CreateClient();
                try
                {
                    TnReservation.Get(client, "1").Wait();
                    if (server.Error != null) throw server.Error;
                }
                catch (AggregateException exc)
                {
                    var ex = exc.InnerExceptions[0] as BandwidthIrisException;
                    if (ex != null)
                    {
                        Assert.AreEqual("Reservation failed: telephone number 9195551212 is not available.", ex.Message);
                        Assert.AreEqual("5041", ex.Code);
                        return;
                    }
                }
                Assert.Fail("should throw an error");
            }
        }

        [TestMethod]
        public void GetWithDefaultClientTest()
        {
            var item = new TnReservation
            {
                Id = "1",
                AccountId = "111",
                ReservedTn = new []{"000"},
                ReservationExpires = 0
            };
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/tnreservation/1", Helper.AccountId),
                ContentToSend = Helper.CreateXmlContent(new ReservationResponse { Reservation = item })
            }))
            {
                var result = TnReservation.Get("1").Result;
                if (server.Error != null) throw server.Error;
                Helper.AssertObjects(item, result);
            }
        }

        [TestMethod]
        public void CreateTest()
        {
            var item = new TnReservation
            {
                AccountId = "111",
                ReservedTn = new[]{"000"},
                ReservationExpires = 0
            };

            using (var server = new HttpServer(new[]
            {
                new RequestHandler
                {
                    EstimatedMethod = "POST",
                    EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/tnreservation", Helper.AccountId),
                    EstimatedContent = Helper.ToXmlString(item),
                    HeadersToSend =
                        new Dictionary<string, string>
                        {
                            {"Location", string.Format("/v1.0/accounts/{0}/tnreservation/1", Helper.AccountId)}
                        }
                },
                new RequestHandler
                {
                    EstimatedMethod = "GET",
                    EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/tnreservation/1", Helper.AccountId),
                    ContentToSend = Helper.CreateXmlContent(new ReservationResponse{Reservation = new TnReservation {Id = "1"}})
                }
            }))
            {
                var client = Helper.CreateClient();
                var i = TnReservation.Create(client, item).Result;
                if (server.Error != null) throw server.Error;
                Assert.AreEqual("1", i.Id);
            }
        }

        [TestMethod]
        public void CreateWithDefaultClientTest()
        {
            var item = new TnReservation
            {
                AccountId = "111",
                ReservedTn = new []{"000"},
                ReservationExpires = 0
            };


            using (var server = new HttpServer(new[]
            {
                new RequestHandler
                {
                    EstimatedMethod = "POST",
                    EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/tnreservation", Helper.AccountId),
                    EstimatedContent = Helper.ToXmlString(item),
                    HeadersToSend =
                        new Dictionary<string, string>
                        {
                            {"Location", string.Format("/v1.0/accounts/{0}/tnreservation/1", Helper.AccountId)}
                        }
                },
                new RequestHandler
                {
                    EstimatedMethod = "GET",
                    EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/tnreservation/1", Helper.AccountId),
                    ContentToSend = Helper.CreateXmlContent(new ReservationResponse{Reservation = new TnReservation {Id = "1"}})
                }
            }))
            {
                var i = TnReservation.Create(item).Result;
                if (server.Error != null) throw server.Error;
                Assert.AreEqual("1", i.Id);
            }
        }

        

        [TestMethod]
        public void DeleteTest()
        {
            using (var server = new HttpServer(new[]
            {
                new RequestHandler
                {
                    EstimatedMethod = "DELETE",
                    EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/tnreservation/1", Helper.AccountId),
                }
            }))
            {
                var client = Helper.CreateClient();
                var i = new TnReservation { Id = "1" };
                i.SetClient(client);
                i.Delete().Wait();
                if (server.Error != null) throw server.Error;
            }
        }
        
    }

}
