using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Bandwidth.Iris.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bandwidth.Iris.Tests.Models
{
    [TestClass]
    public class DldaTests
    {
        [TestInitialize]
        public void Setup()
        {
            Helper.SetEnvironmetVariables();
        }

        [TestMethod]
        public void GetTest()
        {
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/dldas/1", Helper.AccountId),
                ContentToSend = new  StringContent(TestXmlStrings.Dlda, Encoding.UTF8, "application/xml")
            }))
            {
                var client = Helper.CreateClient();
                var result = Dlda.Get(client, "1").Result;
                if (server.Error != null) throw server.Error;
                Assert.AreEqual("ea9e90c2-77a4-4f82-ac47-e1c5bb1311f4", result.Id);
            }
        }

        

        [TestMethod]
        public void GetWithDefaultClientTest()
        {
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/dldas/1", Helper.AccountId),
                ContentToSend = new  StringContent(TestXmlStrings.Dlda, Encoding.UTF8, "application/xml")
            }))
            {
                var result = Dlda.Get("1").Result;
                if (server.Error != null) throw server.Error;
                Assert.AreEqual("ea9e90c2-77a4-4f82-ac47-e1c5bb1311f4", result.Id);
            }
        }

        [TestMethod]
        public void ListTest()
        {
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/dldas", Helper.AccountId),
                ContentToSend = new  StringContent(TestXmlStrings.Dldas, Encoding.UTF8, "application/xml")
            }))
            {
                var client = Helper.CreateClient();
                var result = Dlda.List(client).Result;
                if (server.Error != null) throw server.Error;
                Assert.AreEqual(3, result.Length);
            }
        }
        [TestMethod]
        public void ListWithDefaultClientTest()
        {
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/dldas", Helper.AccountId),
                ContentToSend = new  StringContent(TestXmlStrings.Dldas, Encoding.UTF8, "application/xml")
            }))
            {
                var result = Dlda.List().Result;
                if (server.Error != null) throw server.Error;
                Assert.AreEqual(3, result.Length);
            }
        }

        [TestMethod]
        public void CreateTest()
        {
            var item = new Dlda
            {
                CustomerOrderId = "Your Order Id",
                DldaTnGroups = new[]{
                new DldaTnGroup{
                  TelephoneNumbers  =  new TelephoneNumbers {Numbers =  new[]{"9195551212"}},
                  SubscriberType  =  "RESIDENTIAL",
                  ListingType  =  "LISTED",
                  ListingName  =  new ListingName{
                    FirstName  =  "John",
                    LastName  =  "Smith"
                  },
                  ListAddress  =  true,
                  Address  =  new Address{
                    HouseNumber  =  "123",
                    StreetName  =  "Elm",
                    StreetSuffix  =  "Ave",
                    City  =  "Carpinteria",
                    StateCode  =  "CA",
                    Zip  =  "93013",
                    AddressType  =  "DLDA"
                  }
                }
              }
            };

            
            using (var server = new HttpServer(new[]
            {
                new RequestHandler
                {
                    EstimatedMethod = "POST",
                    EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/dldas", Helper.AccountId),
                    EstimatedContent = Helper.ToXmlString(item),
                    HeadersToSend =
                        new Dictionary<string, string>
                        {
                            {"Location", string.Format("/v1.0/accounts/{0}/dldas/1", Helper.AccountId)}
                        }
                },
                new RequestHandler
                {
                    EstimatedMethod = "GET",
                    EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/dldas/1", Helper.AccountId),
                    ContentToSend = new  StringContent(TestXmlStrings.Dlda, Encoding.UTF8, "application/xml")
                }
            }))
            {
                var client = Helper.CreateClient();
                var i = Dlda.Create(client, item).Result;
                if (server.Error != null) throw server.Error;
                Assert.AreEqual("ea9e90c2-77a4-4f82-ac47-e1c5bb1311f4", i.Id);
            }

        }

        [TestMethod]
        public void CreateWithDefaultClientTest()
        {
            var item = new Dlda
            {
            };

            using (var server = new HttpServer(new[]
            {
                new RequestHandler
                {
                    EstimatedMethod = "POST",
                    EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/dldas", Helper.AccountId),
                    EstimatedContent = Helper.ToXmlString(item),
                    HeadersToSend =
                        new Dictionary<string, string>
                        {
                            {"Location", string.Format("/v1.0/accounts/{0}/dldas/1", Helper.AccountId)}
                        }
                },
                new RequestHandler
                {
                    EstimatedMethod = "GET",
                    EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/dldas/1", Helper.AccountId),
                    ContentToSend = new  StringContent(TestXmlStrings.Dlda, Encoding.UTF8, "application/xml")
                }
            }))
            {
                var i = Dlda.Create(item).Result;
                if (server.Error != null) throw server.Error;
                Assert.AreEqual("ea9e90c2-77a4-4f82-ac47-e1c5bb1311f4", i.Id);
            }
        }

        [TestMethod]
        public void UpdateTest()
        {
            var item = new Dlda
            {
            };
            using (var server = new HttpServer(new[]
            {
                new RequestHandler
                {
                    EstimatedMethod = "PUT",
                    EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/dldas/1", Helper.AccountId),
                    ContentToSend = Helper.CreateXmlContent(item)
                }
            }))
            {
                var client = Helper.CreateClient();
                var i = new Dlda { Id = "1" };
                i.SetClient(client);
                i.Update(item).Wait();
                if (server.Error != null) throw server.Error;
            }
        }

        [TestMethod]
        public void GetHistoryTest()
        {
            using (var server = new HttpServer(new[]
            {
                new RequestHandler
                {
                    EstimatedMethod = "GET",
                    EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/dldas/1/history", Helper.AccountId),
                    ContentToSend = new StringContent(TestXmlStrings.OrderHistory, Encoding.UTF8, "application/xml")
                }
            }))
            {
                var client = Helper.CreateClient();
                var i = new Dlda {Id = "1"};
                i.SetClient(client);
                var result = i.GetHistory().Result;
                if (server.Error != null) throw server.Error;
                Assert.IsTrue(result.Length > 0);
            }
        }
    }
}
