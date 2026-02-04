using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Bandwidth.Iris.Tests
{
    public class ClientAuthTests
    {
        [Fact]
        public async Task UsesBearerHeaderWhenAccessTokenProvided()
        {
            var token = "abc123";
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = "/v1.0/test",
                EstimatedHeaders = new Dictionary<string, string>
                {
                    {"Authorization", "Bearer " + token}
                }
            }))
            {
                var client = Client.GetInstanceWithAccessToken(Helper.AccountId, token, apiEndpoint: "http://localhost:3001/");
                await client.MakeGetRequest("test", null, null, true);
                if (server.Error != null) throw server.Error;
            }
        }

        [Fact]
        public async Task UsesBearerHeaderWhenValidEvenWithClientCredentials()
        {
            var clientId = "FakeClientId";
            var clientSecret = "FakeClientSecret";
            var token = "validToken";
            var tokenExpiration = DateTimeOffset.UtcNow.AddHours(1);
            var tokenRequestAuthString = "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(clientId + ":" + clientSecret));

            using (var apiServer = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = "/v1.0/test",
                EstimatedHeaders = new Dictionary<string, string>
                {
                    {"Authorization", "Bearer " + token}
                }
            }))
            {
                var client = Client.GetInstanceWithClientCredentialsAndAccessToken(Helper.AccountId, clientId, clientSecret, token, tokenExpiration, "http://localhost:3001/");
                await client.MakeGetRequest("test", null, null, true);
                if (apiServer.Error != null) throw apiServer.Error;
            }


        }

        [Fact]
        public async Task RefreshesTokenWithinOneMinuteSkew()
        {
            var clientId = "FakeClientId";
            var clientSecret = "FakeClientSecret";
            var tokenRequestAuthString = "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(clientId + ":" + clientSecret));
            // Token endpoint server on port 3002: first call returns tk1 (short expiry), second returns tk2
            using (var tokenServer = new HttpServer(new[] {
                new RequestHandler
                {
                    EstimatedMethod = "POST",
                    EstimatedPathAndQuery = "/",
                    EstimatedHeaders = new Dictionary<string, string>
                    {
                        {"Authorization", tokenRequestAuthString}
                    },
                    ContentToSend = new StringContent("{\"access_token\":\"tk1\",\"expires_in\":30}", Encoding.UTF8, "application/json")
                },
                new RequestHandler
                {
                    EstimatedMethod = "POST",
                    EstimatedPathAndQuery = "/",
                    EstimatedHeaders = new Dictionary<string, string>
                    {
                        {"Authorization", tokenRequestAuthString}
                    },
                    ContentToSend = new StringContent("{\"access_token\":\"tk2\",\"expires_in\":3600}", Encoding.UTF8, "application/json")
                }
            }, prefix: "http://localhost:3002/"))
            {
                // API server expects first GET with tk1, second GET with tk2
                using (var apiServer = new HttpServer(new[] {
                    new RequestHandler
                    {
                        EstimatedMethod = "GET",
                        EstimatedPathAndQuery = "/v1.0/test",
                        EstimatedHeaders = new Dictionary<string, string>
                        {
                            {"Authorization", "Bearer tk1"}
                        }
                    },
                    new RequestHandler
                    {
                        EstimatedMethod = "GET",
                        EstimatedPathAndQuery = "/v1.0/test",
                        EstimatedHeaders = new Dictionary<string, string>
                        {
                            {"Authorization", "Bearer tk2"}
                        }
                    }
                }))
                {
                    var client = Client.GetInstanceWithClientCredentials(Helper.AccountId, clientId, clientSecret, "http://localhost:3002/");

                    await client.MakeGetRequest("test", null, null, true);
                    if (apiServer.Error != null) throw apiServer.Error;

                    // Second call should see short expiry (<= 1 minute) and refresh to tk2
                    await Task.Delay(100); // small delay to avoid race conditions
                    await client.MakeGetRequest("test", null, null, true);
                    if (apiServer.Error != null) throw apiServer.Error;
                }
            }
        }

        [Fact]
        public async Task UsesBasicAuthWhenNoOauthSupplied()
        {
            var userName = "user1";
            var password = "pass123";
            var basicAuthString = "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(userName + ":" + password));
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = "/v1.0/test",
                EstimatedHeaders = new Dictionary<string, string>
                {
                    {"Authorization", basicAuthString}
                }
            }))
            {
                var client = Client.GetInstance(Helper.AccountId, userName, password, apiEndpoint: "http://localhost:3001/");
                await client.MakeGetRequest("test", null, null, true);
                if (server.Error != null) throw server.Error;
            }
        }
    }
}
