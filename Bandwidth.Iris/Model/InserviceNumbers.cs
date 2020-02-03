using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bandwidth.Iris.Model
{
    class InserviceNumbers
    {

        readonly static private string inserviceNumbersPath = "inserviceNumbers";

        public async static Task<TNs> Get(Client client, Dictionary<string, object> query)
        {
            var item = await client.MakeGetRequest<TNs>(client.ConcatAccountPath(inserviceNumbersPath), query);
            return item;
        }

        public static Task<TNs> Get(Dictionary<string, object> query)
        {
            return Get(Client.GetInstance(), query);
        }

    }

    public class TNs
    {
        public int TotalCount { get; set; }

        public Links Links { get; set; }

        public TelephoneNumber[] TelephoneNumbers { get; set; }
    }


}
