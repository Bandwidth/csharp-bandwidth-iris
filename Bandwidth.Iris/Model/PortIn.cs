using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Net.Http;

namespace Bandwidth.Iris.Model
{
    [XmlType("LnpOrder")]
    public class PortIn : BaseModel
    {
        private const string PortInPath = "portins";
        private const string LoasPath = "loas";

        public async static Task<LnpOrderResponse> Create(Client client, PortIn order)
        {
            var item = await client.MakePostRequest<LnpOrderResponse>(client.ConcatAccountPath(PortInPath), order);
            item.Client = client;
            return item;
        }

#if !PCL
        public static Task<LnpOrderResponse> Create(PortIn order)
        {
            return Create(Client.GetInstance(), order);
        }
#endif

        public async Task<string> CreateFile(Stream stream, string mediaType)
        {
            using (var message = await Client.SendData(Client.ConcatAccountPath(string.Format("{0}/{1}/{2}", PortInPath, Id, LoasPath)), stream, mediaType))
            {
                return
                    XDocument.Parse(await message.Content.ReadAsStringAsync())
                        .Descendants("filename")
                        .FirstOrDefault()
                        .Value;
            }
        }

        public async Task<string> CreateFile(byte[] buffer, string mediaType)
        {
            using (var message = await Client.SendData(Client.ConcatAccountPath(string.Format("{0}/{1}/{2}", PortInPath, Id, LoasPath)), buffer, mediaType))
            {
                return
                    XDocument.Parse(await message.Content.ReadAsStringAsync())
                        .Descendants("filename")
                        .FirstOrDefault()
                        .Value;
            }
        }

        public Task UpdateFile(string fileName, Stream stream, string mediaType)
        {
            return Client.SendData(Client.ConcatAccountPath(string.Format("{0}/{1}/{3}/{2}", PortInPath, Id, fileName, LoasPath)), stream, mediaType, "PUT", true);
        }

        public Task UpdateFile(string fileName, byte[] buffer, string mediaType)
        {
            return Client.SendData(Client.ConcatAccountPath(string.Format("{0}/{1}/{3}/{2}", PortInPath, Id, fileName, LoasPath)), buffer, mediaType, "PUT", true);
        }

        public Task<FileMetadata> GetFileMetadata(string fileName)
        {
            return
                Client.MakeGetRequest<FileMetadata>(
                    Client.ConcatAccountPath(string.Format("{0}/{1}/{3}/{2}/metadata", PortInPath, Id, fileName, LoasPath)));
        }

        public Task<HttpResponseMessage> PutFileMetadata(string fileName, FileMetadata fileMetadata)
        {
            return
                Client.MakePutRequest(Client.ConcatAccountPath(string.Format("{0}/{1}/{3}/{2}/metadata", PortInPath, Id, fileName, LoasPath)), fileMetadata);
        }

        public Task DeleteFile(string fileName)
        {
            return
                Client.MakeDeleteRequest(
                    Client.ConcatAccountPath(string.Format("{0}/{1}/{3}/{2}", PortInPath, Id, fileName, LoasPath)));
        }

        public async Task<FileData[]> GetFiles(bool metadata = false)
        {
            var r = (await Client.MakeGetRequest<FileListResponse>(
                Client.ConcatAccountPath(string.Format("{0}/{1}/{2}", PortInPath, Id, LoasPath)),
                new Dictionary<string, object> { { "metadata", metadata.ToString().ToLowerInvariant() } }));
            return (r == null) ? new FileData[0] : r.FileData.ToArray();
        }

        public async Task<FileContent> GetFile(string fileName, bool asStream = false)
        {
            if (fileName == null) throw new ArgumentNullException("fileName");

            var response = await Client.MakeGetRequest(
                    Client.ConcatAccountPath(string.Format("{0}/{1}/{2}/{3}", PortInPath, Id, LoasPath, fileName)));
            var content = new FileContent(response) { MediaType = response.Content.Headers.ContentType.MediaType };
            if (asStream)
            {
                content.Stream = await response.Content.ReadAsStreamAsync();
            }
            else
            {
                content.Buffer = await response.Content.ReadAsByteArrayAsync();
            }
            return content;
        }

