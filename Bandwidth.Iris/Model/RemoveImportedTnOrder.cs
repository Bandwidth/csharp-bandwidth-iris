using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async static Task<RemoveImportedTnOrderResponse[]> List(Client client, Dictionary<string, object> query)
        {
            var item = await client.MakeGetRequest<RemoveImportedTnOrderResponse[]>(client.ConcatAccountPath(removeImportedTnOrdersPath), query);
            return item;
        }

        public static Task<RemoveImportedTnOrderResponse[]> List(Dictionary<string, object> query)
        {
            return List(Client.GetInstance(), query);
        }

        public async static Task<RemoveImportedTnOrderResponse> Get(Client client, string orderId)
        {
            var item = await client.MakeGetRequest<RemoveImportedTnOrderResponse>(client.ConcatAccountPath(($"{removeImportedTnOrdersPath}/{orderId}")));
            return item;
        }

        public static Task<RemoveImportedTnOrderResponse> Get(string orderId)
        {
            return Get(Client.GetInstance(), orderId);
        }

        public async static Task<RemoveImportedTnOrderResponse> GetHistory(Client client, string orderId)
        {
            var item = await client.MakeGetRequest<RemoveImportedTnOrderResponse>(client.ConcatAccountPath($"{removeImportedTnOrdersPath}/{orderId}/history"));
            return item;
        }

        public static Task<RemoveImportedTnOrderResponse> GetHistory(string orderId)
        {
            return GetHistory(Client.GetInstance(), orderId);
        }

    }

    public class RemoveImportedTnOrderResponse
    {
        public RemoveImportedTnOrder RemoveImportedTnOrder { get; set; }
        public RemoveImportedTnOrder[] RemoveImportedTnOrders { get; set; }
    }
}
