using System;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Bandwidth.Iris.Model
{
    [XmlRoot("LidbOrder")]
    public class Lidb: BaseModel
    {
        internal const string LidbPath = "lidbs";
        public static async Task<Lidb> Get(Client client, string id)
        {
            if (id == null) throw new ArgumentNullException("id");
            var item = (await client.MakeGetRequest<Lidb>(client.ConcatAccountPath(LidbPath), null, id));
            item.Client = client;
            return item;
        }
#if !PCL
        public static Task<Lidb> Get(string id)
        {
            return Get(Client.GetInstance(), id);
        }
#endif

        public static async Task<OrderIdUserIdDate[]> List(Client client)
        {
            return (await client.MakeGetRequest<ResponseSelectWrapper>(client.ConcatAccountPath(LidbPath))).ListOrderIdUserIdDate ?? new OrderIdUserIdDate[0];
        }

        

#if !PCL
        public static Task<OrderIdUserIdDate[]> List()
        {
            return List(Client.GetInstance());
        }

#endif

        public static async Task<Lidb> Create(Client client, Lidb item)
        {
            using (var response = await client.MakePostRequest(client.ConcatAccountPath(LidbPath), item))
            {
                return await Get(client, client.GetIdFromLocationHeader(response.Headers.Location));
            }
        }

#if !PCL
        public static Task<Lidb> Create(Lidb item)
        {
            return Create(Client.GetInstance(), item);
        }

#endif
        public override string Id
        {
            get
            {
                return OrderId;
            }
            set
            {
                OrderId = value;
            }
        }
        
        public string CustomerOrderId { get; set; }
        
        [XmlElement("orderId")]
        public string OrderId { get; set; }

        public DateTime OrderCreateDate { get; set; }

        public string ProcessingStatus { get; set; }

        public string CreatedByUser { get; set; }
        
        public DateTime LastModifiedDate { get; set; }

        public DateTime OrderCompleteDate { get; set; }

        public LidbTnGroup[] LidbTnGroups{get; set;}
    }
        
    public class LidbTnGroup
    {
        [XmlArrayItem("TelephoneNumber")]
        public string[] TelephoneNumbers{get; set;}
        
        public string FullNumber {get; set;}
        
        public string SubscriberInformation {get; set;}
        
        public string UseType {get; set;}
        
        public string Visibility {get; set;}
    }

}
