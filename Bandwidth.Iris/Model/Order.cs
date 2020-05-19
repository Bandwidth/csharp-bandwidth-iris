using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Bandwidth.Iris.Model
{
    public class Order: BaseModel
    {
        internal const string OrderPath = "orders";

        public Order()
        {
            PartialAllowed = true;
        }

        public static Task<OrderResult> Create(Client client, Order item)
        {
            return client.MakePostRequest<OrderResult>(client.ConcatAccountPath(OrderPath), item);
        }

        public static Task<OrderResult> Get(Client client, string id)
        {
            return client.MakeGetRequest<OrderResult>(client.ConcatAccountPath(OrderPath), null, id);
        }

        public static async Task<OrderListResponse> List(Client client, IDictionary<string, object> query = null)
        {
            return (await client.MakeGetRequest<OrderListResponse>(client.ConcatAccountPath(OrderPath), query));
        }

#if !PCL
        public static Task<OrderResult> Create(Order item)
        {
            return Create(Client.GetInstance(), item);
        }

        public static Task<OrderResult> Get(string id)
        {
            return Get(Client.GetInstance(), id);
        }

        public static Task<OrderListResponse> List(IDictionary<string, object> query = null)
        {
            return List(Client.GetInstance(), query);
        }

#endif
        public Task Update(Order item)
        {
            return Client.MakePutRequest(Client.ConcatAccountPath(string.Format("{0}/{1}", OrderPath, Id)),
                item, true);
        }

        public async Task<Note> AddNote(Note note)
        {
            using (var response = await Client.MakePostRequest(Client.ConcatAccountPath(string.Format("{0}/{1}/notes", OrderPath, Id)), note))
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
                    Client.MakeGetRequest<Notes>(Client.ConcatAccountPath(string.Format("{0}/{1}/notes", OrderPath, Id))))
                    .List;
        }

        public async Task<AreaCode[]> GetAreaCodes()
        {
            return
                (await Client.MakeGetRequest<TelephoneDetailsReportsWithAreaCodes>(Client.ConcatAccountPath(string.Format("{0}/{1}/areaCodes", OrderPath, Id))))
                    .Codes;
        }

        public async Task<NpaNxx[]> GetNpaNxx()
        {
            return
                (await Client.MakeGetRequest<TelephoneDetailsReportsWithNpaNxx>(Client.ConcatAccountPath(string.Format("{0}/{1}/npaNxx", OrderPath, Id))))
                    .List;
        }

        public async Task<object[]> GetTotals()
        {
            return
                (await Client.MakeGetRequest<TelephoneDetailsReportsWithTotals>(Client.ConcatAccountPath(string.Format("{0}/{1}/totals", OrderPath, Id))))
                    .List;
        }

        public async Task<string[]> GetTns()
        {
            return
                (await Client.MakeGetRequest<TelephoneNumbers>(Client.ConcatAccountPath(string.Format("{0}/{1}/areaCodes", OrderPath, Id))))
                    .Numbers;
        }

        public async Task<OrderHistoryItem[]> GetHistory()
        {
            return
                (await
                    Client.MakeGetRequest<OrderHistoryWrapper>(Client.ConcatAccountPath(string.Format("{0}/{1}/history", OrderPath, Id))))
                    .Items;
        }


        public override string Id {
            get { return OrderId; }
            set { OrderId = value; } 
        }
        
        [XmlElement("id")]
        public string OrderId { get; set; }
        public string Name { get; set; }
        public string SiteId { get; set; }
        public string PeerId { get; set; }
        [DefaultValue(false)]
        public bool BackOrderRequested { get; set; }
        [DefaultDateTime]
        public DateTime OrderCreateDate { get; set; }
        public string CustomerOrderId { get; set; }
        [DefaultValue(true)]
        public bool PartialAllowed { get; set; }
        [DefaultValue(false)] 
        public bool CloseOrder { get; set; }
        public ExistingTelephoneNumberOrderType ExistingTelephoneNumberOrderType { get; set; }
        public AreaCodeSearchAndOrderType AreaCodeSearchAndOrderType { get; set; }
        public RateCenterSearchAndOrderType RateCenterSearchAndOrderType { get; set; }
        [XmlElement("NPANXXSearchAndOrderType")]
        public NpaNxxSearchAndOrderType NpaNxxSearchAndOrderType { get; set; }
        public TollFreeVanitySearchAndOrderType TollFreeVanitySearchAndOrderType { get; set; }
        public TollFreeWildCharSearchAndOrderType TollFreeWildCharSearchAndOrderType { get; set; }
        public StateSearchAndOrderType StateSearchAndOrderType { get; set; }
        public CitySearchAndOrderType CitySearchAndOrderType { get; set; }
        [XmlElement("ZIPSearchAndOrderType")]
        public ZipSearchAndOrderType ZipSearchAndOrderType { get; set; }
        [XmlElement("LATASearchAndOrderType")]
        public LataSearchAndOrderType LataSearchAndOrderType { get; set; }

    }

    public class LataSearchAndOrderType
    {
        public string Lata { get; set; }
        public int Quantity { get; set; }
    }

    public class ZipSearchAndOrderType
    {
        public string Zip { get; set; }
        public int Quantity { get; set; }
    }

    public class CitySearchAndOrderType
    {
        public string City{ get; set; }
        public string State { get; set; }
        public int Quantity { get; set; }
    }

    public class StateSearchAndOrderType
    {
        public string State { get; set; }
        public int Quantity { get; set; }
    }

    public class TollFreeWildCharSearchAndOrderType
    {
        public string TollFreeWildCardPattern { get; set; }
        public int Quantity { get; set; }
    }

    public class TollFreeVanitySearchAndOrderType
    {
        public string TollFreeVanity { get; set; }
        public int Quantity { get; set; }
    }

    public class NpaNxxSearchAndOrderType
    {
        public string NpaNxx { get; set; }
        [XmlElement("EnableTNDetail")]
        public bool EnableTnDetail { get; set; }
        [XmlElement("EnableLCA")]
        public bool EnableLca { get; set; }
        public int Quantity { get; set; }
    }

    public class RateCenterSearchAndOrderType
    {
        public string AreaCode { get; set; }
        public int Quantity { get; set; }
    }

    public class AreaCodeSearchAndOrderType
    {
        public string RateCenter { get; set; }
        public string State { get; set; }
        public int Quantity { get; set; }
    }

    public class ExistingTelephoneNumberOrderType
    {
        [XmlArrayItem("TelephoneNumber")]
        public string[] TelephoneNumberList { get; set; }
        [XmlArrayItem("ReservationId")]
        public string[] ReservationIdList { get; set; }
    }

    [XmlType("OrderResponse")]
    public class OrderResult
    {
        public Order Order { get; set; }
        public string CreatedByUser { get; set; }
        public int CompletedQuantity { get; set; }
        public int FailedQuantity { get; set; }
        public int PendingQuantity { get; set; }
        public DateTime OrderCompleteDate { get; set; }
        public string OrderStatus { get; set; }
        public TelephoneNumber[] CompletedNumbers { get; set; }
    }


    public class TelephoneNumber
    {
        public string FullNumber { get; set; }
    }

    public class Orders
    {
        [XmlElement("Order")]
        public OrderResult[] List { get; set; }
    }

    [XmlRoot("TelephoneDetailsReports")]
    public class TelephoneDetailsReportsWithAreaCodes
    {
        [XmlElement("TelephoneDetailsReport")]
        public AreaCode[] Codes { get; set; }
    }

    public class AreaCode
    {
        [XmlElement("AreaCode")]
        public string Code { get; set; }
        public int Count { get; set; }
    }

    [XmlRoot("TelephoneDetailsReports")]
    public class TelephoneDetailsReportsWithNpaNxx
    {
        [XmlElement("TelephoneDetailsReport")]
        public NpaNxx[] List { get; set; }
    }

    public class NpaNxx
    {
        [XmlElement("NPA-NXX")]
        public string Value { get; set; }
        public int Count { get; set; }
    }

    [XmlRoot("TelephoneDetailsReports")]
    public class TelephoneDetailsReportsWithTotals
    {
        [XmlElement("TelephoneDetailsReport")]
        public object[] List { get; set; }
    }

    [XmlType("ResponseSelectWrapper")]
    public class OrderListResponse
    {
        [XmlElement("ListOrderIdUserIdDate")]
        public ListOrderIdUserIdDate Orders { get; set; }
    }

    [XmlType("ListOrderIdUserIdDate")]
    public class ListOrderIdUserIdDate
    {
        public int? TotalCount { get; set; }
        public Links Links { get; set; }
        [XmlElement("OrderIdUserIdDate")]
        public List<OrderDetail> OrderDetails {get; set;}
    }

    [XmlType("OrderIdUserIdDate")]
    public class OrderDetail
    {
        [XmlElement("accountId")]
        public string AccountId { get; set; }

        [XmlElement("orderId")]
        public string OrderId { get; set; }

        [XmlElement("userId")]
        public string UserId { get; set; }

        public string OrderType { get; set; }
        public string OrderStatus { get; set; }
        public string OrderDate { get; set; }

        [XmlElement("CountOfTNs")]
        public int? CountOfTns { get; set; }

        [XmlElement("lastModifiedDate")]
        public string LastModifiedDate { get; set; }

        [XmlElement("TelephoneNumberDetails")]
        public TelephoneNumberDetailsWithCount TelephoneNumberDetailsWithCount { get; set; }
        
    }

    [XmlType("TelephoneNumberDetails")]
    public class TelephoneNumberDetailsWithCount
    {
        public List<StateWithCount> States { get; set; }
        public List<RateCenterWithCount> RateCenters { get; set; }
        public List<CityWithCount> Cities { get; set; }
        public List<TierWithCount> Tiers { get; set; }
        public List<VendorWithCount> Vendors { get; set; }
    }

    public class StateWithCount
    {
        public int? Count { get; set; }
        public string State { get; set; }
    }
    public class RateCenterWithCount
    {
        public int? Count { get; set; }
        public string RateCenter { get; set; }
    }
    public class CityWithCount
    {
        public int? Count { get; set; }
        public string City { get; set; }
    }
    public class TierWithCount
    {
        public int? Count { get; set; }
        public int Tier { get; set; }
    }
    public class VendorWithCount
    {
        public int? Count { get; set; }
        public string VendorName { get; set; }
        public int VendorId { get; set; }
    }



}
