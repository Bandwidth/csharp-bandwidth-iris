using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Bandwidth.Iris.Model
{
    [XmlRoot("DldaOrder")]
    public class Dlda: BaseModel
    {
        internal const string DldaPath = "dldas";


        public static async Task<Dlda> Create(Client client, Dlda item)
        {
            using (var response = await client.MakePostRequest(client.ConcatAccountPath(DldaPath), item, false)) 
            {
                return await Get(client, client.GetIdFromLocationHeader(response.Headers.Location));
            }
        }

        public static async Task<Dlda> Get(Client client, string id)
        {
            return (await client.MakeGetRequest<DldaOrderResponse>(client.ConcatAccountPath(DldaPath), null, id)).DldaOrder;
        }

        public static async Task<OrderIdUserIdDate[]> List(Client client, IDictionary<string, object> query = null)
        {
            return (await client.MakeGetRequest<ResponseSelectWrapper>(client.ConcatAccountPath(DldaPath), query)).ListOrderIdUserIdDate;
        }

#if !PCL
        public static Task<Dlda> Create(Dlda item)
        {
            return Create(Client.GetInstance(), item);
        }

        public static Task<Dlda> Get(string id)
        {
            return Get(Client.GetInstance(), id);
        }

        public static Task<OrderIdUserIdDate[]> List(IDictionary<string, object> query = null)
        {
            return List(Client.GetInstance(), query);
        }

#endif
        public Task Update(Dlda item)
        {
            return Client.MakePutRequest(Client.ConcatAccountPath(string.Format("{0}/{1}", DldaPath, Id)),
                item, true);
        }
        
        public async Task<OrderHistoryItem[]> GetHistory()
        {
            return
                (await
                    Client.MakeGetRequest<OrderHistoryWrapper>(Client.ConcatAccountPath(string.Format("{0}/{1}/history", DldaPath, Id))))
                    .Items;
        }

        public override string Id
        {
            get
            {
                return OrderId;
            }
            set
            {
                OrderId = value;
            }
        }

        public string CustomerOrderId { get; set; }
        public DateTime OrderCreateDate { get; set; }
        public string AccountId { get; set; }
        public string CreatedByUser { get; set; }
        public string OrderId { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public string ProcessingStatus { get; set; }
        public DldaTnGroup[] DldaTnGroups { get; set; }

    }

    public class DldaOrderResponse
    {
        public  Dlda DldaOrder { get; set; }
    }

    
    public class DldaTnGroup
    {
        public TelephoneNumbers TelephoneNumbers { get; set; }
        public string AccountType { get; set; }
        public string ListingType { get; set; }
        public ListingName ListingName { get; set; }
        public Address Address { get; set; }
        public string SubscriberType { get; set; }
        public bool ListAddress { get; set;}
    }

    public class ListingName
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class ResponseSelectWrapper
    {
        public OrderIdUserIdDate[] ListOrderIdUserIdDate { get; set; }
    }

    public class OrderIdUserIdDate
    {
        [XmlElement("accountId")]
        public string AccountId { get; set; }

        [XmlElement("orderId")]
        public string OrderId { get; set; }

        [XmlElement("userId")]
        public string UserId { get; set; }

        public string OrderType { get; set; }
        public string OrderStatus { get; set; }
        public DateTime OrderDate { get; set; }

        [XmlElement("CountOfTNs")]
        public int CountOfTns { get; set; }

        [XmlElement("lastModifiedDate")]
        public DateTime LastModifiedDate { get; set; }
    }

    public class OrderHistoryWrapper
    {
        [XmlElement("OrderHistory")]
        public OrderHistoryItem[] Items { get; set; }
    }

    public class OrderHistoryItem
    {
        public DateTime OrderDate {get; set;}
        public string Note{ get; set; }
        public string Author{ get; set; }
        public string Status{ get; set; }
    }
}
