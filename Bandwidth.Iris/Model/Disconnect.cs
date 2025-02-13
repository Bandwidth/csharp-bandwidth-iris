﻿using System;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Bandwidth.Iris.Model
{
    public class Disconnect : BaseModel
    {
        private const string DisconnectNumbersPath = "disconnects";
        public static Task Create(Client client, string orderName, params string[] numbers)
        {
            var order = new DisconnectTelephoneNumberOrder
            {
                Name = orderName,
                DisconnectTelephoneNumberOrderType = new DisconnectTelephoneNumberOrderType
                {
                    TelephoneNumberList = new TelephoneNumberList
                    {
                        TelephoneNumbers = numbers
                    }
                }
            };
            return client.MakePostRequest(client.ConcatAccountPath(DisconnectNumbersPath), order, true);
        }
#if !PCL
        public static Task Create(string orderName, params string[] numbers)
        {
            return Create(Client.GetInstance(), orderName, numbers);
        }
#endif
        public async Task<Note> AddNote(string orderId, Note note)
        {
            if (orderId == null) throw new ArgumentNullException("orderId");
            using (var response = await Client.MakePostRequest(Client.ConcatAccountPath(string.Format("{0}/{1}/notes", DisconnectNumbersPath, orderId)), note))
            {
                var list = await GetNotes(orderId);
                var id = Client.GetIdFromLocationHeader(response.Headers.Location);
                return list.First(n => n.Id == id);
            }
        }

        public async Task<Note[]> GetNotes(string orderId)
        {
            if (orderId == null) throw new ArgumentNullException("orderId");
            return
                (await
                    Client.MakeGetRequest<Notes>(Client.ConcatAccountPath(string.Format("{0}/{1}/notes", DisconnectNumbersPath, orderId))))
                    .List;
        }
    }

    public class DisconnectTelephoneNumberOrder
    {
        [XmlElement("name")]
        public string Name { get; set; }
        [XmlElement("DisconnectTelephoneNumberOrderType")]
        public DisconnectTelephoneNumberOrderType DisconnectTelephoneNumberOrderType { get; set; }
    }

    public class DisconnectTelephoneNumberOrderType
    {
        [XmlElement("TelephoneNumberList")]
        public TelephoneNumberList TelephoneNumberList { get; set; }
    }

    public class TelephoneNumberList
    {
        [XmlElement("TelephoneNumber")]
        public string[] TelephoneNumbers { get; set; }
    }
}
