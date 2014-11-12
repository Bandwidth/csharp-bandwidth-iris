using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Bandwidth.Iris
{
    public sealed class Client
    {
        private readonly string _accountId;
        private readonly string _siteId;
        private readonly string _userName;
        private readonly string _password;
        private readonly string _apiEndpoint;
        private readonly string _apiVersion;

        public static Client GetInstance(string accountId, string siteId, string userName, string password, string apiEndpoint = "https://api.catapult.inetwork.com", string apiVersion = "v1")
        {
            return new Client(accountId, siteId, userName, password, apiEndpoint, apiVersion);
        }

        
#if !PCL
        public const string BandwidthUserId = "BANDWIDTH_USER_ID";
        public const string BandwidthApiAccountId = "BANDWIDTH_API_ACCOUNT_ID";
        public const string BandwidthApiSiteId = "BANDWIDTH_API_SITE_ID";
        public const string BandwidthApiUserName = "BANDWIDTH_API_USERNAME";
        public const string BandwidthApiPassword = "BANDWIDTH_API_PASSWORD";
        public const string BandwidthApiEndpoint = "BANDWIDTH_API_ENDPOINT";
        public const string BandwidthApiVersion = "BANDWIDTH_API_VERSION";

        public static Client GetInstance()
        {
            return GetInstance(
                Environment.GetEnvironmentVariable(BandwidthApiAccountId),
                Environment.GetEnvironmentVariable(BandwidthApiSiteId),
                Environment.GetEnvironmentVariable(BandwidthApiUserName),
                Environment.GetEnvironmentVariable(BandwidthApiPassword),
                Environment.GetEnvironmentVariable(BandwidthApiEndpoint),
                Environment.GetEnvironmentVariable(BandwidthApiVersion));
        }

        
#endif
        private Client(string accountId, string siteId, string userName, string password, string apiEndpoint, string apiVersion)
        {
            if (accountId == null) throw new ArgumentNullException("accountId");
            if (siteId == null) throw new ArgumentNullException("siteId");
            if (userName == null) throw new ArgumentNullException("userName");
            if (password == null) throw new ArgumentNullException("password");
            if (apiEndpoint == null) throw new ArgumentNullException("apiEndpoint");
            if (apiVersion == null) throw new ArgumentNullException("apiVersion");
            _accountId = accountId;
            _siteId = siteId;
            _userName = userName;
            _password = password;
            _apiEndpoint = apiEndpoint;
            _apiVersion = apiVersion;

        }

        private HttpClient CreateHttpClient()
        {
            var url = new UriBuilder(_apiEndpoint) { Path = string.Format("/{0}/", _apiVersion) };
            var client = new HttpClient { BaseAddress = url.Uri };
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Basic",
                    Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Format("{0}:{1}", _userName, _password))));
            return client;
        }

        #region Base Http methods

        internal async Task<HttpResponseMessage> MakeGetRequest(string path, IDictionary<string, object> query = null,
            string id = null, bool disposeResponse = false)
        {
            var urlPath = FixPath(path);
            if (id != null)
            {
                urlPath = urlPath + "/" + id;
            }
            if (query != null && query.Count > 0)
            {
                urlPath = string.Format("{0}?{1}", urlPath,
                    string.Join("&",
                        from p in query select string.Format("{0}={1}", p.Key, Uri.EscapeDataString(Convert.ToString(p.Value)))));
            }
            using (var client = CreateHttpClient())
            {
                var response = await client.GetAsync(urlPath);
                try
                {
                    await CheckResponse(response);
                }
                catch
                {
                    response.Dispose();
                    throw;
                }
                if (!disposeResponse) return response;
                response.Dispose();
                return null;
            }
        }


        internal async Task<TResult> MakeGetRequest<TResult>(string path, IDictionary<string, object> query = null,
            string id = null)
        {
            using (var response = await MakeGetRequest(path, query, id))
            {
                if (response.Content.Headers.ContentType != null &&
                    response.Content.Headers.ContentType.MediaType == "text/xml")
                {
                    var stream = await response.Content.ReadAsStreamAsync();
                    var serializer = new XmlSerializer(typeof(TResult));
                    return stream.Length > 0
                        ? (TResult)serializer.Deserialize(stream)
                        : default(TResult);
                }
            }
            return default(TResult);
        }

        

        internal async Task<HttpResponseMessage> MakePostRequest(string path, object data, bool disposeResponse = false)
        {
            var serializer = new XmlSerializer(data.GetType());
            using (var writer = new StringWriter())
            {
                serializer.Serialize(writer, data);
                var xml = writer.ToString();
                using (var client = CreateHttpClient())
                {
                    var response =
                        await client.PostAsync(FixPath(path), new StringContent(xml, Encoding.UTF8, "text/xml"));
                    try
                    {
                        await CheckResponse(response);
                    }
                    catch
                    {
                        response.Dispose();
                        throw;
                    }
                    if (!disposeResponse) return response;
                    response.Dispose();
                    return null;
                }
            }
        }

        internal async Task<HttpResponseMessage> PutData(string path, Stream stream, string mediaType,
            bool disposeResponse = false)
        {
            if (stream == null) throw new ArgumentNullException("stream");
            return await PutFileContent(path, mediaType, disposeResponse, new StreamContent(stream));
        }

        internal async Task<HttpResponseMessage> PutData(string path, byte[] buffer, string mediaType,
            bool disposeResponse = false)
        {
            if (buffer == null) throw new ArgumentNullException("buffer");
            return await PutFileContent(path, mediaType, disposeResponse, new ByteArrayContent(buffer));
        }

        private async Task<HttpResponseMessage> PutFileContent(string path, string mediaType, bool disposeResponse,
            HttpContent content)
        {
            if (mediaType != null)
            {
                content.Headers.ContentType = new MediaTypeHeaderValue(mediaType);
            }
            using (var client = CreateHttpClient())
            {
                var response = await client.PutAsync(FixPath(path), content);
                try
                {
                    await CheckResponse(response);
                }
                catch
                {
                    response.Dispose();
                    throw;
                }
                if (!disposeResponse) return response;
                response.Dispose();
                return null;
            }
        }


        internal async Task<TResult> MakePostRequest<TResult>(string path, object data)
        {
            using (var response = await MakePostRequest(path, data))
            {
                if (response.Content.Headers.ContentType != null &&
                    response.Content.Headers.ContentType.MediaType == "text/xml")
                {
                    var stream = await response.Content.ReadAsStreamAsync();
                    var serializer = new XmlSerializer(typeof(TResult));
                    return stream.Length > 0
                        ? (TResult)serializer.Deserialize(stream)
                        : default(TResult);
                }
            }
            return default(TResult);
        }

        internal async Task MakeDeleteRequest(string path, string id = null)
        {
            if (id != null)
            {
                path = path + "/" + id;
            }
            using (var client = CreateHttpClient())
            using (var response = await client.DeleteAsync(FixPath(path)))
            {
                await CheckResponse(response);
            }
        }

        #endregion


        
        private static string FixPath(string path)
        {
            if (string.IsNullOrEmpty(path)) throw new ArgumentNullException("path");
            return (path[0] == '/') ? path.Substring(1) : path;
        }

        private async Task CheckResponse(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                try
                {
                    var xml = await response.Content.ReadAsStringAsync();
                    var doc = XDocument.Parse(xml);
                    var code = doc.Descendants("ErrorCode").FirstOrDefault();
                    var description = doc.Descendants("Description").FirstOrDefault();
                    if (code != null && description != null)
                    {
                        throw  new BandwidthIrisException(code.Value, description.Value, response.StatusCode);
                    }

                }
                catch(Exception ex)
                {
                    if (ex is BandwidthIrisException) throw;
                    Debug.WriteLine(ex.Message);
                }
                throw new BandwidthIrisException("", string.Format("Http code {0}", response.StatusCode), response.StatusCode);
            }
        }
    }
}