using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bandwidth.Iris.Model
{
    public class City
    {
        private const string CityCenterPath = "cities";
        public async static Task<City[]> List(Client client, IDictionary<string, object> query)
        {
            return (await client.MakeGetRequest<CityResponse>(CityCenterPath, query)).Cities;
        }
#if !PCL
        public static Task<City[]> List(IDictionary<string, object> query)
        {
            return List(Client.GetInstance(), query);
        }
#endif
        public string RcAbbreviation { get; set; }
        public string Name { get; set; }
    }

    public class CityResponse
    {
        public City[] Cities { get; set; }
    }
}
