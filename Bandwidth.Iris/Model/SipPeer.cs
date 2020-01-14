using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Bandwidth.Iris.Model
{
    public class SipPeer: BaseModel
    {

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

        [XmlArrayItem("Host")]
        public HostData[] VoiceHosts { get; set; }

        [XmlArrayItem("Host")]
        public HostData[] SmsHosts { get; set; }

        public TerminationHost[] TerminationHosts { get; set; }

        public CallingName CallingName { get; set; }

        public string FinalDestinationUri { get; set; }


        public static Task<SipPeer> Create(Client client, SipPeer item)
        {
            if (item.SiteId == null) throw new ArgumentException("SiteId is required");
            var site = new Site {Id = item.SiteId};
            site.SetClient(client);
            return site.CreateSipPeer(item);
        }

        public static Task<SipPeer> Get(Client client, string siteId, string id)
        {
            if (siteId == null) throw new ArgumentNullException("siteId");
            if (id == null) throw new ArgumentNullException("id");
            var site = new Site { Id = siteId };
            site.SetClient(client);
            return site.GetSipPeer(id);
        }

        public static Task<SipPeer> Update(Client client, string siteId, string id, SipPeer sipPeer)
        {
            if (siteId == null) throw new ArgumentNullException("siteId");
            if (id == null) throw new ArgumentNullException("id");
            var site = new Site { Id = siteId };
            site.SetClient(client);
            return site.UpdateSipPeer(id, sipPeer);
        }

        public static Task<SipPeer[]> List(Client client, string siteId)
        {
            if (siteId == null) throw new ArgumentNullException("siteId");
            var site = new Site { Id = siteId };
            site.SetClient(client);
            return site.GetSipPeers();
        }

#if !PCL

        public static Task<SipPeer> Update(string siteId, string id, SipPeer sipPeer)
        {
            return Update(Client.GetInstance(), siteId, id, sipPeer);
        }
        public static Task<SipPeer> Create(SipPeer item)
        {
            return Create(Client.GetInstance(), item);
        }
        public static Task<SipPeer> Get(string siteId, string id)
        {
            return Get(Client.GetInstance(), siteId, id);
        }

        public static Task<SipPeer[]> List(string siteId)
        {
            return List(Client.GetInstance(), siteId);
        }

#endif
        public Task Delete()
        {
            if(SiteId == null) throw new ArgumentNullException("SiteId");
            return Client.MakeDeleteRequest(Client.ConcatAccountPath(string.Format("{0}/{1}/{2}/{3}", Site.SitePath, SiteId, Site.SipPeerPath, Id)));
        }

        #region tns and movetns

        private const string TnsPath = "tns";
        private const string MoveTnsPath = "movetns";
        public async Task<SipPeerTelephoneNumber> GetTns(string number)
        {
            if (number == null) throw new ArgumentNullException("number");
            if (SiteId == null) throw new ArgumentNullException("SiteId");
            var response = await Client.MakeGetRequest<SipPeerTelephoneNumberResponse>(Client.ConcatAccountPath(string.Format("{0}/{1}/{2}/{3}/{4}/{5}", Site.SitePath, SiteId, Site.SipPeerPath, Id, TnsPath, number)));
            return response.SipPeerTelephoneNumber;
        }

        public async Task<SipPeerTelephoneNumber[]> GetTns()
        {
            if (SiteId == null) throw new ArgumentNullException("SiteId");
            var response = await Client.MakeGetRequest<SipPeerTelephoneNumbersResponse>(Client.ConcatAccountPath(string.Format("{0}/{1}/{2}/{3}/{4}", Site.SitePath, SiteId, Site.SipPeerPath, Id, TnsPath)));
            return response.SipPeerTelephoneNumbers;
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

        #endregion

        #region .../product/origination/settings
        private static readonly string PRODUCTS = "products";
        private static readonly string ORIGINATION = "origination";
        private static readonly string SETTINGS = "settings";

        public static async Task<SipPeerOriginationSettingsResponse> GetOriginationSettings(Client client, string siteId, string id)
        {
            return await client.MakeGetRequest<SipPeerOriginationSettingsResponse>(
                    client.ConcatAccountPath($"/{siteId}/sippers/{id}/{PRODUCTS}/{ORIGINATION}/{SETTINGS}")
                );
        }
        public static async Task<SipPeerOriginationSettingsResponse> GetOriginationSettings( string siteId, string id)
        {
            return await GetOriginationSettings(Client.GetInstance(), siteId, id);
        }

        public static async Task<SipPeerOriginationSettingsResponse> SetOriginationSettings(Client client, string siteId, string id, SipPeerOriginationSettings settings)
        {
            return await client.MakePostRequest<SipPeerOriginationSettingsResponse>(
                    client.ConcatAccountPath($"/{siteId}/sippers/{id}/{PRODUCTS}/{ORIGINATION}/{SETTINGS}"),
                    settings
                );
        }
        public static async Task<SipPeerOriginationSettingsResponse> SetOriginationSettings(string siteId, string id, SipPeerOriginationSettings settings)
        {
            return await SetOriginationSettings(Client.GetInstance(), siteId, id, settings);
        }

        public static async Task<HttpResponseMessage> UpdateOriginationSettings(Client client, string siteId, string id, SipPeerOriginationSettings settings)
        {
            return await client.MakePutRequest<HttpResponseMessage>(
                    client.ConcatAccountPath($"/{siteId}/sippers/{id}/{PRODUCTS}/{ORIGINATION}/{SETTINGS}"),
                    settings
                );
        }
        public static async Task<HttpResponseMessage> UpdateOriginationSettings(string siteId, string id, SipPeerOriginationSettings settings)
        {
            return await UpdateOriginationSettings(Client.GetInstance(), siteId, id, settings);
        }

        #endregion

        #region .../product/termination/settings

        public static readonly string TERMINATION = "termination";
        
        public static async Task<SipPeerTerminationSettingsResponse> GetTerminationSetting(Client client, string siteId, string id)
        {
            return await client.MakeGetRequest<SipPeerTerminationSettingsResponse>(
                    client.ConcatAccountPath($"/{siteId}/sippers/{id}/{PRODUCTS}/{TERMINATION}/{SETTINGS}")
                );
        }

        public static async Task<SipPeerTerminationSettingsResponse> GetTerminationSetting(string siteId, string id)
        {
            return await GetTerminationSetting(Client.GetInstance(), siteId, id);
        }

        public static async Task<SipPeerTerminationSettingsResponse> SetTerminationSettings(Client client, string siteId, string id, SipPeerTerminationSettings settings )
        {
            return await client.MakePostRequest<SipPeerTerminationSettingsResponse>(
                    client.ConcatAccountPath($"/{siteId}/sippers/{id}/{PRODUCTS}/{TERMINATION}/{SETTINGS}"),
                    settings
                );
        }

        public static async Task<SipPeerTerminationSettingsResponse> SetTerminationSettingsResponse(string siteId, string id, SipPeerTerminationSettings settings)
        {
            return await SetTerminationSettings(Client.GetInstance(), siteId, id, settings);
        }

        public static async Task<HttpResponseMessage> UpdateTerminationSettings(Client client, string siteId, string id, SipPeerTerminationSettings settings)
        {
            return await client.MakePutRequest<HttpResponseMessage>(
                    client.ConcatAccountPath($"/{siteId}/sippers/{id}/{PRODUCTS}/{TERMINATION}/{SETTINGS}"),
                    settings
                );
        }

        public static async Task<HttpResponseMessage> UpdateTerminationSettings(string siteId, string id, SipPeerTerminationSettings settings)
        {
            return await UpdateTerminationSettings(Client.GetInstance(), siteId, id, settings);
        }

        #endregion

        #region .../products/messaging/features   /sms || /mms

        public static readonly string MESSAGING = "messaging";
        public static readonly string MMS = "mms";
        public static readonly string SMS = "sms";

        public static async Task<SipPeerSmsFeatureResponse> GetSMSSetting(Client client, string siteId, string id)
        {
            return await client.MakeGetRequest<SipPeerSmsFeatureResponse>(
                    client.ConcatAccountPath($"/{siteId}/sippers/{id}/{PRODUCTS}/{MESSAGING}/{SMS}")
                );
        }

        public static async Task<SipPeerSmsFeatureResponse> GetSMSSetting(string siteId, string id)
        {
            return await GetSMSSetting(Client.GetInstance(), siteId, id);
        }

        public static async Task<SipPeerSmsFeatureResponse> CreateSMSSettings(Client client, string siteId, string id, SipPeerSmsFeature settings)
        {
            return await client.MakePostRequest<SipPeerSmsFeatureResponse>(
                    client.ConcatAccountPath($"/{siteId}/sippers/{id}/{PRODUCTS}/{MESSAGING}/{SMS}"),
                    settings
                );
        }

        public static async Task<SipPeerSmsFeatureResponse> CreateSMSSettings(string siteId, string id, SipPeerSmsFeature settings)
        {
            return await CreateSMSSettings(Client.GetInstance(), siteId, id, settings);
        }

        public static async Task<HttpResponseMessage> UpdateSMSSettings(Client client, string siteId, string id, SipPeerSmsFeature settings)
        {
            return await client.MakePutRequest<HttpResponseMessage>(
                    client.ConcatAccountPath($"/{siteId}/sippers/{id}/{PRODUCTS}/{MESSAGING}/{SMS}"),
                    settings
                );
        }

        public static async Task<HttpResponseMessage> UpdateSMSSettings(string siteId, string id, SipPeerSmsFeature settings)
        {
            return await UpdateSMSSettings(Client.GetInstance(), siteId, id, settings);
        }

        public static async Task<HttpResponseMessage> DeleteSMSSettings(Client client, string siteId, string id)
        {
            return await client.MakeDeleteRequestWithResponse(
                    client.ConcatAccountPath($"/{siteId}/sippers/{id}/{PRODUCTS}/{MESSAGING}/{SMS}")
                );
        }

        public static async Task<HttpResponseMessage> DeleteSMSSettings(string siteId, string id )
        {
            return await DeleteSMSSettings(Client.GetInstance(), siteId, id);
        }

        public static async Task<SipPeerSmsFeatureResponse> GetSMSSetting(Client client, string siteId, string id)
        {
            return await client.MakeGetRequest<SipPeerSmsFeatureResponse>(
                    client.ConcatAccountPath($"/{siteId}/sippers/{id}/{PRODUCTS}/{MESSAGING}/{SMS}")
                );
        }

        public static async Task<SipPeerSmsFeatureResponse> GetSMSSetting(string siteId, string id)
        {
            return await GetSMSSetting(Client.GetInstance(), siteId, id);
        }

        public static async Task<SipPeerSmsFeatureResponse> CreateSMSSettings(Client client, string siteId, string id, SipPeerSmsFeature settings)
        {
            return await client.MakePostRequest<SipPeerSmsFeatureResponse>(
                    client.ConcatAccountPath($"/{siteId}/sippers/{id}/{PRODUCTS}/{MESSAGING}/{SMS}"),
                    settings
                );
        }

        public static async Task<SipPeerSmsFeatureResponse> CreateSMSSettings(string siteId, string id, SipPeerSmsFeature settings)
        {
            return await CreateSMSSettings(Client.GetInstance(), siteId, id, settings);
        }

        public static async Task<HttpResponseMessage> UpdateSMSSettings(Client client, string siteId, string id, SipPeerSmsFeature settings)
        {
            return await client.MakePutRequest<HttpResponseMessage>(
                    client.ConcatAccountPath($"/{siteId}/sippers/{id}/{PRODUCTS}/{MESSAGING}/{SMS}"),
                    settings
                );
        }

        public static async Task<HttpResponseMessage> UpdateSMSSettings(string siteId, string id, SipPeerSmsFeature settings)
        {
            return await UpdateSMSSettings(Client.GetInstance(), siteId, id, settings);
        }

        public static async Task<HttpResponseMessage> DeleteSMSSettings(Client client, string siteId, string id)
        {
            return await client.MakeDeleteRequestWithResponse(
                    client.ConcatAccountPath($"/{siteId}/sippers/{id}/{PRODUCTS}/{MESSAGING}/{SMS}")
                );
        }

        public static async Task<HttpResponseMessage> DeleteSMSSettings(string siteId, string id)
        {
            return await DeleteSMSSettings(Client.GetInstance(), siteId, id);
        }

        #endregion

    }


    public class CallingName
    {
        public bool Display { get; set; }
        public bool Enforced { get; set; }
    }

    public class HostData
    {
        public string HostName { get; set; }
        public int Port { get; set; }
    }

    public class TerminationHost
    {
        public string HostName { get; set; }
        public int Port { get; set; }
        public string CustomerTrafficAllowed { get; set; }
        public bool DataAllowed { get; set; }
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

    public class SipPeerTelephoneNumbersResponse
    {
        public SipPeerTelephoneNumber[] SipPeerTelephoneNumbers { get; set; }
    }

    public class SipPeerResponse
    {
        public SipPeer SipPeer { get; set; }
    }

    public class SipPeerOriginationSettingsResponse
    {
        public SipPeerOriginationSettings SipPeerOriginationSettings { get; set; }
    }

    public class SipPeerOriginationSettings
    {
        public string VoiceProtocol { get; set; }
        public HttpSettings HttpSettings { get; set; }


    }

    public class HttpSettings
    {
        public string HttpVoiceV2AppId { get; set; }

        public string proxyPeerId { get; set; }
    }

    public class SipPeerTerminationSettingsResponse
    {
        public SipPeerTerminationSettings SipPeerTerminationSettings  { get; set;}
    }

    public class SipPeerTerminationSettings
    {
        public string VoiceProtocol { get; set; }
        public HttpSettings HttpSettings { get; set; }
    }

    public class SipPeerSmsFeatureResponse
    {
        public SipPeerSmsFeature SipPeerSmsFeature { get; set; }
    }

    public class SipPeerSmsFeature
    {
        public SipPeerSmsFeatureSettings SipPeerSmsFeatureSettings { get; set; }
        [XmlArrayItem("SmppHosts")]
        public SmppHost[] SmppHosts { get; set; }

    }

    public class SipPeerSmsFeatureSettings
    {
        public bool TollFree { get; set; }
        public bool ShortCode { get; set; }
        public string A2pLongCode { get; set; }
        public string A2PMessageClass { get; set; }
        public string A2pCampaignId { get; set; }
        public string Protocol { get; set; }
        public bool Zone1 { get; set; }
        public bool Zone2 { get; set; }
        public bool Zone3 { get; set; }
        public bool Zone4 { get; set; }
        public bool Zone5 { get; set; }
    }

    public class SmppHost
    {
        public string HostName { get; set; }
        public string HostId { get; set; }
        public int Priority { get; set; }
        public string ConnectionType { get; set; }
    }

    public class MmsFeatureResponse
    {
        public MmsFeature MmsFeature { get; set; }
    }

    public class MmsFeature
    {
        public MmsSettings MmsSettings { get; set; }
        public Protocols Protocols { get; set; }
    }

    public class MmsSettings
    {
        public string protocol { get; set; }
    }

    public class Protocols
    {
        public HTTP HTTP { get; set; }
        public MM4 MM4 { get; set; }
    }

    public class HTTP
    {
        public HttpSettings HttpSettings { get; set; }
    }

    public class MM4
    {
        public string Tls { get; set; }

        public MmsMM4TermHosts MmsMM4TermHosts { get; set; }
        public MmsMM4OrigHosts MmsMM4OrigHosts { get; set; }
    }

    public class MmsMM4TermHosts
    {
        [XmlArrayItem("TermHosts")]
        public TermHost[] TermHosts { get; set; }
    }

    public class TermHost
    {
        public string HostName { get; set; }
    }

    public class MmsMM4OrigHosts
    {
        [XmlArrayItem("OrigHosts")]
        public OrigHost[] OrigHosts { get; set; }
    }

    public class OrigHost
    {
        public string HostName { get; set; }
        public int Port { get; set; }
        public int Priority { get; set; }

    }

}
