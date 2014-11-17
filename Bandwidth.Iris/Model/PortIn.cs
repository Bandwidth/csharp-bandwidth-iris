using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Bandwidth.Iris.Model
{
    public class PortIn: BaseModel
    {
        private const string PortInPath = "portins";

        public async static Task<PortIn> Create(Client client, PortIn order)
        {
            return (await client.MakePostRequest<LnpOrderResponse>(client.ConcatAccountPath(PortInPath), order)) as PortIn;
        }

#if !PCL
        public static Task<PortIn> Create(PortIn order)
        {
            return Create(Client.GetInstance(), order);
        }
#endif
        public override string Id {
            get { return OrderId; }
            set { OrderId = value; } 
        }
        public string OrderId { get; set; }
        public string BillingTelephoneNumber { get; set; }
        public Subscriber Subscriber { get; set; }
        public string LoaAuthorizingPerson { get; set; }

        [XmlArrayItem("PhoneNumber")]
        public string[] ListOfPhoneNumbers { get; set; }
        public string SiteId { get; set; }
        public string ProcessingStatus { get; set; }
        public Status Status { get; set; }
        public bool Triggered { get; set; }
        public string BillingType { get; set; }

    }


    public class LnpOrderResponse : PortIn
    {
        
    }

    public class Subscriber
    {
        public string SubscriberType { get; set; }
        public string BusinessName { get; set; }
        public Address ServiceAddress { get; set; }
    }

    
    public class Status
    {
        public string Code { get; set; }
        public string Description { get; set; }
    }
}
