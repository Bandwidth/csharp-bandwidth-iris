using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Bandwidth.Iris.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bandwidth.Iris.Tests.Models
{
    [TestClass]
    public class EmergencyNotificationTests
    {
        [TestInitialize]
        public void Setup()
        {
            Helper.SetEnvironmetVariables();
        }

        [TestMethod]
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

                Assert.IsNotNull(result);
                Assert.AreEqual(" 63865500-0904-46b1-9b4f-7bd237a26363 ", result.EmergencyNotificationRecipient.Identifier);
                Assert.AreEqual("2020-03-18T21:26:47.403Z", result.EmergencyNotificationRecipient.CreatedDate);
                Assert.AreEqual("2020-04-01T18:32:22.316Z", result.EmergencyNotificationRecipient.LastModifiedDate);
                Assert.AreEqual("jgilmore", result.EmergencyNotificationRecipient.ModifiedByUser);
                Assert.AreEqual(" This is a description of the emergency notification recipient. ", result.EmergencyNotificationRecipient.Description);
                Assert.AreEqual("CALLBACK", result.EmergencyNotificationRecipient.Type);

                Assert.AreEqual("https://foo.bar/baz", result.EmergencyNotificationRecipient.Callback.Url);
                Assert.AreEqual("jgilmore", result.EmergencyNotificationRecipient.Callback.Credentials.Username);

                Assert.AreEqual("fred@gmail.com", result.EmergencyNotificationRecipient.EmailAddress);

                Assert.AreEqual("12015551212", result.EmergencyNotificationRecipient.Sms.TelephoneNumber);

                Assert.AreEqual("12015551212", result.EmergencyNotificationRecipient.Tts.TelephoneNumber);

            }
        }

        [TestMethod]
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

                Assert.IsNotNull(result.EmergencyNotificationRecipients);
                Assert.AreEqual(4, result.EmergencyNotificationRecipients.Length);
                Assert.IsNotNull(result.Links);
            }
        }

        [TestMethod]
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

                Assert.IsNotNull(result);

            }
        }

        [TestMethod]
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

                Assert.IsNotNull(result);

            }
        }

        [TestMethod]
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

        [TestMethod]
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

                Assert.IsNotNull(result);
                Assert.AreEqual("3e9a852e-2d1d-4e2d-84c3-87223a78cb70", result.EmergencyNotificationGroup.OrderId);
                Assert.AreEqual("2020-01-23T18:34:17.284Z", result.EmergencyNotificationGroup.OrderCreatedDate);
                Assert.AreEqual("jgilmore", result.EmergencyNotificationGroup.CreatedBy);
                Assert.AreEqual("COMPLETED", result.EmergencyNotificationGroup.ProcessingStatus);
                Assert.AreEqual("ALG-31233884", result.EmergencyNotificationGroup.CustomerOrderId);
                Assert.IsNotNull(result.EmergencyNotificationGroup.AddedEmergencyNotificationGroup);

            }
        }

        [TestMethod]
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

                Assert.IsNotNull(result);
                Assert.IsNotNull( result.Links);
                Assert.IsNotNull(result.EmergencyNotificationGroupOrders);
                Assert.AreEqual(3, result.EmergencyNotificationGroupOrders.Length);

            }
        }

        [TestMethod]
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

                Assert.IsNotNull(result);
                Assert.AreEqual("3e9a852e-2d1d-4e2d-84c3-87223a78cb70", result.EmergencyNotificationGroup.OrderId);
                Assert.AreEqual("2020-01-23T18:34:17.284Z", result.EmergencyNotificationGroup.OrderCreatedDate);
                Assert.AreEqual("jgilmore", result.EmergencyNotificationGroup.CreatedBy);
                Assert.AreEqual("COMPLETED", result.EmergencyNotificationGroup.ProcessingStatus);
                Assert.AreEqual("ALG-31233884", result.EmergencyNotificationGroup.CustomerOrderId);
                Assert.IsNotNull(result.EmergencyNotificationGroup.AddedEmergencyNotificationGroup);

            }
        }

        [TestMethod]
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

                Assert.IsNotNull(result);
                Assert.AreEqual("63865500-0904-46b1-9b4f-7bd237a26363", result.EmergencyNotificationGroup.Identifier);
                Assert.AreEqual("2020-01-23T18:34:17.284Z", result.EmergencyNotificationGroup.CreatedDate);
                Assert.AreEqual("jgilmore", result.EmergencyNotificationGroup.ModifiedBy);
                Assert.AreEqual("2020-01-23T18:34:17.284Z", result.EmergencyNotificationGroup.ModifiedDate);
                Assert.AreEqual("This is a description of the emergency notification group.", result.EmergencyNotificationGroup.Description);
                Assert.AreEqual(2, result.EmergencyNotificationGroup.EmergencyNotificationRecipients.Length);


            }
        }

        [TestMethod]
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

                Assert.IsNotNull(result);
                Assert.IsNotNull(result.Links);
                Assert.AreEqual(2, result.EmergencyNotificationGroups.Length);


            }
        }

        [TestMethod]
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

                Assert.IsNotNull(result);
                Assert.AreEqual("3e9a852e-2d1d-4e2d-84c3-87223a78cb70", result.EmergencyNotificationEndpointOrder.OrderId);
                Assert.AreEqual("2020-01-23T18:34:17.284Z", result.EmergencyNotificationEndpointOrder.OrderCreatedDate);
                Assert.AreEqual("jgilmore", result.EmergencyNotificationEndpointOrder.CreatedBy);
                Assert.AreEqual("COMPLETED", result.EmergencyNotificationEndpointOrder.ProcessingStatus);
                Assert.AreEqual("ALG-31233884", result.EmergencyNotificationEndpointOrder.CustomerOrderId);
                
                Assert.IsNotNull(result.EmergencyNotificationEndpointOrder.EmergencyNotificationEndpointAssociations);
                Assert.IsNotNull(result.EmergencyNotificationEndpointOrder.EmergencyNotificationEndpointAssociations.EmergencyNotificationGroup);

                var addedAssoc = result.EmergencyNotificationEndpointOrder.EmergencyNotificationEndpointAssociations;

                Assert.IsNotNull(addedAssoc);
                Assert.IsNotNull(addedAssoc.AddedEepToEngAssociations);
                Assert.AreEqual(1, addedAssoc.AddedEepToEngAssociations.Length);
                Assert.AreEqual(2, addedAssoc.AddedEepToEngAssociations[0].EepTns.Length);
                Assert.AreEqual(2, addedAssoc.AddedEepToEngAssociations[0].EepAeuiIds.Length);

                Assert.AreEqual("2248838829", addedAssoc.AddedEepToEngAssociations[0].EepTns[0]);
                Assert.AreEqual("Fred992834", addedAssoc.AddedEepToEngAssociations[0].EepAeuiIds[0]);
            }
        }

        [TestMethod]
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

                Assert.IsNotNull(result);

                Assert.IsNotNull(result.Links);
                Assert.IsNotNull(result.EmergencyNotificationEndpointOrders);
                Assert.AreEqual(1, result.EmergencyNotificationEndpointOrders.Length);
               
            }
        }

        [TestMethod]
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

                Assert.IsNotNull(result);


            }
        }

    }

}