        public Task Update(LnpOrderSupp item)
        {
            return Client.MakePutRequest(Client.ConcatAccountPath(string.Format("{0}/{1}", PortInPath, Id)),
                item, true);
        }
        public Task Delete()
        {
            return
                Client.MakeDeleteRequest(
                    Client.ConcatAccountPath(string.Format("{0}/{1}", PortInPath, Id)));
        }

        public Task<LnpOrderResponse> GetOrder()
        {
            return Client.MakeGetRequest<LnpOrderResponse>(Client.ConcatAccountPath(string.Format("{0}/{1}", PortInPath, Id)));
        }
        public async Task<Note> AddNote(Note note)
        {
            using (var response = await Client.MakePostRequest(Client.ConcatAccountPath(string.Format("{0}/{1}/notes", PortInPath, Id)), note))
            {
                var list = await GetNotes();
                var id = Client.GetIdFromLocationHeader(response.Headers.Location);
                return list.First(n => n.Id == id);
            }
        }

        public async Task<Note[]> GetNotes()
        {
            return
                (await
                    Client.MakeGetRequest<Notes>(Client.ConcatAccountPath(string.Format("{0}/{1}/notes", PortInPath, Id))))
                    .List;
        }

        public Task<LnpResponseWrapper> GetPortIns(string accountId, DateTime date, DateTime enddate, DateTime startdate, string pon, string status, string tn, int page = 1, int size = 300)
        {

            Dictionary<string, object> query = new Dictionary<string, object>();

            query.Add("page", page);
            query.Add("size", size);

            if (date != null) query.Add("date", date);
            if (enddate != null) query.Add("enddate", enddate);
            if (startdate != null) query.Add("startdate", startdate);
            if (pon != null) query.Add("pon", pon);
            if (status != null) query.Add("status", status);
            if (tn != null) query.Add("tn", tn);

            return Client.MakeGetRequest<LnpResponseWrapper>(Client.ConcatAccountPath("/portins/"), query);
        }

        public override string Id
        {
            get { return OrderId; }
            set { OrderId = value; }
        }

        public string CustomerOrderId { get; set; }
        public DateTime RequestedFocDate { get; set; }
        public string AlternateSpid { get; set; }
        public string AccountNumber { get; set; }
        public string PinNumber { get; set; }
        public bool PartialPort { get; set; }
        [XmlArrayItem("TnAttribute")]
        public string[] TnAttributes { get; set; }
        public string OrderId { get; set; }
        public string BillingTelephoneNumber { get; set; }
        public string NewBillingTelephoneNumber { get; set; }
        public Subscriber Subscriber { get; set; }
        public string LoaAuthorizingPerson { get; set; }

        [XmlArrayItem("PhoneNumber")]
        public string[] ListOfPhoneNumbers { get; set; }
        public string SiteId { get; set; }
        public string PeerId { get; set; }
        public bool Triggered { get; set; }

    }

    [XmlType("LNPResponseWrapper")]
    public class LnpResponseWrapper
    {
        public int TotalCount { get; set; }

        public Links Links { get; set; }

        public string Snip { get; set; }

        [XmlElement("lnpPortInfoForGivenStatus")]
        public LnpPortInfoForGivenStatus[] lnpPortInfoForGivenStatuses { get; set; }

    }

    public class LnpPortInfoForGivenStatus
    {
        public int CountOfTNs { get; set; }

        [XmlElement("userId")]
        public string UserId { get; set; }

        [XmlElement("lastModifiedDate")]
        public DateTime LastModifiedDate { get; set; }

        public DateTime OrderDate { get; set; }

        public string OrderId { get; set; }

        public string OrderType { get; set; }

        public int ErrorCode { get; set; }

        public string ErrorMessage { get; set; }

        public string FullNumber { get; set; }

        public string ProcessingStatus { get; set; }

        public DateTime RequestedFOCDate { get; set; }

        public string VendorId { get; set; }


    }

