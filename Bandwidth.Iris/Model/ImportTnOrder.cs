using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Bandwidth.Iris.Model
{
    public class ImportTnOrder
    {

        public string CustomerOrderId { get; set; }
        public string OrderCreateDate { get; set; }
        public string AccountId { get; set; }
        public string CreatedByUser { get; set; }
        public string OrderId { get; set; }
        public string LastModifiedDate { get; set; }
        public int SiteId { get; set; }
        public int SipPeerId { get; set; }
        public Subscriber Subscriber { get; set; }
        public TelephoneNumber[] TelephoneNumbers { get; set; }
        public string LoaAuthorizingPerson { get; set; }
        public string ProcessingStatus { get; set; }

        static private string importTnOrdersPath = "importTnOrders";

        public async static Task<ImportTnOrderResponse> Create(Client client, ImportTnOrder order)
        {
            var item = await client.MakePostRequest<ImportTnOrderResponse>(client.ConcatAccountPath(importTnOrdersPath), order);
            return item;
        }

        public static Task<ImportTnOrderResponse> Create(ImportTnOrder order)
        {
            return Create(Client.GetInstance(), order);
        }

        public async static Task<ImportTnOrders> List(Client client, Dictionary<string, object> query)
        {
            var item = await client.MakeGetRequest<ImportTnOrders>(client.ConcatAccountPath(importTnOrdersPath), query);
            return item;
        }

        public static Task<ImportTnOrders> List(Dictionary<string, object> query)
        {
            return List(Client.GetInstance(), query);
        }

        public async static Task<ImportTnOrder> Get(Client client, string orderId)
        {
            var item = await client.MakeGetRequest<ImportTnOrder>(client.ConcatAccountPath(($"{importTnOrdersPath}/{orderId}")));
            return item;
        }

        public static Task<ImportTnOrder> Get(string orderId)
        {
            return Get(Client.GetInstance(), orderId);
        }

        public async static Task<OrderHistoryWrapper> GetHistory(Client client, string orderId)
        {
            var item = await client.MakeGetRequest<OrderHistoryWrapper>(client.ConcatAccountPath($"{importTnOrdersPath}/{orderId}/history"));
            return item;
        }

        public static Task<OrderHistoryWrapper> GetHistory(string orderId)
        {
            return GetHistory(Client.GetInstance(), orderId);
        }

        public async static Task<FileListResponse> ListLoasFiles(Client client, string orderId)
        {
            var item = await client.MakeGetRequest<FileListResponse>(client.ConcatAccountPath($"{importTnOrdersPath}/{orderId}/loas"));
            return item;
        }

        public static Task<FileListResponse> ListLoasFiles(string orderId)
        {
            return ListLoasFiles(Client.GetInstance(), orderId);
        }

        public async static Task<HttpResponseMessage> UploadLoasFile(Client client, string orderId, Stream stream, string contentType)
        {
            var item = await client.SendData(client.ConcatAccountPath($"{importTnOrdersPath}/{orderId}/loas"), stream, contentType);
            return item;
        }

        public static Task<HttpResponseMessage> UploadLoasFile(string orderId, Stream stream, string contentType)
        {
            return UploadLoasFile(Client.GetInstance(), orderId, stream, contentType);
        }

        public async static Task<Stream> GetLoasFile(Client client, string orderId, string fileId)
        {
            var item = await client.MakeGetRequest<Stream>(client.ConcatAccountPath($"{importTnOrdersPath}/{orderId}/loas/{fileId}"));
            return item;
        }

        public static Task<Stream> GetLoasFile(string orderId, string fileId)
        {
            return GetLoasFile(Client.GetInstance(), orderId, fileId);
        }

        public async static Task<fileUploadResponse> ReplaceLoasFile(Client client, string orderId, string fileId, Stream stream)
        {
            var item = await client.MakePutRequest<fileUploadResponse>(client.ConcatAccountPath($"{importTnOrdersPath}/{orderId}/loas/{fileId}"), stream);
            return item;
        }

        public static Task<fileUploadResponse> ReplaceLoasFile(string orderId, string fileId, Stream stream)
        {
            return ReplaceLoasFile(Client.GetInstance(), orderId, fileId, stream);
        }

        public async static Task DeleteLoasFile(Client client, string orderId, string fileId)
        {
            await client.MakeDeleteRequest(client.ConcatAccountPath($"{importTnOrdersPath}/{orderId}/loas/{fileId}"));
        }

        public static Task DeleteLoasFile(string orderId, string fileId)
        {
            return DeleteLoasFile(Client.GetInstance(), orderId, fileId);
        }

        public async static Task<FileMetadata> GetLoasFileMetadata(Client client, string orderId, string fileId)
        {
            var item = await client.MakeGetRequest<FileMetadata>(client.ConcatAccountPath($"{importTnOrdersPath}/{orderId}/loas/{fileId}/metadata"));
            return item;
        }

        public static Task<FileMetadata> GetLoasFileMetadata(string orderId, string fileId)
        {
            return GetLoasFileMetadata(Client.GetInstance(), orderId, fileId);
        }

        public async static Task ReplaceLoasFileMetadata(Client client, string orderId, string fileId, FileMetadata metadata)
        {
            await client.MakePutRequest(client.ConcatAccountPath($"{importTnOrdersPath}/{orderId}/loas/{fileId}/metadata"), metadata);
        }

        public static Task ReplaceLoasFileMetadata(string orderId, string fileId, FileMetadata metadata)
        {
            return ReplaceLoasFileMetadata(Client.GetInstance(), orderId, fileId, metadata);
        }

        public async static Task DeleteLoasFileMetadata(Client client, string orderId, string fileId)
        {
            await client.MakeDeleteRequest(client.ConcatAccountPath($"{importTnOrdersPath}/{orderId}/loas/{fileId}/metadata"));
        }

        public static Task DeleteLoasFileMetadata(string orderId, string fileId)
        {
            return DeleteLoasFileMetadata(Client.GetInstance(), orderId, fileId);
        }

    }

    public class fileUploadResponse
    {
        [XmlElement("filename")]
        public string Filename { get; set; }
        [XmlElement("resultCode")]
        public int ResultCode { get; set; }
        [XmlElement("resultMessage")]
        public string ResultMessage { get; set; }
    }

    public class ImportTnOrderResponse
    {
        public ImportTnOrder ImportTnOrder { get; set; }
    }

    public class ImportTnOrders
    {
        public int TotalCount { get; set; }

        [XmlElement("ImportTnOrderSummary")]
        public ImportTnOrderSummary[] ImportTnOrderSummarys { get; set; }
    }

    public class ImportTnOrderSummary
    {
        public int accountId { get; set; }
        public int CountOfTNs { get; set; }
        public string CustomerOrderId { get; set; }
        public string userId { get; set; }
        public string lastModifiedDate { get; set; }
        public string OrderDate { get; set; }
        public string OrderType { get; set; }
        public string OrderStatus { get; set; }
        public string OrderId { get; set; }

    }


}


