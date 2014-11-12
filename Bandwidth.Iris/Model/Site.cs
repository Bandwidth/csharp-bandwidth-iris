using System;
using System.Threading.Tasks;

namespace Bandwidth.Iris.Model
{
    public class Site: BaseModel
    {
        private const string SitePath = "sites";
        public static async Task<Site> Get(Client client, string id)
        {
            if (id == null) throw new ArgumentNullException("id");
            var item = await client.MakeGetRequest<Site>(client.ConcatAccountPath(SitePath), null, id);
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
            var items = await client.MakeGetRequest<Site[]>(client.ConcatAccountPath(SitePath)) ?? new Site[0];
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
            using (var response = await client.MakePostRequest(SitePath, item))
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

        public string Name { get; set; }
        public string Description { get; set; }
        public Address Address { get; set; }
    }
}
