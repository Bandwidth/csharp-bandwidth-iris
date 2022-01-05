using System;
using System.Net.Http;
using System.Text;
using Bandwidth.Iris.Model;
using Xunit;

namespace Bandwidth.Iris.Tests.Models
{
    
    public class CsrTest
    {
        [Fact]
        public void TestCreate()
        {
            var csr = new Csr
            {
                AccountId =  "accountId"
            };

            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "POST",
                EstimatedPathAndQuery = $"/v1.0/accounts/{Helper.AccountId}/csrs",
                ContentToSend = new StringContent(TestXmlStrings.csrResponse, Encoding.UTF8, "application/xml")
            }))
            {
                var client = Helper.CreateClient();
                var result = Csr.Create(client, csr).Result;
                if (server.Error != null) throw server.Error;

                Assert.AreEqual(result.CustomerOrderId, "TEST BWDB-6506");
                Assert.AreEqual(result.LastModifiedBy, "systemUser");
                Assert.AreEqual(result.OrderCreateDate, "2020-01-13T21:14:35Z");
                Assert.AreEqual(result.AccountId, "14");
                Assert.AreEqual(result.OrderId, "5c3b8240-52b5-45a5-8d7d-42a71ebcd1ba");
                Assert.AreEqual(result.LastModifiedDate, "2020-01-13T16:51:21.920Z");
                Assert.AreEqual(result.Status, "COMPLETE");
                Assert.AreEqual(result.AccountNumber, "987654321");
                Assert.AreEqual(result.AccountTelephoneNumber, "9196194444");
                Assert.AreEqual(result.EndUserName, "bandwidthGuy");
                Assert.AreEqual(result.AuthorizingUserName, "importantAuthGuy");
                Assert.AreEqual(result.CustomerCode, "123");
                Assert.AreEqual(result.EndUserPIN, "12345");
                Assert.AreEqual(result.EndUserPassword, "enduserpassword123");
                Assert.AreEqual(result.AddressLine1, "900 Main Campus Dr");
                Assert.AreEqual(result.City, "Raleigh");
                Assert.AreEqual(result.State, "NC");
                Assert.AreEqual(result.ZIPCode, "27612");
                Assert.AreEqual(result.TypeOfService, "residential");
                Assert.AreEqual(result.CsrData.AccountNumber, "123456789");
                Assert.AreEqual(result.CsrData.CustomerName, "JOHN SMITH");
                Assert.AreEqual(result.CsrData.WorkingTelephoneNumber, "9196191156");
                Assert.AreEqual(result.CsrData.WorkingTelephoneNumbersOnAccount.Length, 1);
                Assert.AreEqual(result.CsrData.WorkingTelephoneNumbersOnAccount[0], "9196191156");

                Assert.IsNotNull(result.CsrData.ServiceAddress);


            }
        }

        [Fact]
        public void TestGet()
        {
            var orderId = "123";

            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = $"/v1.0/accounts/{Helper.AccountId}/csrs/{orderId}",
                ContentToSend = new StringContent(TestXmlStrings.csrResponse, Encoding.UTF8, "application/xml")
            }))
            {
                var client = Helper.CreateClient();
                var result = Csr.Get(client, orderId).Result;
                if (server.Error != null) throw server.Error;

                Assert.AreEqual(result.CustomerOrderId, "TEST BWDB-6506");
                Assert.AreEqual(result.LastModifiedBy, "systemUser");
                Assert.AreEqual(result.OrderCreateDate, "2020-01-13T21:14:35Z");
                Assert.AreEqual(result.AccountId, "14");
                Assert.AreEqual(result.OrderId, "5c3b8240-52b5-45a5-8d7d-42a71ebcd1ba");
                Assert.AreEqual(result.LastModifiedDate, "2020-01-13T16:51:21.920Z");
                Assert.AreEqual(result.Status, "COMPLETE");
                Assert.AreEqual(result.AccountNumber, "987654321");
                Assert.AreEqual(result.AccountTelephoneNumber, "9196194444");
                Assert.AreEqual(result.EndUserName, "bandwidthGuy");
                Assert.AreEqual(result.AuthorizingUserName, "importantAuthGuy");
                Assert.AreEqual(result.CustomerCode, "123");
                Assert.AreEqual(result.EndUserPIN, "12345");
                Assert.AreEqual(result.EndUserPassword, "enduserpassword123");
                Assert.AreEqual(result.AddressLine1, "900 Main Campus Dr");
                Assert.AreEqual(result.City, "Raleigh");
                Assert.AreEqual(result.State, "NC");
                Assert.AreEqual(result.ZIPCode, "27612");
                Assert.AreEqual(result.TypeOfService, "residential");
                Assert.AreEqual(result.CsrData.AccountNumber, "123456789");
                Assert.AreEqual(result.CsrData.CustomerName, "JOHN SMITH");
                Assert.AreEqual(result.CsrData.WorkingTelephoneNumber, "9196191156");
                Assert.AreEqual(result.CsrData.WorkingTelephoneNumbersOnAccount.Length, 1);
                Assert.AreEqual(result.CsrData.WorkingTelephoneNumbersOnAccount[0], "9196191156");

                Assert.IsNotNull(result.CsrData.ServiceAddress);

            }
        }
    
        [Fact]
        public void TestReplace()
        {
            var orderId = "123";

            var csr = new Csr
            {

            };

            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "PUT",
                EstimatedPathAndQuery = $"/v1.0/accounts/{Helper.AccountId}/csrs/{orderId}",
                ContentToSend = new StringContent(TestXmlStrings.csrResponse, Encoding.UTF8, "application/xml")
            }))
            {
                var client = Helper.CreateClient();
                var result = Csr.Replace(client, orderId, csr).Result;
                if (server.Error != null) throw server.Error;

                Assert.AreEqual(result.CustomerOrderId, "TEST BWDB-6506");
                Assert.AreEqual(result.LastModifiedBy, "systemUser");
                Assert.AreEqual(result.OrderCreateDate, "2020-01-13T21:14:35Z");
                Assert.AreEqual(result.AccountId, "14");
                Assert.AreEqual(result.OrderId, "5c3b8240-52b5-45a5-8d7d-42a71ebcd1ba");
                Assert.AreEqual(result.LastModifiedDate, "2020-01-13T16:51:21.920Z");
                Assert.AreEqual(result.Status, "COMPLETE");
                Assert.AreEqual(result.AccountNumber, "987654321");
                Assert.AreEqual(result.AccountTelephoneNumber, "9196194444");
                Assert.AreEqual(result.EndUserName, "bandwidthGuy");
                Assert.AreEqual(result.AuthorizingUserName, "importantAuthGuy");
                Assert.AreEqual(result.CustomerCode, "123");
                Assert.AreEqual(result.EndUserPIN, "12345");
                Assert.AreEqual(result.EndUserPassword, "enduserpassword123");
                Assert.AreEqual(result.AddressLine1, "900 Main Campus Dr");
                Assert.AreEqual(result.City, "Raleigh");
                Assert.AreEqual(result.State, "NC");
                Assert.AreEqual(result.ZIPCode, "27612");
                Assert.AreEqual(result.TypeOfService, "residential");
                Assert.AreEqual(result.CsrData.AccountNumber, "123456789");
                Assert.AreEqual(result.CsrData.CustomerName, "JOHN SMITH");
                Assert.AreEqual(result.CsrData.WorkingTelephoneNumber, "9196191156");
                Assert.AreEqual(result.CsrData.WorkingTelephoneNumbersOnAccount.Length, 1);
                Assert.AreEqual(result.CsrData.WorkingTelephoneNumbersOnAccount[0], "9196191156");

                Assert.IsNotNull(result.CsrData.ServiceAddress);

            }
        }


        [Fact]
        public void TestListNotes()
        {
            var orderId = "123";

            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = $"/v1.0/accounts/{Helper.AccountId}/csrs/{orderId}/notes",
                ContentToSend = new StringContent(TestXmlStrings.notesResponse2, Encoding.UTF8, "application/xml")
            }))
            {
                var client = Helper.CreateClient();
                var result = Csr.ListNotes(client, orderId).Result;
                if (server.Error != null) throw server.Error;

                Assert.IsNotNull(result.List);

                Assert.AreEqual(result.List.Length, 2);

                Assert.AreEqual(result.List[0].Description, "This is a test note");
                Assert.AreEqual(result.List[0].Id, "87037");
                Assert.AreEqual(result.List[0].UserId, "jbm");
                Assert.AreEqual(result.List[0].LastDateModifier, DateTime.Parse("2014-11-16T04:01:10.000"));

            }
        }

        [Fact]
        public void TestCreateNote()
        {
            var orderId = "123";

            var note = new Note
            {
                Description = "Description goes here"
            };

            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "POST",
                EstimatedPathAndQuery = $"/v1.0/accounts/{Helper.AccountId}/csrs/{orderId}/notes",
            }))
            {
                var client = Helper.CreateClient();
                Csr.CreateNote(client, orderId, note).Wait();
                if (server.Error != null) throw server.Error;    

            }
        }

        [Fact]
        public void TestUpdateNote()
        {
            var orderId = "123";
            var noteId = "note123";

            var note = new Note
            {

            };

            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "PUT",
                EstimatedPathAndQuery = $"/v1.0/accounts/{Helper.AccountId}/csrs/{orderId}/notes/{noteId}",
            }))
            {
                var client = Helper.CreateClient();
                Csr.UpdateNote(client, orderId, noteId, note).Wait();
                if (server.Error != null) throw server.Error;

            }
        }




    }


}
