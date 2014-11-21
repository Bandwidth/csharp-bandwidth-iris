using System;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Bandwidth.Iris.Model
{
    [XmlRoot("TelephoneNumberResponse")]
    public class Tn
    {
        private Client _client;
        internal const string TnsPath = "tns";
        public static async Task<Tn> Get(Client client, string number)
        {
            if (number == null) throw new ArgumentNullException("number");
            var item = await client.MakeGetRequest<Tn>(TnsPath, null, number);
            item.Client = client;
            return item;
        }
        
#if !PCL
        public static Task<Tn> Get(string number)
        {
            return Get(Client.GetInstance(), number);
        }

        [XmlIgnore]
        public Client Client
        {
            get { return _client ?? (_client = Client.GetInstance()); }
            set { _client = value; }
        }
#else
        [XmlIgnore]
        public Client Client { get; set; }

#endif

        public Task<TelephoneNumberSite> GetSites()
        {
            return Client.MakeGetRequest<TelephoneNumberSite>(string.Format("{0}/{1}/sites", TnsPath, TelephoneNumber));
        }

        public Task<TelephoneNumberSipPeer> GetSipPeers()
        {
            return Client.MakeGetRequest<TelephoneNumberSipPeer>(string.Format("{0}/{1}/sippeers", TnsPath, TelephoneNumber));
        }

        public async Task<TelephoneNumberRateCenter> GetRateCenter()
        {
            using (var response = await Client.MakeGetRequest(string.Format("{0}/{1}/ratecenter", TnsPath, TelephoneNumber)))
            {
                var text = await response.Content.ReadAsStringAsync();
                var xml = XDocument.Parse(text);
                return new TelephoneNumberRateCenter
                {
                    RateCenter = xml.Descendants("RateCenter").First().Value,
                    State = xml.Descendants("State").First().Value
                };
            }
        }

        
        public async Task<TelephoneNumberDetails> GetDetails()
        {
            return
                (await
                    Client.MakeGetRequest<TelephoneNumberResponse>(string.Format("{0}/{1}/tndetails", TnsPath,
                        TelephoneNumber))).TelephoneNumberDetails;
        }

        public async Task<string> GetLata()
        {
            using (var response = await Client.MakeGetRequest(string.Format("{0}/{1}/lata", TnsPath, TelephoneNumber)))
            {
                var xml = XDocument.Parse(await response.Content.ReadAsStringAsync());
                return xml.Descendants("Lata").First().Value;
            }
        }
        
        public string TelephoneNumber { get; set; }
        public string Status { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public DateTime OrderCreateDate { get; set; }
        public string OrderId { get; set; }
        public string OrderType { get; set; }
        public string SiteId { get; set; }
        public string AccountId { get; set; }

        
    }

    [XmlType("SipPeer")]
    public class TelephoneNumberSipPeer
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    [XmlType("Site")]
    public class TelephoneNumberSite
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class TelephoneNumberRateCenter
    {
        public string State { get; set; }
        public string RateCenter { get; set; }
    }

    public class TelephoneNumberResponse
    {
        public TelephoneNumberDetails TelephoneNumberDetails { get; set; } 
    }

    public class TelephoneNumberDetails
    {
        public string City { get; set; }
        public string Lata { get; set; }
        public string RateCenter { get; set; }
        public string Status { get; set; }
        public string AccountId { get; set; }
        public string FullNumber { get; set; }
        public string Tier { get; set; }
        public string VendorId { get; set; }
        public string VendorName { get; set; }
        public DateTime LastModified { get; set; }
        public string State { get; set; }
    }
}
