using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Bandwidth.Iris.Model
{
    public class LineOptionOrder : BaseModel
    {
        internal const string LineOptionOrderPath = "lineOptionOrders";


        public static async Task<string[]> Create(Client client, params TnLineOptions[] options)
        {
            return
                (await
                    client.MakePostRequest<LineOptionOrderResponse>(client.ConcatAccountPath(LineOptionOrderPath),
                        new LineOptionOrderRequest { TnLineOptions = options })).LineOptions.CompletedNumbers;
        }

#if !PCL
        public static Task<string[]> Create(params TnLineOptions[] options)
        {
            return Create(Client.GetInstance(), options);
        }

#endif

    }

    public class TnLineOptions
    {
        public string TelephoneNumber { get; set; }
        public string CallingNameDisplay { get; set; }
    }

    [XmlRoot("LineOptionOrder")]
    public class LineOptionOrderRequest
    {
        [XmlElement("TnLineOptions")]
        public TnLineOptions[] TnLineOptions { get; set; }
    }

    public class LineOptionOrderResponse
    {
        public LineOptions LineOptions { get; set; }
    }

    public class LineOptions
    {
        [XmlArrayItem("TelephoneNumber")]
        public string[] CompletedNumbers { get; set; }
    }
}
