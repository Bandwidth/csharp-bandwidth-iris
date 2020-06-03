using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Bandwidth.Iris.Model
{
    public class EmergencyNotification : BaseModel
    {
        public async static Task<EmergencyNotificationRecipientsResponse> ListRecipients(Client client, IDictionary<string, object> query = null)
        {
            var item = await client.MakeGetRequest<EmergencyNotificationRecipientsResponse>(client.ConcatAccountPath($"/emergencyNotificationRecipients"), query);
            return item;
        }

        public static Task<EmergencyNotificationRecipientsResponse> ListRecipients(IDictionary<string, object> query = null)
        {
            return ListRecipients(Client.GetInstance(), query);
        }

        public async static Task<EmergencyNotificationRecipientsResponse> GetRecipients(Client client, string orderId)
        {
            var item = await client.MakeGetRequest<EmergencyNotificationRecipientsResponse>(client.ConcatAccountPath($"/emergencyNotificationRecipients/{orderId}"));
            return item;
        }

        public static Task<EmergencyNotificationRecipientsResponse> GetRecipients(string orderId)
        {
            return GetRecipients(Client.GetInstance(), orderId);
        }

        public async static Task<EmergencyNotificationRecipientsResponse> CreateRecipients(Client client, EmergencyNotificationRecipient recipient)
        {
            var item = await client.MakePostRequest<EmergencyNotificationRecipientsResponse>(client.ConcatAccountPath($"/emergencyNotificationRecipients"), recipient);
            return item;
        }

        public static Task<EmergencyNotificationRecipientsResponse> CreateRecipients(EmergencyNotificationRecipient recipient)
        {
            return CreateRecipients(Client.GetInstance(), recipient);
        }

        public async static Task<EmergencyNotificationRecipientsResponse> UpdateRecipients(Client client, string id, EmergencyNotificationRecipient recipient)
        {
            var item = await client.MakePutRequest<EmergencyNotificationRecipientsResponse>(client.ConcatAccountPath($"/emergencyNotificationRecipients/{id}"), recipient);
            return item;
        }

        public static Task<EmergencyNotificationRecipientsResponse> UpdateRecipients(string id, EmergencyNotificationRecipient recipient)
        {
            return UpdateRecipients(Client.GetInstance(), id, recipient);
        }

        public async static Task DeleteRecipients(Client client, string id)
        {
            await client.MakeDeleteRequest(client.ConcatAccountPath($"/emergencyNotificationRecipients/{id}"));
        }

        public static Task DeleteRecipients(string id)
        {
            return DeleteRecipients(Client.GetInstance(), id);
        }

        public async static Task<EmergencyNotificationGroupOrderResponse> ListGroupOrders(Client client, IDictionary<string, object> query = null)
        {
            var item = await client.MakeGetRequest<EmergencyNotificationGroupOrderResponse>(client.ConcatAccountPath($"/emergencyNotificationGroupOrders"), query);
            return item;
        }

        public static Task<EmergencyNotificationGroupOrderResponse> ListGroupOrders(IDictionary<string, object> query = null)
        {
            return ListGroupOrders(Client.GetInstance(), query);
        }

        public async static Task<EmergencyNotificationGroupOrderResponse> GetGroupOrders(Client client, string id)
        {
            var item = await client.MakeGetRequest<EmergencyNotificationGroupOrderResponse>(client.ConcatAccountPath($"/emergencyNotificationGroupOrders/{id}"));
            return item;
        }

        public static Task<EmergencyNotificationGroupOrderResponse> GetGroupOrders(string id)
        {
            return GetGroupOrders(Client.GetInstance(), id);
        }

        public async static Task<EmergencyNotificationGroupOrderResponse> CreateGroupOrders(Client client, EmergencyNotificationGroupOrder order)
        {
            var item = await client.MakePostRequest<EmergencyNotificationGroupOrderResponse>(client.ConcatAccountPath($"/emergencyNotificationGroupOrders"), order);
            return item;
        }

        public static Task<EmergencyNotificationGroupOrderResponse> CreateGroupOrders(EmergencyNotificationGroupOrder order)
        {
            return CreateGroupOrders(Client.GetInstance(), order);
        }

        public async static Task<EmergencyNotificationEndpointOrderResponse> ListEndpointOrders(Client client, IDictionary<string, object> query = null)
        {
            var item = await client.MakeGetRequest<EmergencyNotificationEndpointOrderResponse>(client.ConcatAccountPath($"/emergencyNotificationEndpointOrders"), query);
            return item;
        }

        public static Task<EmergencyNotificationEndpointOrderResponse> ListEndpointOrders(IDictionary<string, object> query = null)
        {
            return ListEndpointOrders(Client.GetInstance(), query);
        }

        public async static Task<EmergencyNotificationGroupsResponse> GetGroups(Client client, string id)
        {
            var item = await client.MakeGetRequest<EmergencyNotificationGroupsResponse>(client.ConcatAccountPath($"/emergencyNotificationGroups/{id}"));
            return item;
        }

        public static Task<EmergencyNotificationGroupsResponse> GetGroups(string id)
        {
            return GetGroups(Client.GetInstance(), id);
        }

        public async static Task<EmergencyNotificationGroupsResponse> ListGroups(Client client, IDictionary<string, object> query = null)
        {
            var item = await client.MakeGetRequest<EmergencyNotificationGroupsResponse>(client.ConcatAccountPath($"/emergencyNotificationGroups"), query);
            return item;
        }

        public static Task<EmergencyNotificationGroupsResponse> ListGroups(IDictionary<string, object> query = null)
        {
            return ListGroups(Client.GetInstance(), query);
        }

        public async static Task<EmergencyNotificationEndpointOrderResponse> GetEndpointOrders(Client client, string id)
        {
            var item = await client.MakeGetRequest<EmergencyNotificationEndpointOrderResponse>(client.ConcatAccountPath($"/emergencyNotificationEndpointOrders/{id}"));
            return item;
        }

        public static Task<EmergencyNotificationEndpointOrderResponse> GetEndpointOrders(string id)
        {
            return GetEndpointOrders(Client.GetInstance(), id);
        }

        public async static Task<EmergencyNotificationEndpointOrder> CreateEndpointOrders(Client client, EmergencyNotificationEndpointOrder order)
        {
            var item = await client.MakePostRequest<EmergencyNotificationEndpointOrder>(client.ConcatAccountPath($"/emergencyNotificationEndpointOrders"), order);
            return item;
        }

        public static Task<EmergencyNotificationEndpointOrder> CreateEndpointOrders(EmergencyNotificationEndpointOrder order)
        {
            return CreateEndpointOrders(Client.GetInstance(), order);
        }


    }

    public class EmergencyNotificationRecipientsResponse
    {
        public Links Links { get; set; }
        public EmergencyNotificationRecipient EmergencyNotificationRecipient { get; set; }

        [XmlArray("EmergencyNotificationRecipients")]
        [XmlArrayItem("EmergencyNotificationRecipient")]
        public EmergencyNotificationRecipient[] EmergencyNotificationRecipients { get; set; }
    }

    public class EmergencyNotificationRecipient
    {
        public string Identifier { get; set; }
        public string CreatedDate { get; set; }
        public string LastModifiedDate { get; set; }
        public string ModifiedByUser { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string EmailAddress { get; set; }
        public Sms Sms { get; set; }
        public Tts Tts { get; set; }
        public Callback Callback { get; set; }

    }

    public class Callback
    {
        public string Url { get; set; }
        public Credentials Credentials { get; set; }

    }

    public class Credentials
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class Sms
    {
        public string TelephoneNumber { get; set; }
    }

    public class Tts
    {
        public string TelephoneNumber { get; set; }
    }

    public class EmergencyNotificationGroupOrderResponse
    {
        public Links Links { get; set; }

        [XmlArray("EmergencyNotificationGroupOrders")]
        [XmlArrayItem("EmergencyNotificationGroupOrder")]
        public EmergencyNotificationGroupOrder[] EmergencyNotificationGroupOrders { get; set; }
        public EmergencyNotificationGroup EmergencyNotificationGroup { get; set; }

    }

    public class EmergencyNotificationGroupsResponse
    {
        public Links Links { get; set; }

        [XmlArray("EmergencyNotificationGroups")]
        [XmlArrayItem("EmergencyNotificationGroup")]
        public EmergencyNotificationGroup[] EmergencyNotificationGroups { get; set; }
        public EmergencyNotificationGroup EmergencyNotificationGroup { get; set; }
    }

    public class EmergencyNotificationGroupOrder
    {
        public string CustomerOrderId { get; set; }

        [XmlElement("DeletedEmergencyNotificationGroup")]
        public EmergencyNotificationGroup DeletedEmergencyNotificationGroup { get; set; }

        [XmlElement("ModifiedEmergencyNotificationGroup")]
        public EmergencyNotificationGroup ModifiedEmergencyNotificationGroup { get; set; }

        [XmlElement("AddedEmergencyNotificationGroup")]
        public EmergencyNotificationGroup AddedEmergencyNotificationGroup { get; set; }


    }

    public class EmergencyNotificationGroup
    {
        public string Identifier { get; set; }
        public string Description { get; set; }
        public string CreatedDate { get; set; }
        public string OrderId { get; set; }
        public string OrderCreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string ProcessingStatus { get; set; }
        public string CustomerOrderId { get; set; }
        public string ModifiedBy { get; set; }
        public string ModifiedDate { get; set; }


        [XmlElement("AddedEmergencyNotificationGroup")]
        public EmergencyNotificationGroup AddedEmergencyNotificationGroup { get; set; }

        [XmlElement("DeletedEmergencyNotificationGroup")]
        public EmergencyNotificationGroup DeletedEmergencyNotificationGroup { get; set; }

        [XmlElement("ModifiedEmergencyNotificationGroup")]
        public EmergencyNotificationGroup ModifiedEmergencyNotificationGroup { get; set; }

        [XmlArray("DeletedEmergencyNotificationRecipients")]
        [XmlArrayItem("EmergencyNotificationRecipient")]
        public EmergencyNotificationRecipient[] DeletedEmergencyNotificationRecipients { get; set; }

        [XmlArray("AddedEmergencyNotificationRecipients")]
        [XmlArrayItem("EmergencyNotificationRecipient")]
        public EmergencyNotificationRecipient[] AddedEmergencyNotificationRecipients { get; set; }

        [XmlArray("EmergencyNotificationRecipients")]
        [XmlArrayItem("EmergencyNotificationRecipient")]
        public EmergencyNotificationRecipient[] EmergencyNotificationRecipients { get; set; }

    }

    public class EmergencyNotificationEndpointOrderResponse
    {
        public Links Links { get; set; }

        [XmlArray("EmergencyNotificationEndpointOrders")]
        [XmlArrayItem("EmergencyNotificationEndpointOrder")]
        public EmergencyNotificationEndpointOrder[] EmergencyNotificationEndpointOrders { get; set; }
        public EmergencyNotificationEndpointOrder EmergencyNotificationEndpointOrder { get; set; }
    }

    public class EmergencyNotificationEndpointOrder
    {
        public string Identifier { get; set; }
        public string Description { get; set; }
        public string OrderId { get; set; }
        public string OrderCreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string ProcessingStatus { get; set; }
        public string CustomerOrderId { get; set; }

        public EmergencyNotificationEndpointAssociations EmergencyNotificationEndpointAssociations { get; set; }
    }

    public class EmergencyNotificationEndpointAssociations
    {
        public EmergencyNotificationGroup EmergencyNotificationGroup { get; set; }

        [XmlArray("AddedAssociations")]
        [XmlArrayItem("EepToEngAssociations")]
        public EepToEngAssociations[] AddedEepToEngAssociations { get; set; }

        [XmlArray("DeletedAssociations")]
        [XmlArrayItem("EepToEngAssociations")]
        public EepToEngAssociations[] DeletedEepToEngAssociations { get; set; }


    }

    public class EepToEngAssociations
    {
        [XmlArray("EepTns")]
        [XmlArrayItem("TelephoneNumber")]
        public string[] EepTns { get; set; }

        [XmlArray("EepAeuiIds")]
        [XmlArrayItem("Identifier")]
        public string[] EepAeuiIds { get; set; }
    }


}
