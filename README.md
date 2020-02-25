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
//Uses the System Environment Variables as detailed below
var client = Client.GetInstance()
```

| Environment Variable | Definition |
|----------------------|------------|
|BANDWIDTH_API_ACCOUNT_ID| Your Bandwidth Account Id |
|BANDWIDTH_API_USERNAME| Your Bandwidth username |
|BANDWIDTH_API_PASSWORD| Your Bandwidth password|
|BANDWIDTH_API_ENDPOINT| https://dashboard.bandwidth.com/api |
|BANDWIDTH_API_VERSION| v1.0 |



## Usage
All static functions support 2 ways to be called: With a client instance as the first arg or without the client instance (default client instance will then be used)

```csharp
var site = await Site.get(client, "siteId");
//Or
var site = await Site.get("siteId"); //This will use the default client where supported
```

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

### Error Handling Tips

When making making API calls the client can throw an `AggregateException` this will normally contain a `BandwidthIrisException`.  A suggested way to handle the API calls is to catch the `AggregateException` that API calls can create and `#Handle` the `BandwidthIrisException`. Example:

```csharp
 try
{
    var site = await Site.Get(client, "siteId");
} catch (AggregateException e)
{
    e.Handle((x) =>
    {
        if(x is BandwidthIrisException)
        {
            //Do something 
            return true;
        }

        return false;
    });
}
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
order.GetHistory();
```

## Applications 

### Create Application 
```csharp
var application = new Application
{
    ApplicationId = "d3e418e9-1833-49c1-b6c7-ca1700f79586",
    ServiceType = "Voice-V2",
    AppName = "v1",
    CallbackCreds = new CallbackCreds
    {
        UserId = "login123"
    },
    CallStatusMethod = "GET",
    CallInitiatedMethod = "GET",
    CallInitiatedCallbackUrl = "https://a.com",
    CallStatusCallbackUrl = "https://b.com"
};

var response = await Application.Create(client, application);
```

### List Applications
```csharp
var response = await Application.List(client);
```

### Get Application
```csharp
var response = await Application.Get(client, applicationId);
```

### Partial Update Application
```csharp
var application = new Application
{
    AppName = "XgRIdP"
};

var response = await Application.PartialUpdate(client, applicationId, application);
```

### Full Update Application
```csharp
var application = new Application
{
    ServiceType = "Voice-V2",
    AppName = "v1",
    CallbackCreds = new CallbackCreds
    {
        UserId = "login123"
    },
    CallStatusMethod = "GET",
    CallInitiatedMethod = "GET",
    CallInitiatedCallbackUrl = "https://a.com",
    CallStatusCallbackUrl = "https://b.com"
};

var response = await Application.FullUpdate(client, applicationId, application);
```

### Delete Application
```csharp
var response = await Application.Delete(client, applicationId);
```

### List Application's Associated Peers
```csharp
var response = await Application.ListAssociatedSippeers(client, applicationId);
```

## Port Ins

### Port In Check

For LNP Checker, send one number at a time when using the C# SDK.

#### Example request and error handling iterating over each error in the response.

```csharp
try
{
    var result = await LnpChecker.Check(client, new string[] { "555555" });
    Console.WriteLine(result);
}
catch (Bandwidth.Iris.BandwidthIrisException error)
{
    Console.WriteLine(error.Message);
}
catch (Exception e)
{
    Exception innerEx = e;

    while(innerEx != null)
    {
        string msg = innerEx.Message;
        Console.WriteLine(msg);
        innerEx = innerEx.InnerException;
    }
}
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

### Put File Metadata
```csharp
var portIn = new PortIn { Id = "1" };
portIn.SetClient(client);

var fileMetadata = new FileMetadata {
	DocumentType = "INVOICE",
	DocumentName = "docName"
};

var r = portIn.PutFileMetadata("test", fileMetadata).Result;
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
PutFileMetadata(string fileName, FileMetadata fileMetadata);
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

