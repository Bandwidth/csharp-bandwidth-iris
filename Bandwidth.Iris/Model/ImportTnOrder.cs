using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bandwidth.Iris.Model
{
    public class ImportTnOrder
    {
       
        public string CustomerOrderId { get; set; }
        public string OrderCreateDate { get; set; }
        public string AccountId { get; set; }
        public string CreatedByUser { get; set; }
        public string OrderId { get; set; }
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

        public async static Task<ImportTnOrder[]> List(Client client, Dictionary<string, object> query)
        {
            var item = await client.MakeGetRequest<ImportTnOrder[]>(client.ConcatAccountPath(importTnOrdersPath), query);
            return item;
        }

        public static Task<ImportTnOrder[]> List(Dictionary<string, object> query)
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

        public async static Task<ImportTnOrder> GetHistory(Client client, string orderId)
        {
            var item = await client.MakeGetRequest<ImportTnOrder>(client.ConcatAccountPath($"{importTnOrdersPath}/{orderId}/history"));
            return item;
        }

        public static Task<ImportTnOrder> GetHistory(string orderId)
        {
            return GetHistory(Client.GetInstance(), orderId);
        }



    }

    public class ImportTnOrderResponse
    {
        public ImportTnOrder ImportTnOrder { get; set; }
    }


}


