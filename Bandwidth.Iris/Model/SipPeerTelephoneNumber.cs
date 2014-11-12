using System.Xml.Serialization;

namespace Bandwidth.Iris.Model
{
    public class SipPeerTelephoneNumber
    {
        public string FullNumber { get; set; }
        public string CallForward { get; set; }
        public string NumberFormat { get; set; }
        public string RewriteUser { get; set; }
        [XmlElement("RPIDFormat")]
        public string RpidFormat { get; set; }
    }

    public class SipPeerTelephoneNumberResponse
    {
        public SipPeerTelephoneNumber SipPeerTelephoneNumber { get; set; }
    }
}
