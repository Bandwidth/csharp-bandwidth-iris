using System;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Bandwidth.Iris.Model
{
    public class SipPeer: BaseModel
    {
        public static Task<SipPeer> Create(Client client, SipPeer item)
        {
            if (item.SiteId == null) throw new ArgumentException("SiteId is required");
            var site = new Site {Id = item.SiteId};
            site.SetClient(client);
            return site.CreateSipPeer(item);
        }


#if !PCL
        public static Task<SipPeer> Create(SipPeer item)
        {
            return Create(Client.GetInstance(), item);
        }

#endif
        public Task Delete()
        {
            if(SiteId == null) throw new ArgumentNullException("SiteId");
            return Client.MakeDeleteRequest(Client.ConcatAccountPath(string.Format("{0}/{1}/{2}/{3}", Site.SitePath, SiteId, Site.SipPeerPath, Id)));
        }

        private const string TnsPath = "tns";
        private const string MoveTnsPath = "movetns";
        public async Task<SipPeerTelephoneNumber> GetTns(string number)
        {
            if (number == null) throw new ArgumentNullException("number");
            if (SiteId == null) throw new ArgumentNullException("SiteId");
            var response = await Client.MakeGetRequest<SipPeerTelephoneNumberResponse>(Client.ConcatAccountPath(string.Format("{0}/{1}/{2}/{3}/{4}/{5}", Site.SitePath, SiteId, Site.SipPeerPath, Id, TnsPath, number)));
            return response.SipPeerTelephoneNumber;
        }

        public Task UpdateTns(string number, SipPeerTelephoneNumber data)
        {
            if (number == null) throw new ArgumentNullException("number");
            if (SiteId == null) throw new ArgumentNullException("SiteId");
            return Client.MakePutRequest(
                Client.ConcatAccountPath(
                    string.Format("{0}/{1}/{2}/{3}/{4}/{5}", Site.SitePath, SiteId, 
                    Site.SipPeerPath, Id, TnsPath, number)), data, true);
        }

        public Task MoveTns(params string[] numbers)
        {
            if (SiteId == null) throw new ArgumentNullException("SiteId");
            return Client.MakePostRequest(
                Client.ConcatAccountPath(string.Format("{0}/{1}/{2}/{3}/{4}", 
                    Site.SitePath, SiteId, Site.SipPeerPath, Id, MoveTnsPath)), 
                    new SipPeerTelephoneNumbers{Numbers = numbers}, true);
        }
        public string SiteId { get; set; }

        public override string Id
        {
            get { return PeerId; }
            set { PeerId = value; }
        }

        public string PeerId { get; set; }

        [XmlElement("PeerName")]
        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsDefaultPeer { get; set; }

        public string ShortMessagingProtocol { get; set; }

        public Host[] VoiceHosts { get; set; }
        
        public Host[] SmsHosts { get; set; }
        
        public Host[] TerminationHosts { get; set; }
        
        public CallingName CallingName { get; set; }
        
        public VoiceHostGroup[] VoiceHostGroups { get; set; }
    }

    public class VoiceHostGroup
    {
    }

    public class CallingName
    {
        public bool Display { get; set; }
        public bool Enforced { get; set; }
    }

    public class Host
    {
        public string HostName { get; set; }
    }

    public class SipPeerTelephoneNumbers: IXmlSerializable
    {
        
        public string[] Numbers { get; set; }

        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(System.Xml.XmlReader reader)
        {
            throw new NotImplementedException();
        }

        public void WriteXml(System.Xml.XmlWriter writer)
        {
            foreach (var number in Numbers ?? new string[0])
            {
                writer.WriteElementString("FullNumber", number);
            }
        }
    }
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

    public class SipPeerResponse
    {
        public SipPeer SipPeer { get; set; }
    }
}
