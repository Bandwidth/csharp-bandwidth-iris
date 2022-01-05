using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using Bandwidth.Iris.Model;
using Xunit;

namespace Bandwidth.Iris.Tests.Models
{

    public class ImportTnOrderTests
    {
        // [TestInitialize]
        public ImportTnOrderTests()
        {
            Helper.SetEnvironmetVariables();
        }

        [Fact]
        public void TestImportTnOrderCreate()
        {
            var order = new ImportTnOrder
            {
                AccountId = "account",
                SipPeerId = 1,
                SiteId = 2
            };

            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "POST",
                EstimatedPathAndQuery = $"/v1.0/accounts/{Helper.AccountId}/importTnOrders",
                ContentToSend = new StringContent(TestXmlStrings.ImportTnOrderResponse, Encoding.UTF8, "application/xml")
            }))
            {
                var client = Helper.CreateClient();
                var result = ImportTnOrder.Create(client, order).Result;
                if (server.Error != null) throw server.Error;

                Assert.Equal(result.ImportTnOrder.CustomerOrderId, "SJM000001");
                Assert.Equal(result.ImportTnOrder.OrderCreateDate, "2018-01-20T02:59:54.000Z");
                Assert.Equal(result.ImportTnOrder.AccountId, "9900012");
                Assert.Equal(result.ImportTnOrder.CreatedByUser, "smckinnon");
                Assert.Equal(result.ImportTnOrder.OrderId, "b05de7e6-0cab-4c83-81bb-9379cba8efd0");
                Assert.Equal(result.ImportTnOrder.LastModifiedDate, "2018-01-20T02:59:54.000Z");
                Assert.Equal(result.ImportTnOrder.SiteId, 202);
                Assert.Equal(result.ImportTnOrder.SipPeerId, 520565);
                Assert.Equal(result.ImportTnOrder.ProcessingStatus, "PROCESSING");

                Assert.NotNull(result.ImportTnOrder.Subscriber);

                Assert.Equal(result.ImportTnOrder.TelephoneNumbers.Length, 4);

            }
        }

        [Fact]
        public void TestImportTnOrderGet()
        {
            var order = new ImportTnOrder
            {
                OrderId = "1",
                AccountId = "account",
                SipPeerId = 1,
                SiteId = 2
            };

            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = $"/v1.0/accounts/{Helper.AccountId}/importTnOrders/{order.OrderId}",
                ContentToSend = new StringContent(TestXmlStrings.ImportTnOrder, Encoding.UTF8, "application/xml")
            }))
            {
                var client = Helper.CreateClient();
                var result = ImportTnOrder.Get(client, order.OrderId).Result;
                if (server.Error != null) throw server.Error;

                Assert.Equal(result.CustomerOrderId, "SJM000001");
                Assert.Equal(result.OrderCreateDate, "2018-01-20T02:59:54.000Z");
                Assert.Equal(result.AccountId, "9900012");
                Assert.Equal(result.CreatedByUser, "smckinnon");
                Assert.Equal(result.OrderId, "b05de7e6-0cab-4c83-81bb-9379cba8efd0");
                Assert.Equal(result.LastModifiedDate, "2018-01-20T02:59:54.000Z");
                Assert.Equal(result.SiteId, 202);
                Assert.Equal(result.SipPeerId, 520565);
                Assert.Equal(result.ProcessingStatus, "PROCESSING");

                Assert.NotNull(result.Subscriber);

                Assert.Equal(result.TelephoneNumbers.Length, 4);

            }
        }

        [Fact]
        public void TestImportTnOrderList()
        {
            var order = new ImportTnOrder
            {
                OrderId = "fbd17609-be44-48e7-a301-90bd6cf42248",
                AccountId = "account",
                SipPeerId = 1,
                SiteId = 2
            };

            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = $"/v1.0/accounts/{Helper.AccountId}/importTnOrders?accountId=1",
                ContentToSend = new StringContent(TestXmlStrings.ImportTnOrders, Encoding.UTF8, "application/xml")
            }))
            {
                var client = Helper.CreateClient();
                var result = ImportTnOrder.List(client, new Dictionary<string, object> { { "accountId", "1" } }).Result;
                if (server.Error != null) throw server.Error;

                Assert.Equal(result.TotalCount, 14);
                Assert.Equal(result.ImportTnOrderSummarys.Length, 14);
                Assert.Equal(result.ImportTnOrderSummarys[0].accountId, 9900778);
                Assert.Equal(result.ImportTnOrderSummarys[0].CountOfTNs, 1);
                Assert.Equal(result.ImportTnOrderSummarys[0].CustomerOrderId, "id");
                Assert.Equal(result.ImportTnOrderSummarys[0].userId, "jmulford-api");
                Assert.Equal(result.ImportTnOrderSummarys[0].lastModifiedDate, "2020-02-04T14:09:08.937Z");
                Assert.Equal(result.ImportTnOrderSummarys[0].OrderDate, "2020-02-04T14:09:07.824Z");
                Assert.Equal(result.ImportTnOrderSummarys[0].OrderType, "import_tn_orders");
                Assert.Equal(result.ImportTnOrderSummarys[0].OrderStatus, "FAILED");
                Assert.Equal(result.ImportTnOrderSummarys[0].OrderId, "fbd17609-be44-48e7-a301-90bd6cf42248");

            }
        }

        [Fact]
        public void TestImportTnOrderHistory()
        {
            var order = new ImportTnOrder
            {
                OrderId = "fbd17609-be44-48e7-a301-90bd6cf42248",
                AccountId = "account",
                SipPeerId = 1,
                SiteId = 2
            };

            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = $"/v1.0/accounts/{Helper.AccountId}/importTnOrders/{order.OrderId}/history",
                ContentToSend = new StringContent(TestXmlStrings.OrderHistoryWrapper, Encoding.UTF8, "application/xml")
            }))
            {
                var client = Helper.CreateClient();
                var result = ImportTnOrder.GetHistory(client, order.OrderId).Result;
                if (server.Error != null) throw server.Error;

                Assert.Equal(result.Items.Length, 2);
                Assert.Equal(result.Items[0].OrderDate, DateTime.Parse("2020-02-04T14:09:07.824"));
                Assert.Equal(result.Items[0].Note, "Import TN order has been received by the system.");
                Assert.Equal(result.Items[0].Author, "jmulford-api");
                Assert.Equal(result.Items[0].Status, "received");

            }
        }

        [Fact]
        public void TestImportTnOrderListLoasFiles()
        {
            var order = new ImportTnOrder
            {
                OrderId = "fbd17609-be44-48e7-a301-90bd6cf42248",
                AccountId = "account",
                SipPeerId = 1,
                SiteId = 2
            };

            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = $"/v1.0/accounts/{Helper.AccountId}/importTnOrders/{order.OrderId}/loas",
                ContentToSend = new StringContent(TestXmlStrings.loasFileListResponse, Encoding.UTF8, "application/xml")
            }))
            {
                var client = Helper.CreateClient();
                var result = ImportTnOrder.ListLoasFiles(client, order.OrderId).Result;
                if (server.Error != null) throw server.Error;

                Assert.Equal(result.FileCount, 2);
                Assert.Equal(result.FileNames.Length, 2);
                Assert.Equal(result.FileNames[0], "803f3cc5-beae-469e-bd65-e9891ccdffb9-1092874634747.pdf");
                Assert.Equal(result.ResultCode, "0");
                Assert.Equal(result.ResultMessage, "LOA file list successfully returned");


            }
        }

        [Fact]
        public void TestImportTnOrderUploadLoasFile()
        {
            var order = new ImportTnOrder
            {
                OrderId = "fbd17609-be44-48e7-a301-90bd6cf42248",
                AccountId = "account",
                SipPeerId = 1,
                SiteId = 2
            };

            byte[] byteArray = Encoding.ASCII.GetBytes("Test string to bytes");

            MemoryStream stream = new MemoryStream(byteArray);

            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "POST",
                EstimatedPathAndQuery = $"/v1.0/accounts/{Helper.AccountId}/importTnOrders/{order.OrderId}/loas",
                ContentToSend = new StringContent(TestXmlStrings.loasFileUploadResponse, Encoding.UTF8, "application/xml")
            }))
            {
                var client = Helper.CreateClient();
                var result = ImportTnOrder.UploadLoasFile(client, order.OrderId, stream, "application/*").Result;
                if (server.Error != null) throw server.Error;

                Assert.Equal("OK", result.StatusCode.ToString());

            }
        }

        [Fact]
        public void TestImportTnOrderGetLoasFile()
        {
            var order = new ImportTnOrder
            {
                OrderId = "fbd17609-be44-48e7-a301-90bd6cf42248",
                AccountId = "account",
                SipPeerId = 1,
                SiteId = 2
            };

            var fileId = "fileId";

            byte[] byteArray = Encoding.ASCII.GetBytes("Test string to bytes");

            MemoryStream stream = new MemoryStream(byteArray);

            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = $"/v1.0/accounts/{Helper.AccountId}/importTnOrders/{order.OrderId}/loas/{fileId}",
                ContentToSend = new StreamContent(stream)
            }))
            {
                var client = Helper.CreateClient();
                var result = ImportTnOrder.GetLoasFile(client, order.OrderId, fileId).Result;
                if (server.Error != null) throw server.Error;

                StreamReader reader = new StreamReader(stream);
                var expected = reader.ReadToEnd();

                StreamReader reader2 = new StreamReader(result);
                var actual = reader2.ReadToEnd();

                Assert.Equal(expected, actual);

            }
        }

        [Fact]
        public void TestImportTnOrderReplaceLoasFile()
        {
            var order = new ImportTnOrder
            {
                OrderId = "fbd17609-be44-48e7-a301-90bd6cf42248",
                AccountId = "account",
                SipPeerId = 1,
                SiteId = 2
            };

            var fileId = "fileId";

            byte[] byteArray = Encoding.ASCII.GetBytes("Test string to bytes");

            MemoryStream stream = new MemoryStream(byteArray);

            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "PUT",
                EstimatedPathAndQuery = $"/v1.0/accounts/{Helper.AccountId}/importTnOrders/{order.OrderId}/loas/{fileId}",
                ContentToSend = new StringContent(TestXmlStrings.loasFileUploadResponse, Encoding.UTF8, "application/xml")
            }))
            {
                var client = Helper.CreateClient();
                var result = ImportTnOrder.ReplaceLoasFile(client, order.OrderId, fileId, stream).Result;
                if (server.Error != null) throw server.Error;

                Assert.Equal("63097af1-37ae-432f-8a0d-9b0e6517a35b-1429550165581.pdf", result.Filename);
                Assert.Equal("LOA file uploaded successfully for order 63097af1-37ae-432f-8a0d-9b0e6517a35b", result.ResultMessage);
                Assert.Equal(0, result.ResultCode);

            }
        }

        [Fact]
        public void TestImportTnOrderDeleteLoasFile()
        {
            var order = new ImportTnOrder
            {
                OrderId = "fbd17609-be44-48e7-a301-90bd6cf42248",
                AccountId = "account",
                SipPeerId = 1,
                SiteId = 2
            };

            var fileId = "fileId";

            byte[] byteArray = Encoding.ASCII.GetBytes("Test string to bytes");

            MemoryStream stream = new MemoryStream(byteArray);

            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "DELETE",
                EstimatedPathAndQuery = $"/v1.0/accounts/{Helper.AccountId}/importTnOrders/{order.OrderId}/loas/{fileId}",
            }))
            {
                var client = Helper.CreateClient();
                ImportTnOrder.DeleteLoasFile(client, order.OrderId, fileId).Wait();
                if (server.Error != null) throw server.Error;
            }
        }

        [Fact]
        public void TestImportTnOrderGetLoasFileMetadata()
        {
            var order = new ImportTnOrder
            {
                OrderId = "fbd17609-be44-48e7-a301-90bd6cf42248",
                AccountId = "account",
                SipPeerId = 1,
                SiteId = 2
            };

            var fileId = "fileId";

            byte[] byteArray = Encoding.ASCII.GetBytes("Test string to bytes");

            MemoryStream stream = new MemoryStream(byteArray);

            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = $"/v1.0/accounts/{Helper.AccountId}/importTnOrders/{order.OrderId}/loas/{fileId}/metadata",
                ContentToSend = new StringContent(TestXmlStrings.FileMetadataResponse, Encoding.UTF8, "application/xml")
            }))
            {
                var client = Helper.CreateClient();
                var result = ImportTnOrder.GetLoasFileMetadata(client, order.OrderId, fileId).Result;
                if (server.Error != null) throw server.Error;

                Assert.Equal("LOA", result.DocumentType);

            }
        }

        [Fact]
        public void TestImportTnOrderReplaceLoasFileMetadata()
        {
            var order = new ImportTnOrder
            {
                OrderId = "fbd17609-be44-48e7-a301-90bd6cf42248",
                AccountId = "account",
                SipPeerId = 1,
                SiteId = 2
            };

            var fileId = "fileId";

            var metadata = new FileMetadata
            {
                DocumentType = "LOA",
                DocumentName = "name.pdf"
            };

            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "PUT",
                EstimatedPathAndQuery = $"/v1.0/accounts/{Helper.AccountId}/importTnOrders/{order.OrderId}/loas/{fileId}/metadata",
            }))
            {
                var client = Helper.CreateClient();
                ImportTnOrder.ReplaceLoasFileMetadata(client, order.OrderId, fileId, metadata).Wait();
                if (server.Error != null) throw server.Error;


            }
        }

        [Fact]
        public void TestImportTnOrderDeleteLoasFileMetadata()
        {
            var order = new ImportTnOrder
            {
                OrderId = "fbd17609-be44-48e7-a301-90bd6cf42248",
                AccountId = "account",
                SipPeerId = 1,
                SiteId = 2
            };

            var fileId = "fileId";


            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "DELETE",
                EstimatedPathAndQuery = $"/v1.0/accounts/{Helper.AccountId}/importTnOrders/{order.OrderId}/loas/{fileId}/metadata",
            }))
            {
                var client = Helper.CreateClient();
                ImportTnOrder.DeleteLoasFileMetadata(client, order.OrderId, fileId).Wait();
                if (server.Error != null) throw server.Error;


            }
        }
    }
}
