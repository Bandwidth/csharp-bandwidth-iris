using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Bandwidth.Iris.Model
{
    public class Aeui
    {

        public async static Task<AlternateEndUserIdentifiersResponse> List(Client client)
        {
            var item = await client.MakeGetRequest<AlternateEndUserIdentifiersResponse>(client.ConcatAccountPath($"/aeuis"));
            return item;
        }

        public static Task<AlternateEndUserIdentifiersResponse> List()
        {
            return List(Client.GetInstance());
        }

        public async static Task<AlternateEndUserIdentifierResponse> Get(Client client, string acid)
        {
            var item = await client.MakeGetRequest<AlternateEndUserIdentifierResponse>(client.ConcatAccountPath($"/aeuis/{acid}"));
            return item;
        }

        public static Task<AlternateEndUserIdentifierResponse> Get(string orderId)
        {
            return Get(Client.GetInstance(), orderId);
        }
    }

    public class AlternateEndUserIdentifiersResponse
    {
        public int TotalCount { get; set; }
        public Links Links { get; set; }

        [XmlArray("AlternateEndUserIdentifiers")]
        [XmlArrayItem("AlternateEndUserIdentifier")]
        public AlternateEndUserIdentifier[] AlternateEndUserIdentifiers { get; set; }
    }

    public class AlternateEndUserIdentifierResponse
    {
        public AlternateEndUserIdentifier AlternateEndUserIdentifier { get; set; }
    }

    public class AlternateEndUserIdentifier
    {
        public string Identifier { get; set; }
        public string CallbackNumber { get; set; }
        public E911 E911 { get; set; }
    }
}
