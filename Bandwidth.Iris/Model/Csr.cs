using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Bandwidth.Iris.Model
{

    [XmlType("Csr")]
    public class Csr : BaseModel
    {

        [XmlElement("accountId")]
        public string AccountId { get; set; }
        public string CustomerOrderId { get; set; }
        public string AccountNumber { get; set; }
        public string AccountTelephoneNumber { get; set; }
        public string EndUserName { get; set; }
        public string AuthorizingUserName { get; set; }
        public string CustomerCode { get; set; }
        public string EndUserPIN { get; set; }
        public string EndUserPassword { get; set; }
        public string AddressLine1 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZIPCode { get; set; }
        public string TypeOfService { get; set; }
        public string WorkingOrBillingTelephoneNumber { get; set; }
        public string Status { get; set; }

        private static string csrPath = "csrs";
        private static string notesPath = "notes";

        public async static Task<CsrResponse> Create(Client client, Csr csr)
        {
            var item = await client.MakePostRequest<CsrResponse>(client.ConcatAccountPath(csrPath), csr);
            return item;
        }

        public static Task<CsrResponse> Create(Csr csr)
        {
            return Create(Client.GetInstance(), csr);
        }

        public async static Task<CsrResponse> Get(Client client, string orderId)
        {
            var item = await client.MakeGetRequest<CsrResponse>(client.ConcatAccountPath($"{csrPath}/{orderId}"));
            return item;
        }

        public static Task<CsrResponse> Get(string orderId)
        {
            return Get(Client.GetInstance(), orderId);
        }

        public async static Task<CsrResponse> Replace(Client client, string orderId, Csr csr)
        {
            var item = await client.MakePutRequest<CsrResponse>(client.ConcatAccountPath($"{csrPath}/{orderId}"), csr);
            return item;
        }

        public static Task<CsrResponse> Replace(string orderId, Csr csr)
        {
            return Replace(Client.GetInstance(), orderId, csr);
        }

        public async static Task<Notes> ListNotes(Client client, string orderId)
        {
            var item = await client.MakeGetRequest<Notes>(client.ConcatAccountPath($"{csrPath}/{orderId}/{notesPath}"));
            return item;
        }

        public static Task<Notes> ListNotes(string orderId)
        {
            return ListNotes(Client.GetInstance(), orderId);
        }

        public  static Task UpdateNote(Client client, string orderId, string noteId, Note note)
        {
            var item = client.MakePutRequest<CsrResponse>(client.ConcatAccountPath($"{csrPath}/{orderId}/{notesPath}/{noteId}"), note);
            return item;
        }

        public static Task UpdateNote(string orderId, string noteId, Note note)
        {
            return UpdateNote(Client.GetInstance(), orderId, noteId, note);
        }

        public static Task CreateNote(Client client, string orderId, Note note)
        {
            var item = client.MakePostRequest(client.ConcatAccountPath($"{csrPath}/{orderId}/{notesPath}"), note);
            return item;
        }

        public static Task CreateNote(string orderId, Note note)
        {
            return CreateNote(Client.GetInstance(), orderId, note);
        }

    }

    [XmlType("CsrResponse")]
    public class CsrResponse
    {
        public string CustomerOrderId { get; set; }
        public string CustomerName { get; set; }
        public string LastModifiedBy { get; set; }
        public string LastModifiedDate { get; set; }
        public string OrderCreateDate { get; set; }
        public string AccountId { get; set; }
        public string OrderId { get; set; }
        public string Status { get; set; }
        public string AccountNumber { get; set; }
        public string AccountTelephoneNumber { get; set; }
        public string EndUserName { get; set; }
        public string AuthorizingUserName { get; set; }
        public string CustomerCode { get; set; }
        public string EndUserPIN { get; set; }
        public string EndUserPassword { get; set; }
        public string AddressLine1 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZIPCode { get; set; }
        public string TypeOfService { get; set; }

        public CsrData CsrData { get; set; }


    }

    public class CsrData
    {
        public string AccountNumber { get; set; }
        public string CustomerName { get; set; }
        public Address ServiceAddress { get; set; }
        public string WorkingTelephoneNumber { get; set; }

        [XmlArray("WorkingTelephoneNumbersOnAccount")]
        [XmlArrayItem("TelephoneNumber")]
        public string[] WorkingTelephoneNumbersOnAccount { get; set; }
        

    }


   
}