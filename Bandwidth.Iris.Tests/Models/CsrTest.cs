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
                AccountId = "accountId"
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

                Assert.Equal("TEST BWDB-6506", result.CustomerOrderId);
                Assert.Equal("systemUser", result.LastModifiedBy);
                Assert.Equal("2020-01-13T21:14:35Z", result.OrderCreateDate);
                Assert.Equal("14", result.AccountId);
                Assert.Equal("5c3b8240-52b5-45a5-8d7d-42a71ebcd1ba", result.OrderId);
                Assert.Equal("2020-01-13T16:51:21.920Z", result.LastModifiedDate);
                Assert.Equal("COMPLETE", result.Status);
                Assert.Equal("987654321", result.AccountNumber);
                Assert.Equal("9196194444", result.AccountTelephoneNumber);
                Assert.Equal("bandwidthGuy", result.EndUserName);
                Assert.Equal("importantAuthGuy", result.AuthorizingUserName);
                Assert.Equal("123", result.CustomerCode);
                Assert.Equal("12345", result.EndUserPIN);
                Assert.Equal("enduserpassword123", result.EndUserPassword);
                Assert.Equal("900 Main Campus Dr", result.AddressLine1);
                Assert.Equal("Raleigh", result.City);
                Assert.Equal("NC", result.State);
                Assert.Equal("27612", result.ZIPCode);
                Assert.Equal("residential", result.TypeOfService);
                Assert.Equal("123456789", result.CsrData.AccountNumber);
                Assert.Equal("JOHN SMITH", result.CsrData.CustomerName);
                Assert.Equal("9196191156", result.CsrData.WorkingTelephoneNumber);
                Assert.Single(result.CsrData.WorkingTelephoneNumbersOnAccount);
                Assert.Equal("9196191156", result.CsrData.WorkingTelephoneNumbersOnAccount[0]);

                Assert.NotNull(result.CsrData.ServiceAddress);


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

                Assert.Equal("TEST BWDB-6506", result.CustomerOrderId);
                Assert.Equal("systemUser", result.LastModifiedBy);
                Assert.Equal("2020-01-13T21:14:35Z", result.OrderCreateDate);
                Assert.Equal("14", result.AccountId);
                Assert.Equal("5c3b8240-52b5-45a5-8d7d-42a71ebcd1ba", result.OrderId);
                Assert.Equal("2020-01-13T16:51:21.920Z", result.LastModifiedDate);
                Assert.Equal("COMPLETE", result.Status);
                Assert.Equal("987654321", result.AccountNumber);
                Assert.Equal("9196194444", result.AccountTelephoneNumber);
                Assert.Equal("bandwidthGuy", result.EndUserName);
                Assert.Equal("importantAuthGuy", result.AuthorizingUserName);
                Assert.Equal("123", result.CustomerCode);
                Assert.Equal("12345", result.EndUserPIN);
                Assert.Equal("enduserpassword123", result.EndUserPassword);
                Assert.Equal("900 Main Campus Dr", result.AddressLine1);
                Assert.Equal("Raleigh", result.City);
                Assert.Equal("NC", result.State);
                Assert.Equal("27612", result.ZIPCode);
                Assert.Equal("residential", result.TypeOfService);
                Assert.Equal("123456789", result.CsrData.AccountNumber);
                Assert.Equal("JOHN SMITH", result.CsrData.CustomerName);
                Assert.Equal("9196191156", result.CsrData.WorkingTelephoneNumber);
                Assert.Single(result.CsrData.WorkingTelephoneNumbersOnAccount);
                Assert.Equal("9196191156", result.CsrData.WorkingTelephoneNumbersOnAccount[0]);

                Assert.NotNull(result.CsrData.ServiceAddress);

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

                Assert.Equal("TEST BWDB-6506", result.CustomerOrderId);
                Assert.Equal("systemUser", result.LastModifiedBy);
                Assert.Equal("2020-01-13T21:14:35Z", result.OrderCreateDate);
                Assert.Equal("14", result.AccountId);
                Assert.Equal("5c3b8240-52b5-45a5-8d7d-42a71ebcd1ba", result.OrderId);
                Assert.Equal("2020-01-13T16:51:21.920Z", result.LastModifiedDate);
                Assert.Equal("COMPLETE", result.Status);
                Assert.Equal("987654321", result.AccountNumber);
                Assert.Equal("9196194444", result.AccountTelephoneNumber);
                Assert.Equal("bandwidthGuy", result.EndUserName);
                Assert.Equal("importantAuthGuy", result.AuthorizingUserName);
                Assert.Equal("123", result.CustomerCode);
                Assert.Equal("12345", result.EndUserPIN);
                Assert.Equal("enduserpassword123", result.EndUserPassword);
                Assert.Equal("900 Main Campus Dr", result.AddressLine1);
                Assert.Equal("Raleigh", result.City);
                Assert.Equal("NC", result.State);
                Assert.Equal("27612", result.ZIPCode);
                Assert.Equal("residential", result.TypeOfService);
                Assert.Equal("123456789", result.CsrData.AccountNumber);
                Assert.Equal("JOHN SMITH", result.CsrData.CustomerName);
                Assert.Equal("9196191156", result.CsrData.WorkingTelephoneNumber);
                Assert.Single(result.CsrData.WorkingTelephoneNumbersOnAccount);
                Assert.Equal("9196191156", result.CsrData.WorkingTelephoneNumbersOnAccount[0]);

                Assert.NotNull(result.CsrData.ServiceAddress);

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

                Assert.NotNull(result.List);

                Assert.Equal(2, result.List.Length);

                Assert.Equal("This is a test note", result.List[0].Description);
                Assert.Equal("87037", result.List[0].Id);
                Assert.Equal("jbm", result.List[0].UserId);
                Assert.Equal(result.List[0].LastDateModifier, DateTime.Parse("2014-11-16T04:01:10.000"));

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
