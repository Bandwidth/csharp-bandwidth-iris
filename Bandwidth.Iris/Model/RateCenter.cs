using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bandwidth.Iris.Model
{
    public class RateCenter
    {
        private const string RateCenterPath = "rateCenters";
        public async static Task<RateCenter[]> List(Client client, IDictionary<string, object> query)
        {
            return (await client.MakeGetRequest<RateCenterResponse>(RateCenterPath, query)).RateCenters;
        }
#if !PCL
        public static Task<RateCenter[]> List(IDictionary<string, object> query)
        {
            return List(Client.GetInstance(), query);
        }
#endif
        public string Abbreviation { get; set; }
        public string Name { get; set; }
    }

    public class RateCenterResponse
    {
        public RateCenter[] RateCenters { get; set; }
    }
}