### Get Origination Settings for Sip Peer
```csharp
var sipPeerOriginationSettingsResponse = await SipPeer.GetOriginationSettings(siteId, sipPeerId);
```

### Set Origination Settings for Sip Peer
```csharp
var sipPeerOriginationSettings = new SipPeerOriginationSettings
{
    VoiceProtocol = "HTTP",
    HttpSettings = new HttpSettings
    {
        HttpVoiceV2AppId = "469ebbac-4459-4d98-bc19-a038960e787f"
    }
};

var sipPeerOriginationSettingsResponse = await SipPeer.SetOriginationSettings(siteId, sipPeerId, sipPeerOriginationSettings);
```

### Update Origination Settings for Sip Peer
```csharp
var sipPeerOriginationSettings = new SipPeerOriginationSettings
{
    VoiceProtocol = "HTTP",
    HttpSettings = new HttpSettings
    {
        HttpVoiceV2AppId = "469ebbac-4459-4d98-bc19-a038960e787f"
    }
};

SipPeer.UpdateOriginationSettings(siteId, sipPeerId, SipPeerOriginationSettings).Wait();
```


### Get Termination Settings for Sip Peer
```csharp
var sipPeerTerminationSettingsResponse = await SipPeer.GetTerminationSetting(siteId, sipPeerId);
```

### Set Termination Settings for Sip Peer
```csharp
var sipPeerTerminationSettings = new SipPeerTerminationSettings
{
    VoiceProtocol = "HTTP",
    HttpSettings = new HttpSettings
    {
        HttpVoiceV2AppId = "469ebbac-4459-4d98-bc19-a038960e787f"
    }
};

var sipPeerTerminationSettingsResponse = await SipPeer.GetTerminationSetting(siteId, sipPeerId, sipPeerTerminationSettings);
```

### Update Termination Settings for Sip Peer
```csharp
var sipPeerTerminationSettings = new SipPeerTerminationSettings
{
    VoiceProtocol = "HTTP",
    HttpSettings = new HttpSettings
    {
        HttpVoiceV2AppId = "469ebbac-4459-4d98-bc19-a038960e787f"
    }
};

SipPeer.UpdateTerminationSettings(client, siteId, sipPeerId, SipPeerTerminationSettings).Wait();
```

### Get SMS Feature Settings for Sip Peer
```csharp
var sipPeerSmsFeatureResponse = await SipPeer.GetSMSSetting(siteId, sipPeerId);
```

### Set SMS Feature Setting for Sip Peer
```csharp
var sipPeerSmsFeature = new SipPeerSmsFeature
{
    SipPeerSmsFeatureSettings = new SipPeerSmsFeatureSettings
    {
        TollFree = true
    },
    SmppHosts = new SmppHost[]
    {
        new SmppHost
        {
            HostName = "Host"
        }
    }
};

var sipPeerSmsFeatureResponse = await SipPeer.CreateSMSSettings(siteId, sipPeerId, sipPeerSmsFeature);
```

### Update SMS Feature Setting for Sip Peer
```csharp
var sipPeerSmsFeature = new SipPeerSmsFeature
{
    SipPeerSmsFeatureSettings = new SipPeerSmsFeatureSettings
    {
        TollFree = true
    },
    SmppHosts = new SmppHost[]
    {
        new SmppHost
        {
            HostName = "Host"
        }
    }
};

var sipPeerSmsFeatureResponse = await SipPeer.UpdateSMSSettings(siteId, sipPeerId, sipPeerSmsFeature);
```

### Delete SMS Feature Settings for Sip Peer
```csharp
SipPeer.DeleteSMSSettings(siteId, sipPeerId).Wait();
```

### Get MMS Feature Settings for Sip Peer
```csharp
var MmsFeatureResponse = await SipPeer.GetMMSSetting(siteId, sipPeerId);
```

### Set MMS Feature Setting for Sip Peer
```csharp
var mmsFeature = new MmsFeature
{
    Protocols = new Protocols
    {
        MM4 = new MM4
        {
            Tls = "OFF"
        }
    }
};

var MmsFeatureResponse = await SipPeer.CreateMMSSettings(siteId, sipPeerId, mmsFeature);
```

