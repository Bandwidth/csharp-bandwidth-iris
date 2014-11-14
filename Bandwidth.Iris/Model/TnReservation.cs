using System;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Bandwidth.Iris.Model
{
    [XmlType("Reservation")]
    public class TnReservation: BaseModel
    {
        internal const string TnReservationPath = "tnreservation";
        public static async Task<TnReservation> Get(Client client, string id)
        {
            if (id == null) throw new ArgumentNullException("id");
            var item = (await client.MakeGetRequest<ReservationResponse>(client.ConcatAccountPath(TnReservationPath), null, id)).Reservation;
            item.Client = client;
            return item;
        }
#if !PCL
        public static Task<TnReservation> Get(string id)
        {
            return Get(Client.GetInstance(), id);
        }
#endif



        public static async Task<TnReservation> Create(Client client, TnReservation item)
        {
            using (var response = await client.MakePostRequest(client.ConcatAccountPath(TnReservationPath), item))
            {
                return await Get(client, client.GetIdFromLocationHeader(response.Headers.Location));
            }
        }

#if !PCL
        public static Task<TnReservation> Create(TnReservation item)
        {
            return Create(Client.GetInstance(), item);
        }

#endif
        
        public Task Delete()
        {
            return Client.MakeDeleteRequest(Client.ConcatAccountPath(string.Format("{0}/{1}", TnReservationPath, Id)));
        }

        public override string Id
        {
            get { return ReservationId; }
            set { ReservationId = value; }
        }
        public string ReservationId { get; set; }
        public string AccountId { get; set; }
        public string ReservationExpires { get; set; }
        public string ReservedTn { get; set; }
    }

    public class ReservationResponse
    {
        public TnReservation Reservation { get; set; }
    }
}
