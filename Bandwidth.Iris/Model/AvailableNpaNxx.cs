using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Bandwidth.Iris.Model
{
    public class AvailableNpaNxx
    {
        private const string AvailableNpaNxxPath = "availableNpaNxx";
        public async static Task<AvailableNpaNxx[]> List(Client client, IDictionary<string, object> query = null)
        {
            return (await client.MakeGetRequest<AvailableNpaNxxResult>(client.ConcatAccountPath(AvailableNpaNxxPath), query)).AvailableNpaNxxList;
        }
#if !PCL
        public static Task<AvailableNpaNxx[]> List(IDictionary<string, object> query = null)
        {
            return List(Client.GetInstance(), query);
        }
#endif
        public string City { get; set; }
        public string State { get; set; }
        public string Npa { get; set; }
        public string Nxx { get; set; }
        public int Quantity { get; set; }
    }

    [XmlType("SearchResultForAvailableNpaNxx")]
    public class AvailableNpaNxxResult
    {
        public AvailableNpaNxx[] AvailableNpaNxxList { get; set; }
    }
}
