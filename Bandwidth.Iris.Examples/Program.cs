using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bandwidth.Iris.Model;

namespace Bandwidth.Iris.Examples
{
    class Program
    {
        private static readonly Client _client = Client.GetInstance(
            ConfigurationManager.AppSettings["accountId"],
            ConfigurationManager.AppSettings["username"],
            ConfigurationManager.AppSettings["password"],
            ConfigurationManager.AppSettings["apiEndpoint"]);
        static void Main(string[] args)
        {
            try
            {
                Start().Wait();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.ToString());
            }

            Console.WriteLine("Demo has concluded");
            Console.ReadLine();
        }

        private static async Task Start()
        {
            await AccountDetailsDemo();
            await AvailableNpaNxxDemo();
            await AvailableNumbersDemo();
            await CitiesDemo();
            await CoveredRateCentersDemo();
            await RateCentersDemo();
            await OrdersDemo();
            //Uncomment and change phone number to run
            //await PortInDemo();
            await SiteDemo();
            //Uncomment and change host in the demo method to run
            //await SipPeerDemo();
            await TnDetailsDemo();
            await TnsListDemo();

        }

        static async Task AccountDetailsDemo()
        {
            var account = await Account.Get(_client);
            Console.WriteLine(account.CompanyName);
        }

        static async Task AvailableNpaNxxDemo()
        {
            var query = new Dictionary<string, object>();
            query.Add("areaCode", "805");
            query.Add("quantity", 3);
            var items = await AvailableNpaNxx.List(_client, query);
            foreach (AvailableNpaNxx npaNxx in items)
            {
                Console.WriteLine(string.Format("NpaNxx: {0}",npaNxx.Npa + npaNxx.Nxx));
            }
        }

        static async Task AvailableNumbersDemo()
        {
            var query = new Dictionary<string, object>();
            query.Add("areaCode", "805");
            query.Add("quantity", 3);
            var result = await AvailableNumbers.List(_client, query);
            foreach (string number in result.TelephoneNumberList)
            {
                Console.WriteLine(string.Format("Number: {0}", number));
            }
            
        }

        static async Task CitiesDemo()
        {
            var query = new Dictionary<string, object>();
            query.Add("state", "CA");
            query.Add("available", true);
            var result = await City.List(_client, query);
            foreach (City city in result)
            {
                Console.WriteLine("City Name: {0}", city.Name);
            }
        }

        static async Task CoveredRateCentersDemo()
        {
            var query = new Dictionary<string, object>();
            query.Add("zip", "27609");
            query.Add("size", 500);
            query.Add("page", 1);
            var result = await CoveredRateCenter.List(_client, query);
            foreach (CoveredRateCenter rateCenter in result)
            {
                Console.WriteLine("RateCenter Name: {0}", rateCenter.Name);
            }
        }

        static async Task RateCentersDemo()
        {
            var query = new Dictionary<string, object>();
            query.Add("state", "CA");
            query.Add("available", true);
            var result = await RateCenter.List(_client, query);
            foreach (RateCenter rateCenter in result)
            {
                Console.WriteLine("RateCenter Name: {0}", rateCenter.Name);
            }
        }

        static async Task OrdersDemo()
        {
            var sites = await Site.List(_client);
            var site = sites[0];

            var result = await Order.Create(_client, new Order
                {
                    Name = "Test Order",
                    SiteId = site.Id,
                    CustomerOrderId = "SomeCustomerId",
                    LataSearchAndOrderType = new LataSearchAndOrderType
                        {
                            Lata = "224",
                            Quantity = 1
                        }
                });

            Console.WriteLine("Created Order ID: {0}", result.Order.OrderId);

        }