### Update MMS Feature Setting for Sip Peer
```csharp
var mmsFeature = new MmsFeature
{
    Protocols = new Protocols
    {
        MM4 = new MM4
        {
            Tls = "OFF"
        }
    }
};

SipPeer.UpdateMMSSettings(siteId, sipPeerId, MmsFeature).Wait();
```

### Delete MMS Feature Settings for Sip Peer
```csharp
SipPeer.DeleteMMSSettings(siteId, sipPeerId).Wait();
```

### Get Application Settings
```csharp
var applicationsSettingsResponse = await SipPeer.GetApplicationSetting(siteId, sipPeerId);
```

### Update Application Settings
```csharp
var applicationSettings = new ApplicationsSettings
{
    HttpMessagingV2AppId = "c3b0f805-06ab-4d36-8bf4-8baff7623398"
};

SipPeer.UpdateApplicationSettings(siteId, sipPeerId, applicationSettings).Wait();
```

### Remove Application Settings
```csharp
SipPeer.RemoveApplicationSettings(siteId, sipPeerId).Wait();
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

## ImportTnOrders

### Create A ImportTnOrders Reqeust

```csharp
var order = new ImportTnOrder
    {
        OrderId = "fbd17609-be44-48e7-a301-90bd6cf42248",
        AccountId = "account",
        SipPeerId = 1,
        SiteId = 2
    };

var response = await ImportTnOrder.Create(client, order);
```


### Retrieve ImportOrders List

```csharp
var response = await ImportTnOrder.List(client, new Dictionary<string, object> { { "accountId", "1" } });
```

### Get ImportOrder by OrderId

```csharp
var response = await ImportTnOrder.Get(client, orderId);
```

### Get ImportOrder History by OrderId

```csharp
var response = await ImportTnOrder.GetHistory(client, orderId )
```


## RemoveImportedTnOrders

### Create A ImportTnOrders Reqeust

```csharp
var order = new RemoveImportedTnOrder
    {
        OrderId = "fbd17609-be44-48e7-a301-90bd6cf42248",
        AccountId = "account"
    };

var response = await RemoveImportedTnOrder.Create(client, order);
```


### Retrieve RemoveImportedTnOrders List

```csharp
var response = await RemoveImportedTnOrder.List(client, new Dictionary<string, object> { { "accountId", "1" } });
```

### Get RemoveImportedTnOrder by OrderId

```csharp
var response = await RemoveImportedTnOrder.Get(client, orderId);
```

### Get RemoveImportedTnOrder History by OrderId

```csharp
var response = await RemoveImportedTnOrder.GetHistory(client, orderId )
```

## ImportTnChecker

### Request Portability Information on a Set of TNs

```csharp
var payload = new ImportTnCheckerPayload
{
    TelephoneNumbers = new TelephoneNumber[]
    {
        new TelephoneNumber
        {
            FullNumber = "3032281000"
        }
    }
};

var response = await ImportTnChecker.Create(client, payload);
```


## Csrs 

### Create Csrs Order 

```csharp
var csr = new Csr
{
    AccountId =  "accountId"
    //Additional information
};

var response = await Csr.Create(client, csr);
```

### Get Csr Order 

```csharp
var response = await Csr.Get(client, orderId);
```

### Replace Csr Order 

```csharp
var csr = new Csr
{
    AccountId =  "new accountId"
    //Additional information
};

var response = await Csr.Replace(client, orderId, csr);
```

### List Notes on Csr Order 

```csharp
var response = await Csr.ListNotes(client, orderId);
```

### Create Note on Csr Order

```csharp
var note = new Note
{
    Description = "Description goes here"
};

Csr.CreateNote(client, orderId, note)
```

### Update Note on Csr Order

```csharp
var note = new Note
{
    Description = "Updated description goes here"
};

Csr.UpdateNote(client, orderId, noteId, note) 
```