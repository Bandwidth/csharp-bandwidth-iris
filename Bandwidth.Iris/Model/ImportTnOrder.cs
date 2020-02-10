using System.Collections.Generic;
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