        static async Task PortInDemo()
        {
            var numberToCheck = "9192971001";
            var lnpResult = await LnpChecker.Check(_client, new []{numberToCheck}, true);
            if (lnpResult.PortableNumbers != null && lnpResult.PortableNumbers[0].Equals(numberToCheck))
            {
                var sites = await Site.List(_client);
                var site = sites[0];
                var sipPeers = await site.GetSipPeers();
                var sipPeer = sipPeers[0];


                var data = new PortIn
                    {
                        BillingTelephoneNumber = numberToCheck,
                        LoaAuthorizingPerson = "Joe Blow",
                        Subscriber = new Subscriber
                            {
                                SubscriberType = "BUSINESS",
                                BusinessName = "Company",
                                ServiceAddress = new Address
                                    {
                                        HouseNumber = "123",
                                        StreetName = "Anywhere St",
                                        City = "Raleigh",
                                        StateCode = "NC",
                                        Zip= "27609"

                                    }
                            },
                            ListOfPhoneNumbers = new string[]
                                {
                                    numberToCheck
                                },
                        PeerId = sipPeer.Id,
                        SiteId = site.Id
                    };
                var order = await PortIn.Create(_client, data);
                Console.WriteLine("Created PortIn Order ID: {0}", order.Id);

                var fileName = await order.CreateFile(new byte[] {0, 1, 2, 3, 4, 5}, "application/pdf");
                var metadata = await order.GetFileMetadata(fileName);
                using (var content = await order.GetFile(fileName))
                {
                    var fileContent = content.Buffer;
                }
                await order.UpdateFile(fileName, new byte[] {10, 11, 12, 13, 14, 15}, "application/pdf");
                await order.DeleteFile(fileName);
                await order.Update(new LnpOrderSupp
                    {
                        RequestedFocDate = DateTime.Parse("2015-07-18T00:00:00.000Z"),
                        WirelessInfo = new[]
                            {
                                new WirelessInfo
                                    {
                                        AccountNumber = "77129766500001",
                                        PinNumber = "0000"
                                    }
                            }
                    });
                await order.Delete();
            }
        }

        static async Task SiteDemo()
        {
            var newSite = await Site.Create(_client, new Site()
                {
                    Name = "Csharp Test Site",
                    Description = "A site from the C# Example",
                    Address = new Address()
                        {
                            HouseNumber = "123",
                            StreetName = "Anywhere St",
                            City = "Raleigh",
                            StateCode = "NC",
                            Zip = "27609",
                            AddressType = "Service"
                        }

                });

            Console.WriteLine("New Site Created, ID {0}", newSite.Id);

            var site = await Site.Get(_client, newSite.Id);
            if (site.Id == newSite.Id)
            {
                Console.WriteLine("Successfully retrieved Site");
            }

        }

        static async Task SipPeerDemo()
        {
            var sipPeerHost = ConfigurationManager.AppSettings["sipPeerHost"];
            var newSipPeer = await SipPeer.Create(_client, new SipPeer()
                {
                    IsDefaultPeer = true,
                    ShortMessagingProtocol = "SMPP",
                    SiteId = ConfigurationManager.AppSettings["selectedSiteId"],
                    VoiceHosts = new []
                        {
                            new HostData
                                {
                                    HostName = sipPeerHost
                                }
                        },
                    SmsHosts = new []
                        {
                            new HostData
                                {
                                    HostName = sipPeerHost
                                }
                        },
                    TerminationHosts = new TerminationHost[]
                        {
                            new TerminationHost()
                                {
                                    HostName = sipPeerHost,
                                    Port = 5060
                                }
                        }

                });

            Console.WriteLine("New SipPeer Created, ID: {0}", newSipPeer.PeerId);

        }

        static async Task TnDetailsDemo()
        {
            var result = await Tn.Get(_client, "8183386252");
            var details = await result.GetDetails();
            if (details.FullNumber != null)
            {
                Console.WriteLine("Got TN Detail For Number: {0}, Lata: {1}", details.FullNumber, details.Lata);
            }
        }
        
        static async Task TnsListDemo()
        {
            var result = await Tn.List(_client, new Dictionary<string, object> {{"npa", "818"}});
            Console.WriteLine("There are {0} tns in this list", result.TelephoneNumberCount);
        }
    }
}
