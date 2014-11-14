using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Bandwidth.Iris.Model
{
    public class Disconnect
    {
        private const string DisconnectNumbersPath = "disconnects";
        public static Task DisconnectNumbers(Client client, string orderName, params string[] numbers)
        {
            var order = new DisconnectTelephoneNumberOrder
            {
                Name = orderName,
                DisconnectTelephoneNumberOrderType = new DisconnectTelephoneNumberOrderType
                {
                    TelephoneNumberList = numbers
                }
            };
            return client.MakePostRequest(client.ConcatAccountPath(DisconnectNumbersPath), order, true);
        }
#if !PCL
        public static Task DisconnectNumbers(string orderName, params string[] numbers)
        {
            return DisconnectNumbers(Client.GetInstance(), orderName, numbers);
        }
#endif
    }

    public class DisconnectTelephoneNumberOrder
    {
        [XmlElement("name")]
        public string Name { get; set; }
        public DisconnectTelephoneNumberOrderType DisconnectTelephoneNumberOrderType { get; set; }
    }

    public class DisconnectTelephoneNumberOrderType
    {
        [XmlArrayItem("TelephoneNumber")]
        public string[] TelephoneNumberList { get; set; }
    }
}
