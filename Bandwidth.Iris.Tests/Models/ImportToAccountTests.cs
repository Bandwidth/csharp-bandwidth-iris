using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Bandwidth.Iris.Model;
using Xunit;

namespace Bandwidth.Iris.Tests.Models
{
    
    public class ImportToAccountTests
    {
        // [TestInitialize]
        public void Setup()
        {
            Helper.SetEnvironmetVariables();
        }

        [Fact]
        public void GetNotesTest()
        {
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/importToAccounts/1/notes", Helper.AccountId),
                ContentToSend = new StringContent(TestXmlStrings.NotesResponse, Encoding.UTF8, "application/xml")
            }))
            {
                var client = Helper.CreateClient();
                var list = ImportToAccount.GetNotes(client, "1").Result;
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
        public void GetNotesWithDefaultClientTest()
        {
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/importToAccounts/1/notes", Helper.AccountId),
                ContentToSend = new StringContent(TestXmlStrings.NotesResponse, Encoding.UTF8, "application/xml")
            }))
            {
                var list = ImportToAccount.GetNotes("1").Result;
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
                    EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/importToAccounts/1/notes", Helper.AccountId),
                    EstimatedContent = Helper.ToXmlString(item),
                    HeadersToSend = new Dictionary<string, string> {
                        {"Location", string.Format("/v1.0/accounts/{0}/portins/1/importToAccounts/11299", Helper.AccountId)} 
                    }
                },
                new RequestHandler
                {
                    EstimatedMethod = "GET",
                    EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/importToAccounts/1/notes", Helper.AccountId),
                    ContentToSend = new StringContent(TestXmlStrings.NotesResponse, Encoding.UTF8, "application/xml")
                }
            }))
            {
                var client = Helper.CreateClient();
                var r = ImportToAccount.AddNote(client, "1", item).Result;
                if (server.Error != null) throw server.Error;
                Assert.AreEqual("11299", r.Id);
                Assert.AreEqual("customer", r.UserId);
                Assert.AreEqual("Test", r.Description);
            }
        }
        [Fact]
        public void AddNoteWithDefaultClientTest()
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
                    EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/importToAccounts/1/notes", Helper.AccountId),
                    EstimatedContent = Helper.ToXmlString(item),
                    HeadersToSend = new Dictionary<string, string> {
                        {"Location", string.Format("/v1.0/accounts/{0}/portins/1/importToAccounts/11299", Helper.AccountId)} 
                    }
                },
                new RequestHandler
                {
                    EstimatedMethod = "GET",
                    EstimatedPathAndQuery = string.Format("/v1.0/accounts/{0}/importToAccounts/1/notes", Helper.AccountId),
                    ContentToSend = new StringContent(TestXmlStrings.NotesResponse, Encoding.UTF8, "application/xml")
                }
            }))
            {
                var r = ImportToAccount.AddNote("1", item).Result;
                if (server.Error != null) throw server.Error;
                Assert.AreEqual("11299", r.Id);
                Assert.AreEqual("customer", r.UserId);
                Assert.AreEqual("Test", r.Description);
            }
        }

    }
}
