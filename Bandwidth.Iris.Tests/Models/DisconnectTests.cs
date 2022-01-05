using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Bandwidth.Iris.Model;
using Xunit;

namespace Bandwidth.Iris.Tests.Models
{
    
    public class DisconnectTests
    {
        // [TestInitialize]
        public void Setup()
        {
            Helper.SetEnvironmetVariables();
        }

        [Fact]
        public void DisconnectNumbersTest()
        {
            var data = new DisconnectTelephoneNumberOrder
            {
                Name = "order",
                DisconnectTelephoneNumberOrderType = new DisconnectTelephoneNumberOrderType
                {
                    TelephoneNumberList = new TelephoneNumberList
                    {
                        TelephoneNumbers = new[] {"111", "222" }
                    }
                }
            };
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "POST",
                EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/disconnects", Helper.AccountId),
                EstimatedContent = Helper.ToXmlString(data)
            }))
            {
                var client = Helper.CreateClient();
                Disconnect.Create(client, "order", "111", "222").Wait();
                if (server.Error != null) throw server.Error;
            }
        }

        public void DisconnectNumbersWithDefaultClientTest()
        {
            var data = new DisconnectTelephoneNumberOrder
            {
                Name = "order",
                DisconnectTelephoneNumberOrderType = new DisconnectTelephoneNumberOrderType
                {
                    TelephoneNumberList = new TelephoneNumberList
                    {
                        TelephoneNumbers = new[] { "111", "222" }
                    }
                }
            };
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "POST",
                EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/disconnects", Helper.AccountId),
                EstimatedContent = Helper.ToXmlString(data)
            }))
            {
                Disconnect.Create("order", "111", "222").Wait();
                if (server.Error != null) throw server.Error;
            }
        }

        [Fact]
        public void GetNotesTest()
        {
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/disconnects/1/notes", Helper.AccountId),
                ContentToSend = new StringContent(TestXmlStrings.NotesResponse, Encoding.UTF8, "application/xml")
            }))
            {
                var client = Helper.CreateClient();
                var disconnect = new Disconnect();
                disconnect.SetClient(client);
                var list = disconnect.GetNotes("1").Result;
                if (server.Error != null) throw server.Error;
                Assert.AreEqual(2, list.Length);
                Assert.AreEqual("11299", list[0].Id);
                Assert.AreEqual("customer", list[0].UserId);
                Assert.AreEqual("Test", list[0].Description);
                Assert.AreEqual("11301", list[1].Id);
                Assert.AreEqual("customer", list[1].UserId);
                Assert.AreEqual("Test1", list[1].Description);
            }
        }

        [Fact]
        public void AddNoteTest()
        {
            var item = new Note
            {
                UserId = "customer",
                Description = "Test"
            };
            using (var server = new HttpServer(new[]{
                new RequestHandler
                {
                    EstimatedMethod = "POST",
                    EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/disconnects/1/notes", Helper.AccountId),
                    EstimatedContent = Helper.ToXmlString(item),
                    HeadersToSend = new Dictionary<string, string> {
                        {"Location", string.Format("/v1.0/accounts/{0}/portins/1/disconnects/11299", Helper.AccountId)} 
                    }
                },
                new RequestHandler
                {
                    EstimatedMethod = "GET",
                    EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/disconnects/1/notes", Helper.AccountId),
                    ContentToSend = new StringContent(TestXmlStrings.NotesResponse, Encoding.UTF8, "application/xml")
                }
            }))
            {
                var client = Helper.CreateClient();
                var disconnect = new Disconnect();
                disconnect.SetClient(client);
                var r = disconnect.AddNote("1", item).Result;
                if (server.Error != null) throw server.Error;
                Assert.AreEqual("11299", r.Id);
                Assert.AreEqual("customer", r.UserId);
                Assert.AreEqual("Test", r.Description);
            }
        }
    }
}
