﻿namespace Bandwidth.Iris.Tests
{
    public static class TestXmlStrings
    {
        public static string LocalAreaSearchResultXml = "<SearchResult><ResultCount>2</ResultCount><TelephoneNumberDetailList><TelephoneNumberDetail><City>JERSEY CITY</City><LATA>224</LATA><RateCenter>JERSEYCITY</RateCenter><State>NJ</State><TelephoneNumber>2012001555</TelephoneNumber></TelephoneNumberDetail><TelephoneNumberDetail><City>JERSEY CITY</City><LATA>224</LATA><RateCenter>JERSEYCITY</RateCenter><State>NJ</State><TelephoneNumber>123123123</TelephoneNumber></TelephoneNumberDetail></TelephoneNumberDetailList></SearchResult>";

        /**
         * Sites Xmls
         */
        public static string ValidSitesResponseXml = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?><SitesResponse><Sites><Site><Id>2858</Id><Name>Test Site</Name><Description>A site description</Description></Site></Sites></SitesResponse>";
        public static string ValidSiteResponseXml = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?><SiteResponse><Site><Id>2858</Id><Name>Test Site</Name><Description>A Site Description</Description><Address><HouseNumber>900</HouseNumber><StreetName>Main Campus Drive</StreetName><City>Raleigh</City><StateCode>NC</StateCode><Zip>27615</Zip><Country>United States</Country><AddressType>Service</AddressType></Address></Site></SiteResponse>";
        public static string InvalidSiteDeleteResponseXml = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?><SiteResponse><ResponseStatus><ErrorCode>12016</ErrorCode><Description>Site '5001' does not exist</Description></ResponseStatus></SiteResponse>";

        /**
         * Sip Peer Xmls
         */
        public static string ValidSipPeersResponseXml = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?><TNSipPeersResponse><SipPeers><SipPeer xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xsi:type=\"SipPeer\"><PeerId>12345</PeerId><PeerName>SIP Peer 1</PeerName><Description>Sip Peer 1 description</Description><IsDefaultPeer>true</IsDefaultPeer><ShortMessagingProtocol>SIP</ShortMessagingProtocol><VoiceHosts><Host><HostName>70.62.112.156</HostName></Host></VoiceHosts><VoiceHostGroups/><SmsHosts><Host><HostName>70.62.112.156</HostName></Host></SmsHosts><TerminationHosts><TerminationHost><HostName>70.62.112.156</HostName><Port>5060</Port><CustomerTrafficAllowed>DOMESTIC</CustomerTrafficAllowed><DataAllowed>true</DataAllowed></TerminationHost></TerminationHosts><CallingName><Display>true</Display><Enforced>false</Enforced></CallingName></SipPeer></SipPeers></TNSipPeersResponse>";
        public static string ValidSipPeerResponseXml = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?><SipPeerResponse><SipPeer><PeerId>5678</PeerId><PeerName>SIP Peer 1</PeerName><Description>Sip Peer 1 description</Description><IsDefaultPeer>true</IsDefaultPeer><ShortMessagingProtocol>SIP</ShortMessagingProtocol><VoiceHosts/><VoiceHostGroups/><SmsHosts/><TerminationHosts/><CallingName><Display>true</Display><Enforced>false</Enforced></CallingName></SipPeer></SipPeerResponse>";
        public static string InvalidIdSipPeerResponseXml = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?><SipPeerResponse><ResponseStatus><ErrorCode>13563</ErrorCode><Description>Sip Peer '5555' account 'accountId' and site '1234' does not exist</Description></ResponseStatus></SipPeerResponse>";
        public static string ValidSipPeerTnResponseXml = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?><SipPeerTelephoneNumberResponse><SipPeerTelephoneNumber><FullNumber>9195551212</FullNumber></SipPeerTelephoneNumber></SipPeerTelephoneNumberResponse>";
        public static string InvalidSipPeerTnResponseXml = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?><SipPeerTelephoneNumberResponse><ResponseStatus><ErrorCode>13536</ErrorCode><Description>Telephone number 9195551213 does not exist on Account accountId, Site 1234, and Sip Peer 5678</Description></ResponseStatus></SipPeerTelephoneNumberResponse>";

        /**
         * Order Xmls
         */
        public static string ValidOrderResponseXml = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?><OrderResponse><Order><Name>A New Order</Name><OrderCreateDate>2014-10-14T17:58:15.299Z</OrderCreateDate><BackOrderRequested>false</BackOrderRequested><id>someid</id><ExistingTelephoneNumberOrderType><TelephoneNumberList><TelephoneNumber>2052865046</TelephoneNumber></TelephoneNumberList></ExistingTelephoneNumberOrderType><PartialAllowed>false</PartialAllowed><SiteId>2858</SiteId></Order></OrderResponse>";
        public static string ValidCreateDisconnectTelephoneNumberOrderResponseXml = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?><DisconnectTelephoneNumberOrderResponse><orderRequest><Name>Disconnect</Name><OrderCreateDate>2014-10-17T15:02:46.077Z</OrderCreateDate><id>disconnectId</id><DisconnectTelephoneNumberOrderType><TelephoneNumberList><TelephoneNumber>2055551212</TelephoneNumber></TelephoneNumberList><DisconnectMode>normal</DisconnectMode></DisconnectTelephoneNumberOrderType></orderRequest></DisconnectTelephoneNumberOrderResponse>";
        public static string ValidCreateDisconnectTelephoneNumberOrderResponseWithErrorsXml = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?><DisconnectTelephoneNumberOrderResponse><ErrorList><Error><Code>5006</Code><Description>The telephone number 2055551212 could not be disconnected since it is not associated with your account</Description><TelephoneNumber>2055551212</TelephoneNumber></Error></ErrorList><orderRequest><Name>Disconnect</Name><OrderCreateDate>2014-10-17T15:02:46.077Z</OrderCreateDate><id>disconnectId</id><DisconnectTelephoneNumberOrderType><TelephoneNumberList><TelephoneNumber>2055551212</TelephoneNumber></TelephoneNumberList><DisconnectMode>normal</DisconnectMode></DisconnectTelephoneNumberOrderType></orderRequest><OrderStatus>FAILED</OrderStatus></DisconnectTelephoneNumberOrderResponse>";

        /**
         * Reservation Xmls
         */

        public static string ValidReservationResponseXml = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?><ReservationResponse><Reservation><ReservationId>1</ReservationId><AccountId>accountId</AccountId><ReservationExpires>30</ReservationExpires><ReservedTn>9195551212</ReservedTn></Reservation></ReservationResponse>";
        public static string InvalidReservationResponseXml = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?><ReservationResponse><ResponseStatus><ErrorCode>5041</ErrorCode><Description>Reservation failed: telephone number 9195551212 is not available.</Description></ResponseStatus></ReservationResponse>";
    }
}