    public class LnpOrderResponse : PortIn
    {
        public string OrderId { get; set; }
        public string ProcessingStatus { get; set; }
        public string CustomerOrderId { get; set; }
        public Status Status { get; set; }
        public string LoaAuthorizingPerson { get; set; }
        public Subscriber Subscriber { get; set; }
        public string BillingType { get; set; }
        [XmlElement("WirelessInfo")]
        public WirelessInfo[] WirelessInfo { get; set; }
        [XmlArrayItem("TnAttribute")]
        public string[] TnAttributes { get; set; }
        public string BillingTelephoneNumber { get; set; }
        public string NewBillingTelephoneNumber { get; set; }
        [XmlArrayItem("PhoneNumber")]
        public string[] ListOfPhoneNumbers { get; set; }
        public string AlternateSpid { get; set; }
        public string AccountId { get; set; }
        public string SiteId { get; set; }
        public string PeerId { get; set; }
        public string VendorName { get; set; }
        public DateTime OrderCreateDate { get; set; }

        public bool Triggered { get; set; }
        public string LosingCarrierName { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public PortType PortType { get; set; }
        [XmlElement("userId")]
        public string UserId { get; set; }
        [XmlElement("Errors")]
        public Errors[] Errors { get; set; }

    }

    public class LnpOrderSupp : PortIn
    {
        public string CustomerOrderId { get; set; }
        public string BillingTelephoneNumber { get; set; }
        public string NewBillingTelephoneNumber { get; set; }
        public DateTime RequestedFocDate { get; set; }
        [XmlElement("WirelessInfo")]
        public WirelessInfo[] WirelessInfo { get; set; }
        [XmlArrayItem("TnAttribute")]
        public string[] TnAttributes { get; set; }
        public Subscriber Subscriber { get; set; }
        public string SiteId { get; set; }
        public bool PartialPort { get; set; }
        [XmlArrayItem("PhoneNumber")]
        public string[] ListOfPhoneNumbers { get; set; }
        public string LoaAuthorizingPerson { get; set; }
        public string AlternateSpid { get; set; }


    }

    public class WirelessInfo
    {
        public string AccountNumber { get; set; }
        public string PinNumber { get; set; }
    }

    public class Subscriber
    {
        public string SubscriberType { get; set; }
        public string BusinessName { get; set; }
        public string AccountNumber { get; set; }
        public string PinNumber { get; set; }
        public string FirstName { get; set; }
        public string MiddleInitial { get; set; }
        public string LastName { get; set; }

        public Address ServiceAddress { get; set; }

    }

    public class Status
    {
        public string Code { get; set; }
        public string Description { get; set; }
    }

    [XmlType("FileMetaData")]
    public class FileMetadata
    {
        public string DocumentType { get; set; }
        public string DocumentName { get; set; }
    }

    [XmlType("fileListResponse")]
    public class FileListResponse
    {
        [XmlElement("fileCount")]
        public int FileCount { get; set; }

        [XmlElement("fileData")]
        public FileData[] FileData { get; set; }

        [XmlElement("fileNames")]
        public string[] FileNames { get; set; }

        [XmlElement("resultCode")]
        public string ResultCode { get; set; }

        [XmlElement("resultMessage")]
        public string ResultMessage { get; set; }
    }

    public class FileData
    {
        public string FileName { get; set; }
        [XmlElement("FileMetaData")]
        public FileMetadata FileMetadata { get; set; }
    }

    public class Errors
    {
        public string Code { get; set; }
        public string Description { get; set; }
    }

    public enum PortType
    {
        AUTOMATED, INTERNAL, MANUALOFFNET, MANUALONNET, MANUALTOLLFREE
    }
    public sealed class FileContent : IDisposable
    {
        private readonly IDisposable _owner;

        internal FileContent(IDisposable owner)
        {
            if (owner == null) throw new ArgumentNullException("owner");
            _owner = owner;
        }

        public string MediaType { get; set; }
        public Stream Stream { get; set; }
        public byte[] Buffer { get; set; }

        public void Dispose()
        {
            _owner.Dispose();
        }
    }
}
