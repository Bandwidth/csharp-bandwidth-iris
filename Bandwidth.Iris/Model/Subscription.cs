using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Bandwidth.Iris.Model
{
    public class Subscription : BaseModel
    {
        internal const string SubscriptionPath = "subscriptions";
        public static async Task<Subscription> Get(Client client, string id)
        {
            if (id == null) throw new ArgumentNullException("id");
            var item = (await client.MakeGetRequest<SubscriptionsResponse>(client.ConcatAccountPath(SubscriptionPath), null, id)).Subscriptions.First();
            item.Client = client;
            return item;
        }
#if !PCL
        public static Task<Subscription> Get(string id)
        {
            return Get(Client.GetInstance(), id);
        }
#endif

        public static async Task<Subscription[]> List(Client client, IDictionary<string, object> query = null)
        {
            var items = (await client.MakeGetRequest<SubscriptionsResponse>(client.ConcatAccountPath(SubscriptionPath), query)).Subscriptions ?? new Subscription[0];
            foreach (var item in items)
            {
                item.Client = client;
            }
            return items;
        }



#if !PCL
        public static Task<Subscription[]> List(IDictionary<string, object> query = null)
        {
            return List(Client.GetInstance(), query);
        }

#endif

        public static async Task<Subscription> Create(Client client, Subscription item)
        {
            using (var response = await client.MakePostRequest(client.ConcatAccountPath(SubscriptionPath), item))
            {
                return await Get(client, client.GetIdFromLocationHeader(response.Headers.Location));
            }
        }

#if !PCL
        public static Task<Subscription> Create(Subscription item)
        {
            return Create(Client.GetInstance(), item);
        }

#endif
        public Task Update(Subscription site)
        {
            return Client.MakePutRequest(Client.ConcatAccountPath(string.Format("{0}/{1}", SubscriptionPath, Id)),
                site, true);
        }

        public Task Delete()
        {
            return Client.MakeDeleteRequest(Client.ConcatAccountPath(string.Format("{0}/{1}", SubscriptionPath, Id)));
        }

        public override string Id
        {
            get { return SubscriptionId; }
            set { SubscriptionId = value; }
        }
        public string SubscriptionId { get; set; }
        public string OrderType { get; set; }
        public string OrderId { get; set; }
        public EmailSubscription EmailSubscription { get; set; }
        public CallbackSubscription CallbackSubscription { get; set; }
    }

    public class CallbackSubscription
    {
        [XmlElement("URL")]
        public string Url { get; set; }
        public string User { get; set; }
        public Int64 Expiry { get; set; }
    }

    public class EmailSubscription
    {
        public string Email { get; set; }
        public string DigestRequested { get; set; }
    }

    public class SubscriptionsResponse
    {
        public Subscription[] Subscriptions { get; set; }
    }
}
