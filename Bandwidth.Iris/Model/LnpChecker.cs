using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Bandwidth.Iris.Model
{
    public class LnpChecker
    {
        private const string LnpCheckerPath = "lnpchecker";
        public static Task<NumberPortabilityResponse> Check(Client client, string[] numbers, bool fullCheck = false)
        {
            var request = new NumberPortabilityRequest
            {
                TnList = numbers
            };
            return client.MakePostRequest<NumberPortabilityResponse>(string.Format("{0}?fullCheck={1}", client.ConcatAccountPath(LnpCheckerPath), fullCheck.ToString().ToLower()), request);
        }
#if !PCL
        public static Task<NumberPortabilityResponse> Check(string[] numbers, bool fullCheck = false)
        {
            return Check(Client.GetInstance(), numbers, fullCheck);
        }
#endif
    }

    public class NumberPortabilityResponse
    {
        public string PortType { get; set; }
        public RateCenterGroup[] SupportedRateCenters { get; set; }
        public RateCenterGroup[] UnsupportedRateCenters { get; set; }
        public RateCenterGroup[] PartnerSupportedRateCenters { get; set; }
        [XmlArrayItem("Tn")]
        public string[] PortableNumbers { get; set; }
        public LosingCarriers SupportedLosingCarriers { get; set; }
        public LosingCarriers UnsupportedLosingCarriers { get; set; }
        public PortabilityErrors PortabilityErrors { get; set; }
    }

    public class PortabilityErrors
    {
        [XmlElement("Error")]
        public Error[] Errors { get; set; }
    }

    public class Error
    {
        public string Code { get; set; }
        public string Description { get; set; }
        [XmlArrayItem("Tn")]
        public string[] TelephoneNumbers { get; set; }
    }

    public class LosingCarriers
    {
        public LosingCarrierTnList LosingCarrierTnList { get; set; }
    }

    public class LosingCarrierTnList
    {
        [XmlElement("LosingCarrierSPID")]
        public string LosingCarrierSpid { get; set; }
        public string LosingCarrierName { get; set; }
        [XmlArrayItem("Tn")]
        public string[] TnList { get; set; }
    }

    public class RateCenterGroup
    {
        public string RateCenter { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        
        [XmlElement("LATA")]
        public string Lata { get; set; }

        [XmlArrayItem("Tier")]
        public string[] Tiers { get; set; }

        [XmlArrayItem("Tn")]
        public string[] TnList { get; set; }
    }

    public class NumberPortabilityRequest
    {
        [XmlArrayItem("Tn")]
        public string[] TnList { get; set; }
    }
}
