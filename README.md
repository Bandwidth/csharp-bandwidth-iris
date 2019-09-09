# Bandwidth C# IRIS Client

## Installing
Bandwidth C# SDK uses Nuget for Package Management

Run 
``` 
nuget install Bandwidth.Iris
```
Or install Bandwidth.Iris via UI in Visual Studio

## Getting Started
* Install Bandwidth.Iris
* Get an account ID, user name and password from Bandwidth for your account
* Configure the Client 

```csharp
var client = Client.GetInstance("accountId", "username", "password", "apiEndpoint")
//Or
// Configure System Environment Variables as detailed below:
public static Client GetInstance()
{
    return GetInstance(
        Environment.GetEnvironmentVariable(BandwidthApiAccountId),
        Environment.GetEnvironmentVariable(BandwidthApiUserName),
        Environment.GetEnvironmentVariable(BandwidthApiPassword),
        Environment.GetEnvironmentVariable(BandwidthApiEndpoint),
        Environment.GetEnvironmentVariable(BandwidthApiVersion));
}
var client = Client.GetInstance()
```

## Usage
All static functions support 2 ways to be called: With a client instance as the first arg or without the client instance (default client instance will then be used)

```csharp
var site = await Site.get(client, "siteId");
//Or
var site = await Site.get("siteId"); //This will use the default client where supported
```

## Examples
There is a Project in the solution called Bandwidth.Iris.Examples.  This project has working code examples for the most commonly used objects and methods.

To run the examples, from the Bandwidth Iris Project root folder:

```bash
copy Bandwidth.Iris.Examples\App.config.example Bandwidth.Iris.Examples\App.config
```
* Fill in the config file with the values from your account or credentials provided by Bandwidth
* Compile the project and run the Examples from Visual Studio or from the command line

## API Objects

### General Principles
When using the objects, there are generally static methods for Create, Get and List of each entity.  Once you have an instance of any entity, you can call the instance methods of that entity to perform additional operations.

```csharp
var sites = await Site.List(client);
var site = await Site.Get(client, "siteId");
var newSite = await Site.Create(client, new Site(){
  Name = "MyTest Site"

});
var sipPeers = await site.GetSipPeers();
site.Delete();
```

## Available NpaNxx
```csharp
var query = new Dictionary<string, object>();
query.Add("areaCode", "805");
query.Add("quantity", 3);
var items = await AvailableNpaNxx.List(_client, query);
foreach (AvailableNpaNxx npaNxx in items)
{
    Console.WriteLine(string.Format("NpaNxx: {0}",npaNxx.Npa + npaNxx.Nxx));
}
```

## Available Numbers
```csharp
var query = new Dictionary<string, object>();
query.Add("areaCode", "805");
query.Add("quantity", 3);
var result = await AvailableNumbers.List(_client, query);
foreach (string number in result.TelephoneNumberList)
{
    Console.WriteLine(string.Format("Number: {0}", number));
}

```

## Cities
```csharp
var query = new Dictionary<string, object>();
query.Add("state", "CA");
query.Add("available", true);
var result = await City.List(_client, query);
foreach (City city in result)
{
    Console.WriteLine("City Name: {0}", city.Name);
}
```
## Rate Centers
```csharp
var query = new Dictionary<string, object>();
query.Add("state", "CA");
query.Add("available", true);
var result = await RateCenter.List(query);
foreach (RateCenter rateCenter in result)
{
    Console.WriteLine("RateCenter Name: {0}", rateCenter.Name);
}
```


## Covered Rate Centers
```csharp
var query = new Dictionary<string, object>();
query.Add("zip", "27609");
var result = await CoveredRateCenter.List(_client, query);
foreach (CoveredRateCenter rateCenter in result)
{
    Console.WriteLine("RateCenter Name: {0}", rateCenter.Name);
}
```

## Orders

### Create Order
```csharp
var result = await Order.Create(_client, new Order
{
    Name = "Test Order",
    SiteId = "SiteId",
    CustomerOrderId = "SomeCustomerId",
    LataSearchAndOrderType = new LataSearchAndOrderType
    {
        Lata = "224",
        Quantity = 1
    }
});

```
### Get Order
```csharp
var order = await Order.Get("orderId");
```

### Order Instance Methods
```csharp
order.Update()
order.AddNote(var note);
order.GetNotes();
order.GetAreaCodes();
order.GetNpaNxx();
order.GetTotals();
order.GetTns();
order.GetHistory()
```

## Port Ins

### Port In Check
```csharp
var result = LnpChecker.Check(new string[]{"9195551212", true});
```

### Create PortIn
```csharp
var data = new PortIn
    {
        BillingTelephoneNumber = "+1-202-555-0158",
        Subscriber = new Subscriber
            {
                SubscriberType = "BUSINESS",
                BusinessName = "Company",
		FirstName = "John",
                LastName = "Doe",
                ServiceAddress = new Address
                    {
                        City = "City",
                        StateCode = "State",
                        Country = "Country"
                    }
            },
        PeerId = sipPeer.Id,
        SiteId = site.Id
    };
var order = await PortIn.Create(_client, data);
```

### Get PortIn
```csharp
var portInOrder = PortIn.Get("orderId");
```

