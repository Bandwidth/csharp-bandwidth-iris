using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Bandwidth.Iris.Model;
using Xunit;

namespace Bandwidth.Iris.Tests.Models
{

    public class EmergencyNotificationTests
    {
        // [TestInitialize]
        public EmergencyNotificationTests()
        {
            Helper.SetEnvironmetVariables();
        }

        [Fact]
        public void TestGetRecipients()
        {

            string orderId = "1234";

            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = $"/v1.0/accounts/{Helper.AccountId}/emergencyNotificationRecipients/{orderId}",
                ContentToSend = new StringContent(TestXmlStrings.getRecipients, Encoding.UTF8, "application/xml")
            }))
            {
                var client = Helper.CreateClient();
                var result = EmergencyNotification.GetRecipients(client, orderId).Result;
                if (server.Error != null) throw server.Error;

                Assert.NotNull(result);
                Assert.Equal(" 63865500-0904-46b1-9b4f-7bd237a26363 ", result.EmergencyNotificationRecipient.Identifier);
                Assert.Equal("2020-03-18T21:26:47.403Z", result.EmergencyNotificationRecipient.CreatedDate);
                Assert.Equal("2020-04-01T18:32:22.316Z", result.EmergencyNotificationRecipient.LastModifiedDate);
                Assert.Equal("jgilmore", result.EmergencyNotificationRecipient.ModifiedByUser);
                Assert.Equal(" This is a description of the emergency notification recipient. ", result.EmergencyNotificationRecipient.Description);
                Assert.Equal("CALLBACK", result.EmergencyNotificationRecipient.Type);

                Assert.Equal("https://foo.bar/baz", result.EmergencyNotificationRecipient.Callback.Url);
                Assert.Equal("jgilmore", result.EmergencyNotificationRecipient.Callback.Credentials.Username);

                Assert.Equal("fred@gmail.com", result.EmergencyNotificationRecipient.EmailAddress);

                Assert.Equal("12015551212", result.EmergencyNotificationRecipient.Sms.TelephoneNumber);

                Assert.Equal("12015551212", result.EmergencyNotificationRecipient.Tts.TelephoneNumber);

            }
        }

        [Fact]
        public void TestListRecipients()
        {
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = $"/v1.0/accounts/{Helper.AccountId}/emergencyNotificationRecipients?EnrNotificationType=SMS",
                ContentToSend = new StringContent(TestXmlStrings.listRecipients, Encoding.UTF8, "application/xml")
            }))
            {
                var client = Helper.CreateClient();
                var result = EmergencyNotification.ListRecipients(client, new Dictionary<string, Object>
                {
                    {"EnrNotificationType", "SMS" }
                }).Result;
                if (server.Error != null) throw server.Error;

                Assert.NotNull(result.EmergencyNotificationRecipients);
                Assert.Equal(4, result.EmergencyNotificationRecipients.Length);
                Assert.NotNull(result.Links);
            }
        }

        [Fact]
        public void TestCreateRecipients()
        {


            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "POST",
                EstimatedPathAndQuery = $"/v1.0/accounts/{Helper.AccountId}/emergencyNotificationRecipients",
                ContentToSend = new StringContent(TestXmlStrings.getRecipients, Encoding.UTF8, "application/xml")
            }))
            {
                var client = Helper.CreateClient();
                var result = EmergencyNotification.CreateRecipients(client, new EmergencyNotificationRecipient
                {
                    ModifiedByUser = "testuser",
                    Type = "EMAIL",
                    EmailAddress = "test@example.com"
                }).Result;
                if (server.Error != null) throw server.Error;

                Assert.NotNull(result);

            }
        }

        [Fact]
        public void TestUpdateRecipients()
        {
            string id = "123";


            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "PUT",
                EstimatedPathAndQuery = $"/v1.0/accounts/{Helper.AccountId}/emergencyNotificationRecipients/{id}",
                ContentToSend = new StringContent(TestXmlStrings.getRecipients, Encoding.UTF8, "application/xml")
            }))
            {
                var client = Helper.CreateClient();
                var result = EmergencyNotification.UpdateRecipients(client, id, new EmergencyNotificationRecipient
                {
                    ModifiedByUser = "testuser",
                    Type = "EMAIL",
                    EmailAddress = "test@example.com"
                }).Result;
                if (server.Error != null) throw server.Error;

                Assert.NotNull(result);

            }
        }

        [Fact]
        public void TestDeleteRecipients()
        {
            string id = "123";


            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "DELETE",
                EstimatedPathAndQuery = $"/v1.0/accounts/{Helper.AccountId}/emergencyNotificationRecipients/{id}",
            }))
            {
                var client = Helper.CreateClient();
                EmergencyNotification.DeleteRecipients(client, id).Wait();



            }
        }

        [Fact]
        public void TestGetGroupOrder()
        {
            string id = "123";


            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = $"/v1.0/accounts/{Helper.AccountId}/emergencyNotificationGroupOrders/{id}",
                ContentToSend = new StringContent(TestXmlStrings.getGroupOrders, Encoding.UTF8, "application/xml")
            }))
            {
                var client = Helper.CreateClient();
                var result = EmergencyNotification.GetGroupOrders(client, id).Result;
                if (server.Error != null) throw server.Error;

                Assert.NotNull(result);
                Assert.Equal("3e9a852e-2d1d-4e2d-84c3-87223a78cb70", result.EmergencyNotificationGroup.OrderId);
                Assert.Equal("2020-01-23T18:34:17.284Z", result.EmergencyNotificationGroup.OrderCreatedDate);
                Assert.Equal("jgilmore", result.EmergencyNotificationGroup.CreatedBy);
                Assert.Equal("COMPLETED", result.EmergencyNotificationGroup.ProcessingStatus);
                Assert.Equal("ALG-31233884", result.EmergencyNotificationGroup.CustomerOrderId);
                Assert.NotNull(result.EmergencyNotificationGroup.AddedEmergencyNotificationGroup);

            }
        }

        [Fact]
        public void TestListGroupOrder()
        {
            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = $"/v1.0/accounts/{Helper.AccountId}/emergencyNotificationGroupOrders?EnrNotificationType=TTS",
                ContentToSend = new StringContent(TestXmlStrings.listGroupOrders, Encoding.UTF8, "application/xml")
            }))
            {
                var client = Helper.CreateClient();
                var result = EmergencyNotification.ListGroupOrders(client, new Dictionary<string, Object>
                {
                    {"EnrNotificationType", "TTS" }
                }).Result;
                if (server.Error != null) throw server.Error;

                Assert.NotNull(result);
                Assert.NotNull( result.Links);
                Assert.NotNull(result.EmergencyNotificationGroupOrders);
                Assert.Equal(3, result.EmergencyNotificationGroupOrders.Length);

            }
        }

        [Fact]
        public void TestCreatGroupOrder()
        {


            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "POST",
                EstimatedPathAndQuery = $"/v1.0/accounts/{Helper.AccountId}/emergencyNotificationGroupOrders",
                ContentToSend = new StringContent(TestXmlStrings.getGroupOrders, Encoding.UTF8, "application/xml")
            }))
            {
                var client = Helper.CreateClient();
                var result = EmergencyNotification.CreateGroupOrders(client, new EmergencyNotificationGroupOrder
                {
                    CustomerOrderId = "test",
                    AddedEmergencyNotificationGroup = new EmergencyNotificationGroup
                    {
                        CreatedBy = "samwise"
                    }
                }).Result;
                if (server.Error != null) throw server.Error;

                Assert.NotNull(result);
                Assert.Equal("3e9a852e-2d1d-4e2d-84c3-87223a78cb70", result.EmergencyNotificationGroup.OrderId);
                Assert.Equal("2020-01-23T18:34:17.284Z", result.EmergencyNotificationGroup.OrderCreatedDate);
                Assert.Equal("jgilmore", result.EmergencyNotificationGroup.CreatedBy);
                Assert.Equal("COMPLETED", result.EmergencyNotificationGroup.ProcessingStatus);
                Assert.Equal("ALG-31233884", result.EmergencyNotificationGroup.CustomerOrderId);
                Assert.NotNull(result.EmergencyNotificationGroup.AddedEmergencyNotificationGroup);

            }
        }

        [Fact]
        public void TestGetGroups()
        {
            string id = "123";


            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = $"/v1.0/accounts/{Helper.AccountId}/emergencyNotificationGroups/{id}",
                ContentToSend = new StringContent(TestXmlStrings.getGroup, Encoding.UTF8, "application/xml")
            }))
            {
                var client = Helper.CreateClient();
                var result = EmergencyNotification.GetGroups(client, id).Result;
                if (server.Error != null) throw server.Error;

                Assert.NotNull(result);
                Assert.Equal("63865500-0904-46b1-9b4f-7bd237a26363", result.EmergencyNotificationGroup.Identifier);
                Assert.Equal("2020-01-23T18:34:17.284Z", result.EmergencyNotificationGroup.CreatedDate);
                Assert.Equal("jgilmore", result.EmergencyNotificationGroup.ModifiedBy);
                Assert.Equal("2020-01-23T18:34:17.284Z", result.EmergencyNotificationGroup.ModifiedDate);
                Assert.Equal("This is a description of the emergency notification group.", result.EmergencyNotificationGroup.Description);
                Assert.Equal(2, result.EmergencyNotificationGroup.EmergencyNotificationRecipients.Length);


            }
        }

        [Fact]
        public void TestListGroups()
        {


            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = $"/v1.0/accounts/{Helper.AccountId}/emergencyNotificationGroups?EnrDetails=true&EnrEmailAddress=test%40example.com",
                ContentToSend = new StringContent(TestXmlStrings.listGroups, Encoding.UTF8, "application/xml")
            }))
            {
                var client = Helper.CreateClient();
                var result = EmergencyNotification.ListGroups(client, new Dictionary<string, Object>
            {
                {"EnrDetails", "true" },
                {"EnrEmailAddress", "test@example.com" }
            }).Result;
                if (server.Error != null) throw server.Error;

                Assert.NotNull(result);
                Assert.NotNull(result.Links);
                Assert.Equal(2, result.EmergencyNotificationGroups.Length);


            }
        }

        [Fact]
        public void TestGetEndpointOrders()
        {
            string id = "123";

            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = $"/v1.0/accounts/{Helper.AccountId}/emergencyNotificationEndpointOrders/{id}",
                ContentToSend = new StringContent(TestXmlStrings.getEndpointOrder, Encoding.UTF8, "application/xml")
            }))
            {
                var client = Helper.CreateClient();
                var result = EmergencyNotification.GetEndpointOrders(client, id).Result;
                if (server.Error != null) throw server.Error;

                Assert.NotNull(result);
                Assert.Equal("3e9a852e-2d1d-4e2d-84c3-87223a78cb70", result.EmergencyNotificationEndpointOrder.OrderId);
                Assert.Equal("2020-01-23T18:34:17.284Z", result.EmergencyNotificationEndpointOrder.OrderCreatedDate);
                Assert.Equal("jgilmore", result.EmergencyNotificationEndpointOrder.CreatedBy);
                Assert.Equal("COMPLETED", result.EmergencyNotificationEndpointOrder.ProcessingStatus);
                Assert.Equal("ALG-31233884", result.EmergencyNotificationEndpointOrder.CustomerOrderId);

                Assert.NotNull(result.EmergencyNotificationEndpointOrder.EmergencyNotificationEndpointAssociations);
                Assert.NotNull(result.EmergencyNotificationEndpointOrder.EmergencyNotificationEndpointAssociations.EmergencyNotificationGroup);

                var addedAssoc = result.EmergencyNotificationEndpointOrder.EmergencyNotificationEndpointAssociations;

                Assert.NotNull(addedAssoc);
                Assert.NotNull(addedAssoc.AddedEepToEngAssociations);
                Assert.Equal(1, addedAssoc.AddedEepToEngAssociations.Length);
                Assert.Equal(2, addedAssoc.AddedEepToEngAssociations[0].EepTns.Length);
                Assert.Equal(2, addedAssoc.AddedEepToEngAssociations[0].EepAeuiIds.Length);

                Assert.Equal("2248838829", addedAssoc.AddedEepToEngAssociations[0].EepTns[0]);
                Assert.Equal("Fred992834", addedAssoc.AddedEepToEngAssociations[0].EepAeuiIds[0]);
            }
        }

        [Fact]
        public void TestListEndpointOrders()
        {


            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "GET",
                EstimatedPathAndQuery = $"/v1.0/accounts/{Helper.AccountId}/emergencyNotificationEndpointOrders?EepTns=404",
                ContentToSend = new StringContent(TestXmlStrings.listEndpointOrder, Encoding.UTF8, "application/xml")
            }))
            {
                var client = Helper.CreateClient();
                var result = EmergencyNotification.ListEndpointOrders(client, new Dictionary<string, Object>
            {
                {"EepTns", "404" }
            }).Result;
                if (server.Error != null) throw server.Error;

                Assert.NotNull(result);

                Assert.NotNull(result.Links);
                Assert.NotNull(result.EmergencyNotificationEndpointOrders);
                Assert.Equal(1, result.EmergencyNotificationEndpointOrders.Length);

            }
        }

        [Fact]
        public void TestCreateEndpointOrders()
        {
            var endpointOrder = new EmergencyNotificationEndpointOrder
            {
                CreatedBy = "user",
                Description = "example description"
            };

            using (var server = new HttpServer(new RequestHandler
            {
                EstimatedMethod = "POST",
                EstimatedPathAndQuery = $"/v1.0/accounts/{Helper.AccountId}/emergencyNotificationEndpointOrders",
                ContentToSend = new StringContent(TestXmlStrings.createEnpointOrder, Encoding.UTF8, "application/xml")
            }))
            {
                var client = Helper.CreateClient();
                var result = EmergencyNotification.CreateEndpointOrders(client, endpointOrder).Result;
                if (server.Error != null) throw server.Error;

                Assert.NotNull(result);


            }
        }

    }

}
