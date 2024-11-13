using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Bandwidth.Iris.Model
{
    public class LsrOrder : BaseModel
    {
        internal const string LsrOrderPath = "lsrorders";

        public static async Task<LsrOrder> Get(Client client, string id)
        {
            if (id == null) throw new ArgumentNullException("id");
            var item = (await client.MakeGetRequest<LsrOrder>(client.ConcatAccountPath(LsrOrderPath), null, id));
            item.Client = client;
            return item;
        }
#if !PCL
        public static Task<LsrOrder> Get(string id)
        {
            return Get(Client.GetInstance(), id);
        }
#endif

        public static async Task<LsrOrderSummary[]> List(Client client, IDictionary<string, object> query = null)
        {
            return (await client.MakeGetRequest<LsrOrders>(client.ConcatAccountPath(LsrOrderPath), query)).Orders;
        }



#if !PCL
        public static Task<LsrOrderSummary[]> List(IDictionary<string, object> query = null)
        {
            return List(Client.GetInstance(), query);
        }

#endif


        public static async Task<LsrOrder> Create(Client client, LsrOrder item)
        {
            using (var response = await client.MakePostRequest(client.ConcatAccountPath(LsrOrderPath), item, false))
            {
                return await Get(client, client.GetIdFromLocationHeader(response.Headers.Location));
            }
        }

#if !PCL
        public static Task<LsrOrder> Create(LsrOrder item)
        {
            return Create(Client.GetInstance(), item);
        }

#endif
        public Task Update(LsrOrder item)
        {
            return Client.MakePutRequest(Client.ConcatAccountPath(string.Format("{0}/{1}", LsrOrderPath, Id)),
                item, true);
        }

        public async Task<Note> AddNote(Note note)
        {
            using (var response = await Client.MakePostRequest(Client.ConcatAccountPath(string.Format("{0}/{1}/notes", LsrOrderPath, Id)), note))
            {
                var list = await GetNotes();
                var id = Client.GetIdFromLocationHeader(response.Headers.Location);
                return list.First(n => n.Id == id);
            }
        }

        public async Task<Note[]> GetNotes()
        {
            return
                (await
                    Client.MakeGetRequest<Notes>(Client.ConcatAccountPath(string.Format("{0}/{1}/notes", LsrOrderPath, Id))))
                    .List;
        }

        public async Task<OrderHistoryItem[]> GetHistory()
        {
            return
                (await
                    Client.MakeGetRequest<OrderHistoryWrapper>(Client.ConcatAccountPath(string.Format("{0}/{1}/history", LsrOrderPath, Id))))
                    .Items;
        }


        public override string Id
        {
            get { return OrderId; }
            set { OrderId = value; }
        }

        public string OrderId { get; set; }
        public string CustomerOrderId { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime OrderCreateDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public string AccountId { get; set; }
        public string OrderStatus { get; set; }
        [XmlElement("SPID")]
        public string Spid { get; set; }
        public string BillingTelephoneNumber { get; set; }
        public string Pon { get; set; }
        public string PonVersion { get; set; }
        public DateTime RequestedFocDate { get; set; }
        public string AuthorizingPerson { get; set; }
        public Subscriber Subscriber { get; set; }
        [XmlArrayItem("TelephoneNumber")]
        public string[] ListOfTelephoneNumbers { get; set; }
        [XmlElement("CountOfTNs")]
        public int CountOfTns { get; set; }
    }

    public class LsrOrders
    {
        [XmlElement("LsrOrderSummary")]
        public LsrOrderSummary[] Orders { get; set; }
    }

    public class LsrOrderSummary
    {
        public string Id
        {
            get { return OrderId; }
            set { OrderId = value; }
        }
        [XmlElement("accountId")]
        public string AccountId { get; set; }
        [XmlElement("CountOfTNs")]
        public int CountOfTns { get; set; }
        public string CustomerOrderId { get; set; }
        [XmlElement("lastModifiedDate")]
        public DateTime LastModifiedDate { get; set; }
        [XmlElement("userId")]
        public string UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderType { get; set; }
        public string OrderStatus { get; set; }
        public string BillingTelephoneNumber { get; set; }
        public DateTime ActualFocDate { get; set; }
        public string CreatedByUser { get; set; }
        public string OrderId { get; set; }
        public string Pon { get; set; }
        public string PonVersion { get; set; }
        public DateTime RequestedFocDate { get; set; }
    }
}