### PortIn Instance Methods
```csharp
portInOrder.Update();
portInOrder.Delete();
portInOrder.AddNote(Note n);
portInOrder.GetNotes();

```

### Port In LOA Management
```csharp
portInOrder.CreateFile(Stream s, string mediaType);
portInOrder.CreateFile(byte[] buffer, string mediaType);
portInOrder.UpdateFile(string fileName, Stream s, string mediaType);
portInOrder.UpdateFile(string fileName, byte[] buffer, string mediaType);
portInOrder.GetFileMetaData(string fileName);
portInOrder.DeleteFile(string fileName);
portInOrder.GetFiles(bool metaData);
portInOrder.GetFile(string fileName);
```

## Sites

### Create Site
```csharp
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
```
### List all sites
```csharp
var sites = awaite Site.List();
```

### Deleting a Site
```csharp
site.Delete();
```

## Sip Peers

### Create a Sip Peer
```csharp
var sipPeerHost = "1.2.3.4";
var newSipPeer = await SipPeer.Create(_client, new SipPeer()
    {
        IsDefaultPeer = true,
        ShortMessagingProtocol = "SMPP",
        SiteId = "SiteId,
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
```

### Get Sip Peer
```csharp
var sipPeer = await SipPeer.Get("sipPeerId");
```

### Delete Sip Peer
```csharp
var sipPeer = await SipPeer.Get("sipPeerId");
sipPeer.Delete();
```

### Sip Peer TN Methods
```csharp
sipPeer.GetTn(number);
sipPeer.UpdateTns(number, data);
sipPeer.MoveTns(new string[] numbers);
```

## Subscriptions
### Create Subscription
```csharp
var subscription = await Subscription.Create(new Subscription()
{
    OrderType = "orders",
    OrderId = "100",
    EmailSubscription = new EmailSubscription
    {
        Email = "test@test",
        DigestRequested = "NONE"
    }
};
```

### Get Subscription
```csharp
var subscription = await Subscription.Get("subscriptionId");
```

### List Subscriptions
```
var list = await Subscription.List(new Dictionary<string,object>{{"orderType", "orders"}})
```

### Subscription Instance Methods
```csharp
subscription.Update();
subscription.Delete();
```

## TNs

### Get TN Details
```csharp
var result = await Tn.Get("9195551212");
var details = await result.GetDetails();
```

### List TNs
```csharp
var result = await Tn.List(new Dictionary<string, object>{{{"npa", "919"}})
```

## TN Reservation
### Create TN Reservation

```csharp
var item = await TnReservation.Create(new TnReservation()
{
    AccountId = "accountId",
    ReservedTn = "9195551212",
    ReservationExpires = 0
};
```

### Get TN Reservation

```csharp
var reservation = await TnReservation.Get("Id");
```

### Delete TN Reservation

```csharp
reservation.Delete();
```

## Dlda

### Create Ddla
```csharp
var dlda = new Dlda
            {
                CustomerOrderId = "Your Order Id",
                DldaTnGroups = new[]{
                new DldaTnGroup{
                  TelephoneNumbers  =  new TelephoneNumbers {Numbers =  new[]{"9195551212"}},
                  SubscriberType  =  "RESIDENTIAL",
                  ListingType  =  "LISTED",
                  ListingName  =  new ListingName{
                    FirstName  =  "John",
                    LastName  =  "Smith"
                  },
                  ListAddress  =  true,
                  Address  =  new Address{
                    HouseNumber  =  "123",
                    StreetName  =  "Elm",
                    StreetSuffix  =  "Ave",
                    City  =  "Carpinteria",
                    StateCode  =  "CA",
                    Zip  =  "93013",
                    AddressType  =  "DLDA"
                  }
                }
              }
            };

await Dlda.Create(dlda);
```

### Get Dlda
```csharp
var dlda = await Dlda.Get(id);
```

### Get Dlda History
```csharp
var list = await Dlda.GetHistory();
```
## Lidb

### Create
```csharp
var item = new Lidb
    {
        CustomerOrderId = "A Test order",
        LidbTnGroups = new[] { 
            new LidbTnGroup{
                TelephoneNumbers = new []{"8048030097", "8045030098"},
                SubscriberInformation = "Joes Grarage",
                UseType = "RESIDENTIAL",
                Visibility = "PUBLIC"
            }
        } 
    };
await Lidb.Create(item);
```
### Get Lidb
```csharp
var item = await Lidb.Get(id);
```
### List Lidbs
```csharp
var list = await Lidb.List();
```

## LineOptionOrder

### Create an order

```csharp
var item = new TnLineOptions
{
    TelephoneNumber = "5209072451",
    CallingNameDisplay = "off"
};
var numbers = await LineOptionOrder.Create(item);
```

## InServiceNumber

### List 

```csharp
var list = await InServiceNumber.List(new Dictionary<string, object>{{"city", "Cary"}});

```

### Get totals 

```csharp
var totals = await InServiceNumber.GetTotals();
```

## DiscNumber

### List numbers

```csharp
var list = await DiscNumber.List(new Dictionary<string, object>{{"type", "NPA"}});
```

### Get totals
```csharp
var totals = await DiscNumber.GetTotals();
```

## Host

### List hosts

```csharp
var list = await Host.List(new Dictionary<string,object>{{"type", "SMS"}});
```
