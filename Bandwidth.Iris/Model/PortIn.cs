using System;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Bandwidth.Iris.Model
{
    public class PortIn
    {
        private const string PortInPath = "portins";

        public static Task<LnpOrderResponse> CreateOrder(Client client, LnpOrder order)
        {
            return client.MakePostRequest<LnpOrderResponse>(client.ConcatAccountPath(PortInPath), order);
        }

#if !PCL
        public static Task<LnpOrderResponse> CreateOrder(LnpOrder order)
        {
            return CreateOrder(Client.GetInstance(), order);
        }
#endif
    }

    public class LnpOrder
    {
        public string BillingTelephoneNumber { get; set; }
        public Subscriber Subscriber { get; set; }
        public string LoaAuthorizingPerson { get; set; }

        [XmlArrayItem("PhoneNumber")]
        public string[] ListOfPhoneNumbers { get; set; }
        public string SiteId { get; set; }
    }

    public class Subscriber
    {
        public string SubscriberType { get; set; }
        public string BusinessName { get; set; }
        public Address ServiceAddress { get; set; }
    }

    public class LnpOrderResponse : LnpOrder
    {
        public string ProcessingStatus { get; set; }
        public DateTime RequestedFocDate { get; set; }
        public Status Status { get; set; }
    }

    public class Status
    {
        public string Code { get; set; }
        public string Description { get; set; }
    }
}
