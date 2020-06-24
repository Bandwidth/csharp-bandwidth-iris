using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Bandwidth.Iris.Model
{
    class TnOptions : BaseModel
    {

        public async static Task<TnOptionOrders> List(Client client, IDictionary<string, object> query = null)
        {
            var item = await client.MakeGetRequest<TnOptionOrders>(client.ConcatAccountPath($"/tnoptions"), query);
            return item;
        }

        public static Task<TnOptionOrders> List(IDictionary<string, object> query = null)
        {
            return List(Client.GetInstance(), query);
        }

        public async static Task<TnOptionOrder> Get(Client client, string orderId)
        {
            var item = await client.MakeGetRequest<TnOptionOrder>(client.ConcatAccountPath($"/tnoptions/{orderId}"));
            return item;
        }

        public static Task<TnOptionOrder> Get(string orderId)
        {
            return Get(Client.GetInstance(), orderId);
        }

        public async static Task<TnOptionOrderResponse> Create(Client client, TnOptionOrder order)
        {
            var item = await client.MakePostRequest<TnOptionOrderResponse>(client.ConcatAccountPath($"/tnoptions"), order);
            return item;
        }

        public static Task<TnOptionOrderResponse> Create(TnOptionOrder order)
        {
            return Create(Client.GetInstance(), order);
        }
    }

    public class TnOptionOrderResponse
    {
        public TnOptionOrder TnOptionOrder { get; set; }
    }

    public class TnOptionOrder
    {
        public string CustomerOrderId { get; set; }

        public string OrderCreateDate { get; set; }

        public string AccountId { get; set; }

        public string CreatedByUser { get; set; }

        public string OrderId { get; set; }

        public string LastModifiedDate { get; set; }

        public string ProcessingStatus { get; set; }

        [XmlArray("TnOptionGroups")]
        [XmlArrayItem("TnOptionGroup")]
        public List<TnOptionGroup> TnOptionGroups { get; set; }

        [XmlArray("ErrorList")]
        [XmlArrayItem("Error")]
        public List<Error> Errors { get; set; }

        [XmlArray("Warnings")]
        [XmlArrayItem("Warning")]
        public List<Warning> Warnings { get; set; }
    }

    public class TnOptionGroup
    {
        public string NumberFormat { get; set; }
        public string RPIDFormat { get; set; }
        public string RewriteUser { get; set; }
        public string CallForward { get; set; }
        public string CallingNameDisplay { get; set; }
        public string PortOutPasscode { get; set; }
        public string Protected { get; set; }
        public string Sms { get; set; }
        public string FinalDestinationURI { get; set; }

        public A2pSettings A2pSettings { get; set; }

        [XmlArray("TelephoneNumbers")]
        [XmlArrayItem("TelephoneNumber")]
        public List<string> TelephoneNumbers { get; set; }
    }

    public class A2pSettings
    {
        public string MessageClass { get; set; }
        public string CampaignId { get; set; }
        public string Action { get; set; }
    }

    public class TnOptionOrders
    {
        public int TotalCount { get; set; }

        [XmlElement("TnOptionOrder")]
        public List<TnOptionOrder> TnOptionOrderList { get; set; }

        [XmlElement("TnOptionOrderSummary")]
        public List<TnOptionOrderSummary> TnOptionOrderSummaryList { get; set; }
    }

    public class TnOptionOrderSummary
    {
        [XmlElement("accountId")]
        public string AccountId { get; set; }
        public int CountOfTNs { get; set; }
        [XmlElement("userId")]
        public string UserId { get; set; }
        [XmlElement("lastModifiedDate")]
        public string LastModifiedDate { get; set; }
        public string OrderDate { get; set; }
        public string OrderType { get; set; }
        public string OrderStatus { get; set; }
        public string OrderId { get; set; }
    }

    public class Warning
    {
        public string TelephoneNumber { get; set; }
        public string Description { get; set; }
    }
}
