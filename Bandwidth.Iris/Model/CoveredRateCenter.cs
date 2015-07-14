using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Bandwidth.Iris.Model
{
    public class CoveredRateCenter
    {
        private const string CoveredRateCenterPath = "coveredRateCenters";
        public async static Task<CoveredRateCenter[]> List(Client client, IDictionary<string, object> query = null)
        {
            var r = (await client.MakeGetRequest<CoveredRateCenters>(CoveredRateCenterPath, query));
            return r.CoveredRateCenter;
        }
#if !PCL
        public static Task<CoveredRateCenter[]> List(IDictionary<string, object> query = null)
        {
            return List(Client.GetInstance(), query);
        }
#endif
        public string Abbreviation { get; set; }
        public string Name { get; set; }
    }

    public class CoveredRateCenters
    {
        [XmlElement("CoveredRateCenter")]
        public CoveredRateCenter[] CoveredRateCenter { get; set; }

        public Links Links { get; set; }
    }
}
