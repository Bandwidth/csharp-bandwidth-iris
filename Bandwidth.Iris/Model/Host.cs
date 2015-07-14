using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Bandwidth.Iris.Model
{
    public class Host
    {
        internal const string HostPath = "hosts";

        public static async Task<SiteHost[]> List(Client client, IDictionary<string, object> query = null)
        {
            return (await client.MakeGetRequest<SiteHostsResponse>(client.ConcatAccountPath(HostPath), query)).SiteHosts;
        }

#if !PCL

        public static Task<SiteHost[]> List(IDictionary<string, object> query = null)
        {
            return List(Client.GetInstance(), query);
        }

#endif
    }

    public class SiteHostsResponse
    {
        public SiteHost[] SiteHosts { get; set; }
    }

    public class SiteHost
    {
        public string SiteId { get; set; }
        public SipPeerHost[] SipPeerHosts { get; set; }
    }

    public class SipPeerHost
    {
        public string SipPeerId { get; set; }
        
        [XmlArrayItem("Host")]
        public HostData[] VoiceHosts { get; set; }

        [XmlArrayItem("Host")]
        public HostData[] SmsHosts { get; set; }

        [XmlArrayItem("Host")]
        public HostData[] TerminationHosts { get; set; }
    }
}
