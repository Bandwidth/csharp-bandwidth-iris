using System;
using System.Threading.Tasks;

namespace Bandwidth.Iris.Model
{
    public class Site: BaseModel
    {
        internal const string SitePath = "sites";
        public static async Task<Site> Get(Client client, string id)
        {
            if (id == null) throw new ArgumentNullException("id");
            var item = (await client.MakeGetRequest<SiteResponse>(client.ConcatAccountPath(SitePath), null, id)).Site;
            item.Client = client;
            return item;
        }
#if !PCL
        public static Task<Site> Get(string callId)
        {
            return Get(Client.GetInstance(), callId);
        }
#endif

        public static async Task<Site[]> List(Client client)
        {
            var items = (await client.MakeGetRequest<SitesResponse>(client.ConcatAccountPath(SitePath))).Sites ?? new Site[0];
            foreach (var item in items)
            {
                item.Client = client;
            }
            return items;
        }

        

#if !PCL
        public static Task<Site[]> List()
        {
            return List(Client.GetInstance());
        }

#endif

        public static async Task<Site> Create(Client client, Site item)
        {
            using (var response = await client.MakePostRequest(client.ConcatAccountPath(SitePath), item))
            {
                return await Get(client, client.GetIdFromLocationHeader(response.Headers.Location));
            }
        }

#if !PCL
        public static Task<Site> Create(Site item)
        {
            return Create(Client.GetInstance(), item);
        }

#endif
        public Task Update(Site site)
        {
            return Client.MakePutRequest(Client.ConcatAccountPath(string.Format("{0}/{1}", SitePath, Id)),
                site, true);
        }

        public Task Delete()
        {
            return Client.MakeDeleteRequest(Client.ConcatAccountPath(string.Format("{0}/{1}", SitePath, Id)));
        }

        internal const string SipPeerPath = "sippeers";

        public async Task<SipPeer> CreateSipPeer(SipPeer item)
        {
            item =
                (await Client.MakePostRequest<TNSipPeersResponse>(
                    Client.ConcatAccountPath(string.Format("{0}/{1}/{2}", SitePath, Id, SipPeerPath)), item)).SipPeers.SipPeer;
            item.Client = Client;
            item.SiteId = Id;
            return item;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public Address Address { get; set; }
    }

    public class SitesResponse
    {
        public Site[] Sites { get; set; }
    }

    public class SiteResponse
    {
        public Site Site { get; set; }
    }

    public class TNSipPeersResponse
    {
        public SipPeers SipPeers { get; set; }
    }

    public class SipPeers
    {
        public SipPeer SipPeer { get; set; }
    }
}
