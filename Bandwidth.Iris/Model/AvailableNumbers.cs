using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Bandwidth.Iris.Model
{
    public class AvailableNumbers
    {
        private const string AvailableNumbersPath = "availableNumbers";
        public static Task<AvailableNumbersResult> List(Client client, IDictionary<string, object> query = null)
        {
            return client.MakeGetRequest<AvailableNumbersResult>(client.ConcatAccountPath(AvailableNumbersPath), query);
        }
#if !PCL
        public static Task<AvailableNumbersResult> List(IDictionary<string, object> query = null)
        {
            return List(Client.GetInstance(), query);
        }
#endif
    }

    [XmlType("SearchResult")]
    public class AvailableNumbersResult
    {
        public int ResultCount { get; set; }

        [XmlArrayItem("TelephoneNumber")]
        public string[] TelephoneNumberList { get; set; }

        public TelephoneNumberDetail[] TelephoneNumberDetailList { get; set; }
    }

    public class TelephoneNumberDetail
    {
        public string City { get; set; }
        [XmlElement("LATA")]
        public string Lata { get; set; }
        public string RateCenter { get; set; }
        public string State { get; set; }
        public string FullNumber { get; set; }
        public string Tier { get; set; }
        public string VendorId { get; set; }
        public string VendorName { get; set; }
        public string TelephoneNumber { get; set; }
    }
}
