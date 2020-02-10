using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Bandwidth.Iris.Model
{
    public class ImportTnChecker
    {

        readonly static private string importCheckerPath = "importTnChecker";

        public async static Task<ImportTnCheckerResponse> Create(Client client, ImportTnCheckerPayload payload)
        {
            var item = await client.MakePostRequest<ImportTnCheckerResponse>(client.ConcatAccountPath(importCheckerPath), payload);
            return item;
        }

        public static Task<ImportTnCheckerResponse> Create(ImportTnCheckerPayload payload)
        {
            return Create(Client.GetInstance(), payload);
        }
    }

    public class ImportTnCheckerPayload
    {
        public TelephoneNumber[] TelephoneNumbers { get; set; }
        [XmlArray("ImportTnErrors")]
        [XmlArrayItem("ImportTnError")]
        public ImportTnError[] ImportTnErrors { get; set; }
    }

    public class ImportTnCheckerResponse
    {
        public ImportTnCheckerPayload ImportTnCheckerPayload { get; set; }

    }

    public class ImportTnError
    {
        public int Code { get; set; }
        public string Description { get; set; }
        public TelephoneNumber[] TelephoneNumbers { get; set; }
    }
}
