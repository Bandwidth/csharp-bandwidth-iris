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
    public class Application : BaseModel
    {
        public string ApplicationId { get; set; }
        public string ServiceType { get; set; }
        public string AppName { get; set; }
        public string MsgCallbackUrl { get; set; }
        public CallbackCreds CallbackCreds { get; set; }
        public string CallStatusMethod { get; set; }
        public string CallInitiatedMethod { get; set; }
        public string CallInitiatedCallbackUrl { get; set; }
        public string CallStatusCallbackUrl { get; set; }


        private static string applicatoinPath = "applications";
        private static string associatedSipppeersPath = "associatedsippeers";

        public async static Task<ApplicationProvisioningResponse> List(Client client)
        {
            var item = await client.MakeGetRequest<ApplicationProvisioningResponse>(client.ConcatAccountPath(applicatoinPath));
            item.Client = client;
            return item;
        }

        public async static Task<ApplicationProvisioningResponse> Create(Client client, Application application)
        {
            var item = await client.MakePostRequest<ApplicationProvisioningResponse>(client.ConcatAccountPath(applicatoinPath), application);
            item.Client = client;
            return item;
        }

        public static Task<ApplicationProvisioningResponse> Create(Application application)
        {
            return Create(Client.GetInstance(), application);
        }

        public async static Task<ApplicationProvisioningResponse> Get(Client client, string applicationId)
        {
            var item = await client.MakeGetRequest<ApplicationProvisioningResponse>(client.ConcatAccountPath($"{applicatoinPath}/{applicationId}") );
            item.Client = client;
            return item;
        }

        public static Task<ApplicationProvisioningResponse> Get(string applicationId)
        {
            return Get(Client.GetInstance(), applicationId);
        }

        public async static Task<ApplicationProvisioningResponse> PartialUpdate(Client client, string applicationId, Application application )
        {
            //TODO need Patch
            var item = await client.MakePatchRequest<ApplicationProvisioningResponse>(client.ConcatAccountPath($"{applicatoinPath}/{applicationId}"), application);
            item.Client = client;
            return item;
        }

        public async static Task<ApplicationProvisioningResponse> FullUpdate(Client client, string applicationId, Application application)
        {
            var item = await client.MakePutRequest<ApplicationProvisioningResponse>(client.ConcatAccountPath($"{applicatoinPath}/{applicationId}"), application);
            item.Client = client;
            return item;
        }

        public static Task<ApplicationProvisioningResponse> FullUpdate(string applicationId, Application application)
        {
            return FullUpdate(Client.GetInstance(), applicationId, application);
        }

        public async static Task<HttpResponseMessage> Delete(Client client, string applicationId)
        {
            var item = await client.MakeDeleteRequestWithResponse(client.ConcatAccountPath($"{applicatoinPath}/{applicationId}"));
            return item;
        }

        public static Task<HttpResponseMessage> Delete(string applicationId)
        {
            return Delete(Client.GetInstance(), applicationId);
        }

        public async static Task<AssociatedSipPeersResponse> ListAssociatedSippeers(Client client, string applicationId)
        { 
            var item = await client.MakeGetRequest<AssociatedSipPeersResponse>(client.ConcatAccountPath($"{applicatoinPath}/{applicationId}/{associatedSipppeersPath}"));
            item.Client = client;
            return item;
        }
    }

    public class ApplicationProvisioningResponse : BaseModel
    {
        public Application[] ApplicationList { get; set; }
        public Application Application { get; set; }
    }

    public class CallbackCreds
    {
        public string UserId { get; set; }
        public string Password { get; set; }
    }

    public class AssociatedSipPeer
    {
        public string SiteId { get; set; }
        public string SiteName { get; set; }
        public string PeerId { get; set; }
        public string PeerName { get; set; }
    }

    public class AssociatedSipPeersResponse : BaseModel
    {
        public AssociatedSipPeer[] AssociatedSipPeers { get; set; }
        public ResponseStatus ResponseStatus { get; set; }
    }

    public class ResponseStatus
    {
        public string ErrorCode { get; set; }
        public string Description { get; set; }
    }





}
