using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Bandwidth.Iris.Model
{
    public class InServiceNumber : BaseModel
    {
        internal const string InServiceNumberPath = "inserviceNumbers";


        public static async Task<string[]> List(Client client, IDictionary<string, object> query = null)
        {
            return (await client.MakeGetRequest<InServiceNumberTns>(client.ConcatAccountPath(InServiceNumberPath), query)).TelephoneNumbers.Numbers;
        }

        public static async Task<Quantity> GetTotals(Client client)
        {
            return (await client.MakeGetRequest<Quantity>(client.ConcatAccountPath(InServiceNumberPath) + "/totals"));
        }

#if !PCL

        public static Task<string[]> List(IDictionary<string, object> query = null)
        {
            return List(Client.GetInstance(), query);
        }

        public static Task<Quantity> GetTotals()
        {
            return GetTotals(Client.GetInstance());
        }

#endif

    }

    [XmlRoot("TNs")]
    public class InServiceNumberTns
    {
        public TelephoneNumbers TelephoneNumbers { get; set; }
    }

    public class Quantity
    {
        public int Count { get; set; }
    }
}
