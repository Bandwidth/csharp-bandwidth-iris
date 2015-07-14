using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Bandwidth.Iris.Model
{
    public class DiscNumber
    {
        private const string DiscNumberPath = "discnumbers";
        public async static Task<string[]> List(Client client, IDictionary<string, object> query = null)
        {
            var r = (await client.MakeGetRequest<DiscNumberResponse>(client.ConcatAccountPath(DiscNumberPath), query));
            return r.TelephoneNumbers.Numbers;
        }

        public static async Task<Quantity> GetTotals(Client client)
        {
            return (await client.MakeGetRequest<Quantity>(client.ConcatAccountPath(DiscNumberPath) + "/totals"));
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
    public class DiscNumberResponse
    {
        public TelephoneNumbers TelephoneNumbers { get; set; }

    }

    public class TelephoneNumbers
    {
        [XmlElement("TelephoneNumber")]
        public string[] Numbers { get; set; }
    }
}
