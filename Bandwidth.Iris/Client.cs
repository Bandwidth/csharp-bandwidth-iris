using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Bandwidth.Iris
{
    public sealed class Client
    {
        public static readonly string USER_AGENT = $"csharp-bandwidth-iris-{Assembly.GetAssembly(typeof(Bandwidth.Iris.Client)).GetName().Version.ToString()}";

        private readonly string _userName;
        private readonly string _password;
        private readonly string _apiEndpoint;
        private readonly string _apiVersion;
        private readonly string _accountPath;

        public static Client GetInstance(string accountId, string userName, string password, string apiEndpoint = "https://dashboard.bandwidth.com", string apiVersion = "v1.0")
        {
            return new Client(accountId, userName, password, apiEndpoint, apiVersion);
        }

        
#if !PCL
        public const string BandwidthApiAccountId = "BANDWIDTH_API_ACCOUNT_ID";
        public const string BandwidthApiUserName = "BANDWIDTH_API_USERNAME";
        public const string BandwidthApiPassword = "BANDWIDTH_API_PASSWORD";
        public const string BandwidthApiEndpoint = "BANDWIDTH_API_ENDPOINT";
        public const string BandwidthApiVersion = "BANDWIDTH_API_VERSION";

        public static Client GetInstance()
        {
            return GetInstance(
                Environment.GetEnvironmentVariable(BandwidthApiAccountId),
                Environment.GetEnvironmentVariable(BandwidthApiUserName),
                Environment.GetEnvironmentVariable(BandwidthApiPassword),
                Environment.GetEnvironmentVariable(BandwidthApiEndpoint),
                Environment.GetEnvironmentVariable(BandwidthApiVersion));
        }

        
#endif
        private Client(string accountId, string userName, string password, string apiEndpoint, string apiVersion)
        {
            if (accountId == null) throw new ArgumentNullException("accountId");
            if (userName == null) throw new ArgumentNullException("userName");
            if (password == null) throw new ArgumentNullException("password");
            if (apiEndpoint == null) throw new ArgumentNullException("apiEndpoint");
            if (apiVersion == null) throw new ArgumentNullException("apiVersion");
            _userName = userName;
            _password = password;
            _apiEndpoint = apiEndpoint;
            _apiVersion = apiVersion;
            _accountPath = string.Format("accounts/{0}", accountId);
        }

        private HttpClient CreateHttpClient()
        {
            var url = new UriBuilder(_apiEndpoint) { Path = string.Format("/{0}/", _apiVersion) };
            var client = new HttpClient(new HttpClientHandler { AllowAutoRedirect = false }) { BaseAddress = url.Uri };
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Basic",
                    Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Format("{0}:{1}", _userName, _password))));
            client.DefaultRequestHeaders.Add("User-Agent", USER_AGENT);
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
                if (response.StatusCode == HttpStatusCode.SeeOther)
                {
                    var location = response.Headers.Location.PathAndQuery;
                    var index = location.IndexOf("/", 1, StringComparison.Ordinal);
                    if (index >= 0)
                    {
                        location = location.Substring(index);
                    }
                    index = location.IndexOf("?", StringComparison.Ordinal);
                    var q = new Dictionary<string, object>();
                    if (index > 0)
                    {
                        var d = location.Substring(index + 1);
                        foreach (var pair in d.Split('&'))
                        {
                            var values = pair.Split('=');
                            q.Add(values[0], Uri.UnescapeDataString(values[1]));
                        }
                        location = location.Substring(0, index);
                    }

                    response.Dispose();
                    return await MakeGetRequest(location, q, id, disposeResponse);
                }
                try
                {
                    await CheckResponse(response);
                }
                catch(Exception ex)
                {
                    response.Dispose();
                    throw ex;
                }
                if (!disposeResponse) return response;
                response.Dispose();
                return null;
            }
        }


        public async Task<TResult> MakeGetRequest<TResult>(string path, IDictionary<string, object> query = null,
            string id = null)
        {
            using (var response = await MakeGetRequest(path, query, id))
            {
                using (var stream = await response.Content.ReadAsStreamAsync())
                {
                    if (typeof(TResult).Equals(typeof(Stream)))
                    {
                        var returnStream = new MemoryStream();
                        stream.CopyTo(returnStream);

                        return (TResult)(object)returnStream;
                    }

                    var serializer = new XmlSerializer(typeof(TResult));
                    return stream.Length > 0
                        ? (TResult)serializer.Deserialize(stream)
                        : default(TResult);
                }
            }
        }



        public async Task<HttpResponseMessage> MakePostRequest(string path, object data, bool disposeResponse = false)
        {
            var serializer = new XmlSerializer(data.GetType());
            using (var writer = new Utf8StringWriter())
            {
                serializer.Serialize(writer, data);
                var xml = writer.ToString();
                using (var client = CreateHttpClient())
                {
                    var response =
                        await client.PostAsync(FixPath(path), new StringContent(xml, Encoding.UTF8, "application/xml"));
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

        public async Task<HttpResponseMessage> MakePatchRequest(string path, object data, bool disposeResponse = false)
        {
            var serializer = new XmlSerializer(data.GetType());
            using (var writer = new Utf8StringWriter())
            {
                serializer.Serialize(writer, data);
                var xml = writer.ToString();

                HttpContent content = new StringContent(xml, Encoding.UTF8, "application/xml");

                

                using (var client = CreateHttpClient())
                {

                    var request = new HttpRequestMessage
                    {
                        Method = new HttpMethod("PATCH"),
                        Content = content,
                        RequestUri = new Uri(client.BaseAddress + FixPath(path))
                    };

                    var response =
                        await client.SendAsync(request);
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

        internal async Task<HttpResponseMessage> MakePutRequest(string path, object data, bool disposeResponse = false)
        {
            
            using (var writer = new Utf8StringWriter())
            {
                var payload = "";
                if (data is string)
                {
                    payload = (string)data;
                }
                    
                else if ( !(typeof(Stream)).IsAssignableFrom(data.GetType()))
                {
                    var serializer = new XmlSerializer(data.GetType());
                    serializer.Serialize(writer, data);
                    payload = writer.ToString();
                }

                using (var client = CreateHttpClient())
                {
                    if ( (typeof(Stream)).IsAssignableFrom(data.GetType()) ) {
                        return await client.PutAsync(FixPath(path), new StreamContent((Stream)data));
                    }

                    var response =  await client.PutAsync(FixPath(path), new StringContent(payload, Encoding.UTF8, "application/xml"));
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

        internal async Task<HttpResponseMessage> SendData(string path, Stream stream, string mediaType, string method = "POST",
            bool disposeResponse = false)
        {
            if (stream == null) throw new ArgumentNullException("stream");
            return await SendFileContent(path, mediaType, disposeResponse, new StreamContent(stream), method);
        }

        internal async Task<HttpResponseMessage> SendData(string path,  byte[] buffer, string mediaType, string method = "POST",
            bool disposeResponse = false)
        {
            if (buffer == null) throw new ArgumentNullException("buffer");
            return await SendFileContent(path, mediaType, disposeResponse, new ByteArrayContent(buffer), method);
        }

        private async Task<HttpResponseMessage> SendFileContent(string path, string mediaType, bool disposeResponse,
            HttpContent content, string method)
        {
            if (mediaType != null)
            {
                content.Headers.ContentType = new MediaTypeHeaderValue(mediaType);
            }
            using (var client = CreateHttpClient())
            {
                var makeAction = ((method == "PUT") ? client.PutAsync : new Func<string, HttpContent, Task<HttpResponseMessage>>(client.PostAsync));
                var response = await makeAction(FixPath(path), content);
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


        public async Task<TResult> MakePostRequest<TResult>(string path, object data)
        {
            using (var response = await MakePostRequest(path, data))
            {
                using (var stream = await response.Content.ReadAsStreamAsync())
                {
                    var serializer = new XmlSerializer(typeof(TResult));
                    return stream.Length > 0
                        ? (TResult)serializer.Deserialize(stream)
                        : default(TResult);
                }
            }
        }

        public async Task<TResult> MakePutRequest<TResult>(string path, object data)
        {
            using (var response = await MakePutRequest(path, data))
            {
                using (var stream = await response.Content.ReadAsStreamAsync())
                {
                    if (typeof(TResult).IsAssignableFrom(typeof(Stream)))
                    {
                        var returnStream = new MemoryStream();
                        stream.CopyTo(returnStream);

                        return (TResult)(object)returnStream;
                    }

                    var serializer = new XmlSerializer(typeof(TResult));
                    return stream.Length > 0
                        ? (TResult)serializer.Deserialize(stream)
                        : default(TResult);
                }
            }
        }

        public async Task<TResult> MakePatchRequest<TResult>(string path, object data)
        {
            using (var response = await MakePatchRequest(path, data)) 
            {
                using (var stream = await response.Content.ReadAsStreamAsync())
                {
                    var serializer = new XmlSerializer(typeof(TResult));
                    return stream.Length > 0
                        ? (TResult)serializer.Deserialize(stream)
                        : default(TResult);
                }
            }
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

        internal async Task<HttpResponseMessage> MakeDeleteRequestWithResponse(string path, string id = null)
        {
            if (id != null)
            {
                path = path + "/" + id;
            }
            using (var client = CreateHttpClient())
            using (var response = await client.DeleteAsync(FixPath(path)))
            {
                await CheckResponse(response);
                return response;
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
            try
            {
                var xml = await response.Content.ReadAsStringAsync();
                if (xml.Length > 0)
                {
                    var doc = XDocument.Parse(xml);
                    var code = doc.Descendants("ErrorCode").FirstOrDefault();
                    var description = doc.Descendants("Description").FirstOrDefault();
                    if (code == null)
                    {
                        var error = doc.Descendants("Error").FirstOrDefault();
                        if (error == null)
                        {
                            var exceptions =
                                (from item in doc.Descendants("Errors")
                                    select
                                        (Exception) new BandwidthIrisException(item.Element("Code").Value,
                                            item.Element("Description").Value, response.StatusCode)).ToArray();
                            if (exceptions.Length > 0)
                            {
                                throw new AggregateException(exceptions);
                            }
                            code = doc.Descendants("resultCode").FirstOrDefault();
                            description = doc.Descendants("resultMessage").FirstOrDefault();
                        }
                        else
                        {
                            code = error.Element("Code");
                            description = error.Element("Description");    
                        }
                    }
                    if (code != null && description != null && !string.IsNullOrEmpty(code.Value) && code.Value != "0")
                    {
                        throw new BandwidthIrisException(code.Value, description.Value, response.StatusCode, doc);
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex is BandwidthIrisException || ex is AggregateException) throw;
                Debug.WriteLine(ex.Message);
            }
            if (!response.IsSuccessStatusCode)
            {
                throw new BandwidthIrisException("", string.Format("Http code {0}", response.StatusCode), response.StatusCode);
            }
        }

        internal string GetIdFromLocationHeader(Uri locationHeader)
        {
            if (locationHeader == null)
            {
                throw new Exception("Missing location header in response");
            }
            var location = locationHeader.ToString();
            var index = location.LastIndexOf("/", StringComparison.Ordinal);
            if (index < 0)
            {
                throw new Exception("Missing id in response");
            }
            return location.Substring(index + 1);
        }

        public string ConcatAccountPath(string path)
        {
            if (string.IsNullOrEmpty(path))
                return _accountPath;
            if (path[0] == '/')
            {
                return _accountPath + path;
            }
            return string.Format("{0}/{1}", _accountPath, path);
        }
    }
}
