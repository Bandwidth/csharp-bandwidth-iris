using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Bandwidth.Iris.Model
{
    public class RemoveImportedTnOrder
    {

        public string CustomerOrderId { get; set; }
        public TelephoneNumber[] TelephoneNumbers { get; set; }
        public string OrderCreateDate { get; set; }
        public string AccountId { get; set; }
        public string CreatedByUser { get; set; }
        public string OrderId { get; set; }
        public string LastModifiedDate { get; set; }
        public string ProcessingStatus { get; set; }

        readonly static private string removeImportedTnOrdersPath = "removeImportedTnOrders";

        public async static Task<RemoveImportedTnOrderResponse> Create(Client client, RemoveImportedTnOrder order)
        {
            var item = await client.MakePostRequest<RemoveImportedTnOrderResponse>(client.ConcatAccountPath(removeImportedTnOrdersPath), order);
            return item;
        }

        public static Task<RemoveImportedTnOrderResponse> Create(RemoveImportedTnOrder order)
        {
            return Create(Client.GetInstance(), order);
        }

        public async static Task<RemoveImportedTnOrders> List(Client client, Dictionary<string, object> query)
        {
            var item = await client.MakeGetRequest<RemoveImportedTnOrders>(client.ConcatAccountPath(removeImportedTnOrdersPath), query);
            return item;
        }

        public static Task<RemoveImportedTnOrders> List(Dictionary<string, object> query)
        {
            return List(Client.GetInstance(), query);
        }

        public async static Task<RemoveImportedTnOrder> Get(Client client, string orderId)
        {
            var item = await client.MakeGetRequest<RemoveImportedTnOrder>(client.ConcatAccountPath(($"{removeImportedTnOrdersPath}/{orderId}")));
            return item;
        }

        public static Task<RemoveImportedTnOrder> Get(string orderId)
        {
            return Get(Client.GetInstance(), orderId);
        }

        public async static Task<OrderHistoryWrapper> GetHistory(Client client, string orderId)
        {
            var item = await client.MakeGetRequest<OrderHistoryWrapper>(client.ConcatAccountPath($"{removeImportedTnOrdersPath}/{orderId}/history"));
            return item;
        }

        public static Task<OrderHistoryWrapper> GetHistory(string orderId)
        {
            return GetHistory(Client.GetInstance(), orderId);
        }

    }

    public class RemoveImportedTnOrderResponse
    {
        public RemoveImportedTnOrder RemoveImportedTnOrder { get; set; }
    }

    public class RemoveImportedTnOrders
    {
        public int TotalCount { get; set; }
        [XmlElement("RemoveImportedTnOrderSummary")]
        public RemoveImportedTnOrderSummary[] Items { get; set; }
    }

    public class RemoveImportedTnOrderSummary
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
