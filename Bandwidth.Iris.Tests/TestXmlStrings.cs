namespace Bandwidth.Iris.Tests
{
    public static class TestXmlStrings
    {
        public static string getTnOptions = @"<TnOptionOrder>
    <OrderCreateDate>2016-01-15T11:22:58.789Z</OrderCreateDate>
    <AccountId>14</AccountId>
    <CreatedByUser>jbm</CreatedByUser>
    <OrderId>409033ee-88ec-43e3-85f3-538f30733963</OrderId>
    <LastModifiedDate>2016-01-15T11:22:58.969Z</LastModifiedDate>
    <ProcessingStatus>COMPLETE</ProcessingStatus>
    <TnOptionGroups>
        <TnOptionGroup>
            <CallingNameDisplay>on</CallingNameDisplay>
            <Sms>on</Sms>
            <TelephoneNumbers>
                <TelephoneNumber>2174101601</TelephoneNumber>
            </TelephoneNumbers>
        </TnOptionGroup>
        <TnOptionGroup>
            <CallingNameDisplay>off</CallingNameDisplay>
            <TelephoneNumbers>
                <TelephoneNumber>2174101602</TelephoneNumber>
            </TelephoneNumbers>
        </TnOptionGroup>
        <TnOptionGroup>
            <CallingNameDisplay>systemdefault</CallingNameDisplay>
            <FinalDestinationURI>sip:+12345678901@1.2.3.4:5060</FinalDestinationURI>
            <TelephoneNumbers>
                <TelephoneNumber>2174101603</TelephoneNumber>
            </TelephoneNumbers>
        </TnOptionGroup>
    </TnOptionGroups>
    <ErrorList/>
    <Warnings>
        <Warning>
            <TelephoneNumber>2174101601</TelephoneNumber>
            <Description>SMS is already Enabled or number is in processing.</Description>
        </Warning>
    </Warnings>
</TnOptionOrder>";

        public static string listTnOptions = @"<TnOptionOrders>
    <TotalCount>2</TotalCount>
    <TnOptionOrder>
        <OrderCreateDate>2016-01-15T12:01:14.324Z</OrderCreateDate>
        <AccountId>14</AccountId>
        <CreatedByUser>jbm</CreatedByUser>
        <OrderId>ddbdc72e-dc27-490c-904e-d0c11291b095</OrderId>
        <LastModifiedDate>2016-01-15T12:01:14.363Z</LastModifiedDate>
        <ProcessingStatus>FAILED</ProcessingStatus>
        <TnOptionGroups>
            <TnOptionGroup>
                <NumberFormat>10digit</NumberFormat>
                <RPIDFormat>10digit</RPIDFormat>
                <RewriteUser>testUser1</RewriteUser>
                <CallForward>6042661720</CallForward>
                <CallingNameDisplay>on</CallingNameDisplay>
                <Protected>true</Protected>
                <Sms>on</Sms>
                <FinalDestinationURI>sip:+12345678901@1.2.3.4:5060</FinalDestinationURI>
                <TelephoneNumbers>
                    <TelephoneNumber>2018551020</TelephoneNumber>
                </TelephoneNumbers>
            </TnOptionGroup>
            <TnOptionGroup>
                <CallingNameDisplay>off</CallingNameDisplay>
                <Protected>false</Protected>
                <Sms>off</Sms>
                <TelephoneNumbers>
                    <TelephoneNumber>2018551025</TelephoneNumber>
                </TelephoneNumbers>
            </TnOptionGroup>
        </TnOptionGroups>
         <ErrorList>
            <Error>
                <Code>5076</Code>
                <Description>Telephone number is not available.</Description>
                <TelephoneNumber>2018551025</TelephoneNumber>
            </Error>
            <Error>
                <Code>5076</Code>
                <Description>Telephone number is not available.</Description>
                <TelephoneNumber>2018551020</TelephoneNumber>
            </Error>
        </ErrorList>
    </TnOptionOrder>
    <TnOptionOrder>
        <OrderCreateDate>2016-01-15T11:22:58.789Z</OrderCreateDate>
        <AccountId>14</AccountId>
        <CreatedByUser>jbm</CreatedByUser>
        <OrderId>409033ee-88ec-43e3-85f3-538f30733963</OrderId>
        <LastModifiedDate>2016-01-15T11:22:58.969Z</LastModifiedDate>
        <ProcessingStatus>COMPLETE</ProcessingStatus>
        <TnOptionGroups>
            <TnOptionGroup>
                <CallingNameDisplay>on</CallingNameDisplay>
                <TelephoneNumbers>
                    <TelephoneNumber>2174101601</TelephoneNumber>
                </TelephoneNumbers>
            </TnOptionGroup>
            <TnOptionGroup>
                <CallingNameDisplay>off</CallingNameDisplay>
                <TelephoneNumbers>
                    <TelephoneNumber>2174101602</TelephoneNumber>
                </TelephoneNumbers>
            </TnOptionGroup>
            <TnOptionGroup>
                <CallingNameDisplay>systemdefault</CallingNameDisplay>
                <TelephoneNumbers>
                    <TelephoneNumber>2174101603</TelephoneNumber>
                </TelephoneNumbers>
            </TnOptionGroup>
        </TnOptionGroups>
        <ErrorList/>
    </TnOptionOrder>
</TnOptionOrders>";


        public static string listTnOptionsNoError = @"<TnOptionOrders>
    <TotalCount>2</TotalCount>
    <TnOptionOrder>
        <OrderCreateDate>2016-01-15T12:01:14.324Z</OrderCreateDate>
        <AccountId>14</AccountId>
        <CreatedByUser>jbm</CreatedByUser>
        <OrderId>ddbdc72e-dc27-490c-904e-d0c11291b095</OrderId>
        <LastModifiedDate>2016-01-15T12:01:14.363Z</LastModifiedDate>
        <ProcessingStatus>FAILED</ProcessingStatus>
        <TnOptionGroups>
            <TnOptionGroup>
                <NumberFormat>10digit</NumberFormat>
                <RPIDFormat>10digit</RPIDFormat>
                <RewriteUser>testUser1</RewriteUser>
                <CallForward>6042661720</CallForward>
                <CallingNameDisplay>on</CallingNameDisplay>
                <Protected>true</Protected>
                <Sms>on</Sms>
                <FinalDestinationURI>sip:+12345678901@1.2.3.4:5060</FinalDestinationURI>
                <TelephoneNumbers>
                    <TelephoneNumber>2018551020</TelephoneNumber>
                </TelephoneNumbers>
            </TnOptionGroup>
            <TnOptionGroup>
                <CallingNameDisplay>off</CallingNameDisplay>
                <Protected>false</Protected>
                <Sms>off</Sms>
                <TelephoneNumbers>
                    <TelephoneNumber>2018551025</TelephoneNumber>
                </TelephoneNumbers>
            </TnOptionGroup>
        </TnOptionGroups>
    </TnOptionOrder>
    <TnOptionOrder>
        <OrderCreateDate>2016-01-15T11:22:58.789Z</OrderCreateDate>
        <AccountId>14</AccountId>
        <CreatedByUser>jbm</CreatedByUser>
        <OrderId>409033ee-88ec-43e3-85f3-538f30733963</OrderId>
        <LastModifiedDate>2016-01-15T11:22:58.969Z</LastModifiedDate>
        <ProcessingStatus>COMPLETE</ProcessingStatus>
        <TnOptionGroups>
            <TnOptionGroup>
                <CallingNameDisplay>on</CallingNameDisplay>
                <TelephoneNumbers>
                    <TelephoneNumber>2174101601</TelephoneNumber>
                </TelephoneNumbers>
            </TnOptionGroup>
            <TnOptionGroup>
                <CallingNameDisplay>off</CallingNameDisplay>
                <TelephoneNumbers>
                    <TelephoneNumber>2174101602</TelephoneNumber>
                </TelephoneNumbers>
            </TnOptionGroup>
            <TnOptionGroup>
                <CallingNameDisplay>systemdefault</CallingNameDisplay>
                <TelephoneNumbers>
                    <TelephoneNumber>2174101603</TelephoneNumber>
                </TelephoneNumbers>
            </TnOptionGroup>
        </TnOptionGroups>
    </TnOptionOrder>
</TnOptionOrders>";

        public static string listTnOptionsSummary = @"<TnOptionOrders>
    <TotalCount>2</TotalCount>
    <TnOptionOrderSummary>
        <accountId>14</accountId>
        <CountOfTNs>2</CountOfTNs>
        <userId>jbm</userId>
        <lastModifiedDate>2016-01-15T12:01:14.363Z</lastModifiedDate>
        <OrderDate>2016-01-15T12:01:14.324Z</OrderDate>
        <OrderType>tn_option</OrderType>
        <OrderStatus>FAILED</OrderStatus>
        <OrderId>ddbdc72e-dc27-490c-904e-d0c11291b095</OrderId>
    </TnOptionOrderSummary>
    <TnOptionOrderSummary>
        <accountId>14</accountId>
        <CountOfTNs>3</CountOfTNs>
        <userId>jbm</userId>
        <lastModifiedDate>2016-01-15T11:22:58.969Z</lastModifiedDate>
        <OrderDate>2016-01-15T11:22:58.789Z</OrderDate>
        <OrderType>tn_option</OrderType>
        <OrderStatus>COMPLETE</OrderStatus>
        <OrderId>409033ee-88ec-43e3-85f3-538f30733963</OrderId>
    </TnOptionOrderSummary>
</TnOptionOrders>";

        public static string createTnOptionsResponse = @"<TnOptionOrderResponse>
    <TnOptionOrder>
        <OrderCreateDate>2016-01-15T12:01:14.324Z</OrderCreateDate>
        <AccountId>14</AccountId>
        <CreatedByUser>jbm</CreatedByUser>
        <OrderId>ddbdc72e-dc27-490c-904e-d0c11291b095</OrderId>
        <LastModifiedDate>2016-01-15T12:01:14.324Z</LastModifiedDate>
        <ProcessingStatus>RECEIVED</ProcessingStatus>
        <TnOptionGroups>
            <TnOptionGroup>
                <NumberFormat>10digit</NumberFormat>
                <RPIDFormat>10digit</RPIDFormat>
                <RewriteUser>testUser1</RewriteUser>
                <CallForward>6042661720</CallForward>
                <CallingNameDisplay>on</CallingNameDisplay>
                <Protected>true</Protected>
                <Sms>on</Sms>
                <TelephoneNumbers>
                    <TelephoneNumber>2018551020</TelephoneNumber>
                </TelephoneNumbers>
            </TnOptionGroup>
            <TnOptionGroup>
                <CallingNameDisplay>off</CallingNameDisplay>
                <Protected>false</Protected>
                <Sms>off</Sms>
                <TelephoneNumbers>
                    <TelephoneNumber>2018551025</TelephoneNumber>
                </TelephoneNumbers>
            </TnOptionGroup>
        </TnOptionGroups>
        <ErrorList/>
    </TnOptionOrder>
</TnOptionOrderResponse>";

        public static string listAeui = @"<AlternateEndUserIdentifiersResponse>
    <TotalCount>2</TotalCount>
    <Links>
        <first>Link=&lt;http://localhost:8080/iris/accounts/14/aeuis?page=1&amp;size=500&gt;;rel=""first"";</first>
    </Links>
    <AlternateEndUserIdentifiers>
        <AlternateEndUserIdentifier>
            <Identifier>DavidAcid</Identifier>
            <CallbackNumber>8042105760</CallbackNumber>
            <EmergencyNotificationGroup>
                <Identifier>63865500-0904-46b1-9b4f-7bd237a26363</Identifier>
                <Description>Building 5, 5th Floor.</Description>
            </EmergencyNotificationGroup>
        </AlternateEndUserIdentifier>
        <AlternateEndUserIdentifier>
            <Identifier>JohnAcid</Identifier>
            <CallbackNumber>8042105618</CallbackNumber>
        </AlternateEndUserIdentifier>
    </AlternateEndUserIdentifiers>
</AlternateEndUserIdentifiersResponse>";

        public static string getAeui = @"<AlternateEndUserIdentifierResponse>
    <AlternateEndUserIdentifier>
        <Identifier>DavidAcid</Identifier>
        <CallbackNumber>8042105760</CallbackNumber>
        <E911>
            <CallerName>David</CallerName>
            <Address>
                <HouseNumber>900</HouseNumber>
                <HouseSuffix></HouseSuffix>
                <PreDirectional></PreDirectional>
                <StreetName>MAIN CAMPUS</StreetName>
                <StreetSuffix>DR</StreetSuffix>
                <AddressLine2></AddressLine2>
                <City>RALEIGH</City>
                <StateCode>NC</StateCode>
                <Zip>27606</Zip>
                <PlusFour>5214</PlusFour>
                <Country>United States</Country>
                <AddressType>Billing</AddressType>
            </Address>
            <EmergencyNotificationGroup>
                <Identifier>63865500-0904-46b1-9b4f-7bd237a26363</Identifier>
                <Description>Building 5, 5th Floor.</Description>
            </EmergencyNotificationGroup>
        </E911>
    </AlternateEndUserIdentifier>
</AlternateEndUserIdentifierResponse>";

        public static string createEnpointOrder = @"<EmergencyNotificationEndpointOrder>
    <CustomerOrderId>ALG-31233884</CustomerOrderId>
    <EmergencyNotificationEndpointAssociations>
        <EmergencyNotificationGroup>
            <Identifier>3e9a852e-2d1d-4e2d-84c3-04595ba2eb93</Identifier>
        </EmergencyNotificationGroup>
        <AddedAssociations>
            <EepToEngAssociations>
                <EepTns>
                    <TelephoneNumber>2248838829</TelephoneNumber>
                    <TelephoneNumber>4052397735</TelephoneNumber>
                </EepTns>
                <EepAeuiIds>
                    <Identifier>Fred992834</Identifier>
                    <Identifier>Bob00359</Identifier>
                </EepAeuiIds>
            </EepToEngAssociations>
        </AddedAssociations>
    </EmergencyNotificationEndpointAssociations>
</EmergencyNotificationEndpointOrder>";

        public static string listEndpointOrder = @"<EmergencyNotificationEndpointOrderResponse>
    <Links>
        <first> -- link to first page of results -- </first>
        <next> -- link to next page of results -- </next>
    </Links>
    <EmergencyNotificationEndpointOrders>
        <EmergencyNotificationEndpointOrder>
            <OrderId>3e9a852e-2d1d-4e2d-84c3-87223a78cb70</OrderId>
            <OrderCreatedDate>2020-01-23T18:34:17.284Z</OrderCreatedDate>
            <CreatedBy>jgilmore</CreatedBy>
            <ProcessingStatus>COMPLETED</ProcessingStatus>
            <CustomerOrderId>ALG-31233884</CustomerOrderId>
            <EmergencyNotificationEndpointAssociations>
                <EmergencyNotificationGroup>
                    <Identifier>3e9a852e-2d1d-4e2d-84c3-04595ba2eb93</Identifier>
                </EmergencyNotificationGroup>
                <AddedAssociations>
                    <EepToEngAssociations>
                        <EepTns>
                            <TelephoneNumber>2248838829</TelephoneNumber>
                            <TelephoneNumber>4052397735</TelephoneNumber>
                        </EepTns>
                        <EepAeuiIds>
                            <Identifier>Fred992834</Identifier>
                            <Identifier>Bob00359</Identifier>
                        </EepAeuiIds>
                    </EepToEngAssociations>
                    <ErrorList />
                </AddedAssociations>
            </EmergencyNotificationEndpointAssociations>
        </EmergencyNotificationEndpointOrder>
    </EmergencyNotificationEndpointOrders>
</EmergencyNotificationEndpointOrderResponse>";


        public static string getEndpointOrder = @"<EmergencyNotificationEndpointOrderResponse>
    <EmergencyNotificationEndpointOrder>
        <OrderId>3e9a852e-2d1d-4e2d-84c3-87223a78cb70</OrderId>
        <OrderCreatedDate>2020-01-23T18:34:17.284Z</OrderCreatedDate>
        <CreatedBy>jgilmore</CreatedBy>
        <ProcessingStatus>COMPLETED</ProcessingStatus>
        <CustomerOrderId>ALG-31233884</CustomerOrderId>
        <EmergencyNotificationEndpointAssociations>
            <EmergencyNotificationGroup>
                <Identifier>3e9a852e-2d1d-4e2d-84c3-04595ba2eb93</Identifier>
            </EmergencyNotificationGroup>
            <AddedAssociations>
                <EepToEngAssociations>
                    <EepTns>
                        <TelephoneNumber>2248838829</TelephoneNumber>
                        <TelephoneNumber>4052397735</TelephoneNumber>
                    </EepTns>
                    <EepAeuiIds>
                        <Identifier>Fred992834</Identifier>
                        <Identifier>Bob00359</Identifier>
                    </EepAeuiIds>
                </EepToEngAssociations>
                <ErrorList />
            </AddedAssociations>
        </EmergencyNotificationEndpointAssociations>
    </EmergencyNotificationEndpointOrder>
</EmergencyNotificationEndpointOrderResponse>";

        public static string listGroups = @"<EmergencyNotificationGroupsResponse>
    <Links>
        <first> -- link to first page of results -- </first>
        <next> -- link to next page of results -- </next>
    </Links>
    <EmergencyNotificationGroups>
        <EmergencyNotificationGroup>
            <Identifier>63865500-0904-46b1-9b4f-7bd237a26363</Identifier>
            <CreatedDate>2020-01-23T18:34:17.284Z</CreatedDate>
            <ModifiedBy>jgilmore</ModifiedBy>
            <ModifiedDate>2020-01-23T18:34:17.284Z</ModifiedDate>
            <Description>This is a description of the emergency notification group.</Description>
            <EmergencyNotificationRecipients>
                <EmergencyNotificationRecipient>
                    <Identifier>63865500-0904-46b1-9b4f-7bd237a26363</Identifier>
                </EmergencyNotificationRecipient>
                <EmergencyNotificationRecipient>
                    <Identifier>ef47eb61-e3b1-449d-834b-0fbc5a11da30</Identifier>
                </EmergencyNotificationRecipient>
            </EmergencyNotificationRecipients>
        </EmergencyNotificationGroup>
        <EmergencyNotificationGroup>
            <Identifier>29477382-23947-23c-2349-aa8238b22743</Identifier>
            <CreatedDate>2020-01-23T18:36:51.987Z</CreatedDate>
            <ModifiedBy>jgilmore</ModifiedBy>
            <ModifiedDate>2020-01-23T18:36:51.987Z</ModifiedDate>
            <Description>This is a description of the emergency notification group.</Description>
            <EmergencyNotificationRecipients>
                <EmergencyNotificationRecipient>
                    <Identifier>37742335-8722-3abc-8722-e2434f123a4d</Identifier>
                </EmergencyNotificationRecipient>
            </EmergencyNotificationRecipients>
        </EmergencyNotificationGroup>
    </EmergencyNotificationGroups>
</EmergencyNotificationGroupsResponse>";

        public static string getGroup = @"<EmergencyNotificationGroupsResponse>
    <EmergencyNotificationGroup>
        <Identifier>63865500-0904-46b1-9b4f-7bd237a26363</Identifier>
        <CreatedDate>2020-01-23T18:34:17.284Z</CreatedDate>
        <ModifiedBy>jgilmore</ModifiedBy>
        <ModifiedDate>2020-01-23T18:34:17.284Z</ModifiedDate>
        <Description>This is a description of the emergency notification group.</Description>
        <EmergencyNotificationRecipients>
            <EmergencyNotificationRecipient>
                <Identifier>63865500-0904-46b1-9b4f-7bd237a26363</Identifier>
            </EmergencyNotificationRecipient>
            <EmergencyNotificationRecipient>
                <Identifier>ef47eb61-e3b1-449d-834b-0fbc5a11da30</Identifier>
            </EmergencyNotificationRecipient>
        </EmergencyNotificationRecipients>
    </EmergencyNotificationGroup>
</EmergencyNotificationGroupsResponse>";

        public static string listGroupOrders = @"<EmergencyNotificationGroupOrderResponse>
    <Links>
        <first>Link=&lt;http://localhost:8080/v1.0/accounts/12346371/emergencyNotificationGroupOrders&gt;;rel=""first"";</first>
    </Links>
    <EmergencyNotificationGroupOrders>
        <EmergencyNotificationGroupOrder>
            <OrderId>092815dc-9ced-4d67-a070-a80eb243b914</OrderId>
            <OrderCreatedDate>2020-04-29T15:40:01.449Z</OrderCreatedDate>
            <CreatedBy>systemUser</CreatedBy>
            <ProcessingStatus>COMPLETED</ProcessingStatus>
            <CustomerOrderId>QTWeKMys</CustomerOrderId>
            <AddedEmergencyNotificationGroup>
                <Identifier>6daa55e1-e499-4cf0-9f3d-9524215f1bee</Identifier>
                <Description>enr test description 3</Description>
                <AddedEmergencyNotificationRecipients>
                    <EmergencyNotificationRecipient>
                        <Identifier>44f203915ca249b7b69bbc084af09a</Identifier>
                        <Description>TestDesc SEHsbDMM</Description>
                        <Type>SMS</Type>
                        <Sms>
                            <TelephoneNumber>15638765448</TelephoneNumber>
                        </Sms>
                    </EmergencyNotificationRecipient>
                </AddedEmergencyNotificationRecipients>
            </AddedEmergencyNotificationGroup>
        </EmergencyNotificationGroupOrder>
        <EmergencyNotificationGroupOrder>
            <OrderId>89b4e0a1-2789-43fb-b948-38d368159142</OrderId>
            <OrderCreatedDate>2020-04-29T15:39:59.325Z</OrderCreatedDate>
            <CreatedBy>systemUser</CreatedBy>
            <ProcessingStatus>COMPLETED</ProcessingStatus>
            <CustomerOrderId>SDWupQpf</CustomerOrderId>
            <AddedEmergencyNotificationGroup>
                <Identifier>b49fa543-5bb3-4b9d-9213-96c8b63e77f5</Identifier>
                <Description>enr test description 2</Description>
                <AddedEmergencyNotificationRecipients>
                    <EmergencyNotificationRecipient>
                        <Identifier>c719e060a6ba4212a2c0642b87a784</Identifier>
                        <Description>TestDesc zscxcAGG</Description>
                        <Type>SMS</Type>
                        <Sms>
                            <TelephoneNumber>15678765448</TelephoneNumber>
                        </Sms>
                    </EmergencyNotificationRecipient>
                    <EmergencyNotificationRecipient>
                        <Identifier>93ad72dfe59c4992be6f8aa625466d</Identifier>
                        <Description>TestDesc RTflsKBz</Description>
                        <Type>TTS</Type>
                        <Tts>
                            <TelephoneNumber>17678765449</TelephoneNumber>
                        </Tts>
                    </EmergencyNotificationRecipient>
                </AddedEmergencyNotificationRecipients>
            </AddedEmergencyNotificationGroup>
        </EmergencyNotificationGroupOrder>
        <EmergencyNotificationGroupOrder>
            <OrderId>247d1425-4247-4b27-99d8-83ce30038b14</OrderId>
            <OrderCreatedDate>2020-04-29T15:39:57.058Z</OrderCreatedDate>
            <CreatedBy>systemUser</CreatedBy>
            <ProcessingStatus>COMPLETED</ProcessingStatus>
            <CustomerOrderId>vgshuNMB</CustomerOrderId>
            <AddedEmergencyNotificationGroup>
                <Identifier>69a3d588-f314-42ca-8726-faa824bdf4be</Identifier>
                <Description>eng test description</Description>
                <AddedEmergencyNotificationRecipients>
                    <EmergencyNotificationRecipient>
                        <Identifier>aab78f87074940f1aaaf1c9658be4b</Identifier>
                        <Description>enr test description</Description>
                        <Type>EMAIL</Type>
                        <EmailAddress>testEmail @gmail.com</EmailAddress>
                    </EmergencyNotificationRecipient>
                    <EmergencyNotificationRecipient>
                        <Identifier>852e9eee161b4da6823c91173b05c4</Identifier>
                        <Description>TestDesc WkHqpnNH</Description>
                        <Type>TTS</Type>
                        <Tts>
                            <TelephoneNumber>15678765449</TelephoneNumber>
                        </Tts>
                    </EmergencyNotificationRecipient>
                </AddedEmergencyNotificationRecipients>
            </AddedEmergencyNotificationGroup>
        </EmergencyNotificationGroupOrder>
    </EmergencyNotificationGroupOrders>
</EmergencyNotificationGroupOrderResponse>";

        public static string getGroupOrders = @"<EmergencyNotificationGroupOrderResponse>
    <EmergencyNotificationGroup>
        <OrderId>3e9a852e-2d1d-4e2d-84c3-87223a78cb70</OrderId>
        <OrderCreatedDate>2020-01-23T18:34:17.284Z</OrderCreatedDate>
        <CreatedBy>jgilmore</CreatedBy>
        <ProcessingStatus>COMPLETED</ProcessingStatus>
        <CustomerOrderId>ALG-31233884</CustomerOrderId>
        <AddedEmergencyNotificationGroup>
            <Identifier>63865500-0904-46b1-9b4f-7bd237a26363</Identifier>
            <Description>Building 5, 5th Floor.</Description>
        </AddedEmergencyNotificationGroup>
    </EmergencyNotificationGroup>
</EmergencyNotificationGroupOrderResponse>";

        public static string listRecipients = @"<EmergencyNotificationRecipientsResponse>
    <Links>
        <first> -- link to first page of results -- </first>
        <next> -- link to next page of results -- </next>
    </Links>
    <EmergencyNotificationRecipients>
        <EmergencyNotificationRecipient>
            <Identifier> 63865500-0904-46b1-9b4f-7bd237a26363 </Identifier>
            <CreatedDate>2020-03-18T21:26:47.403Z</CreatedDate>
            <LastModifiedDate>2020-03-18T21:26:47.403Z</LastModifiedDate>
            <ModifiedByUser>jgilmore</ModifiedByUser>
            <Description> This is a description of the emergency notification recipient. </Description>
            <Type>CALLBACK</Type>
            <Callback>
                <Url>https://foo.bar/baz</Url>
                <Credentials>
                    <Username>jgilmore</Username>
                </Credentials>
            </Callback>
        </EmergencyNotificationRecipient>
        <EmergencyNotificationRecipient>
            <Identifier> 63865500-0904-46b1-9b4f-7bd237a26363 </Identifier>
            <CreatedDate>2020-03-22T12:13:25.782Z</CreatedDate>
            <LastModifiedDate>2020-03-22T12:13:25.782Z</LastModifiedDate>
            <ModifiedByUser>gfranklin</ModifiedByUser>
            <Description> This is a description of the emergency notification recipient. </Description>
            <Type>EMAIL</Type>
            <EmailAddress>fred@gmail.com</EmailAddress>
        </EmergencyNotificationRecipient>
        <EmergencyNotificationRecipient>
            <Identifier> 63865500-0904-46b1-9b4f-7bd237a26363 </Identifier>
            <CreatedDate>2020-03-25T17:04:53.042Z</CreatedDate>
            <LastModifiedDate>2020-03-25T17:04:53.042Z</LastModifiedDate>
            <ModifiedByUser>msimpson</ModifiedByUser>
            <Description> This is a description of the emergency notification recipient. </Description>
            <Type>SMS</Type>
            <Sms>
                <TelephoneNumber>12015551212</TelephoneNumber>
            </Sms>
        </EmergencyNotificationRecipient>
        <EmergencyNotificationRecipient>
            <Identifier> 63865500-0904-46b1-9b4f-7bd237a26363 </Identifier>
            <CreatedDate>2020-03-29T20:14:01.736Z</CreatedDate>
            <LastModifiedDate>2020-03-29T20:17:53.294Z</LastModifiedDate>
            <ModifiedByUser>lsimpson</ModifiedByUser>
            <Description> This is a description of the emergency notification recipient. </Description>
            <Type>TTS</Type>
            <Tts>
                <TelephoneNumber>12015551212</TelephoneNumber>
            </Tts>
        </EmergencyNotificationRecipient>
    </EmergencyNotificationRecipients>
</EmergencyNotificationRecipientsResponse>";

        public static string getRecipients = @"<EmergencyNotificationRecipientsResponse>
    <EmergencyNotificationRecipient>
        <Identifier> 63865500-0904-46b1-9b4f-7bd237a26363 </Identifier>
        <CreatedDate>2020-03-18T21:26:47.403Z</CreatedDate>
        <LastModifiedDate>2020-04-01T18:32:22.316Z</LastModifiedDate>
        <ModifiedByUser>jgilmore</ModifiedByUser>
        <Description> This is a description of the emergency notification recipient. </Description>
        <Type>CALLBACK</Type>
        <EmailAddress>fred@gmail.com</EmailAddress>
        <Sms>
            <TelephoneNumber>12015551212</TelephoneNumber>
        </Sms>
        <Tts>
            <TelephoneNumber>12015551212</TelephoneNumber>
        </Tts>
        <Callback>
            <Url>https://foo.bar/baz</Url>
            <Credentials>
                <Username>jgilmore</Username>
                <!-- CallbackPassword is omitted for security -->
            </Credentials>
        </Callback>
    </EmergencyNotificationRecipient>
</EmergencyNotificationRecipientsResponse>";

        public static string listOrders = @"<ResponseSelectWrapper>
    <ListOrderIdUserIdDate>
        <TotalCount>122</TotalCount>
        <Links>
            <first>
                test
            </first>
            <next>
                test2
            </next>
        </Links>
        <OrderIdUserIdDate>
            <CountOfTNs>1</CountOfTNs>
            <lastModifiedDate>2013-12-20T06</lastModifiedDate>
            <OrderDate>2013-12-20T06</OrderDate>
            <orderId>c5d8d076-345c-45d7-87b3-803a35cca89b</orderId>
            <OrderStatus>COMPLETE</OrderStatus>
            <TelephoneNumberDetails>
                <States>
                    <StateWithCount>
                        <State>VA</State>
                        <Count>1</Count>
                    </StateWithCount>
                </States>
                <RateCenters>
                    <RateCenterWithCount>
                        <Count>1</Count>
                        <RateCenter>LADYSMITH</RateCenter>
                    </RateCenterWithCount>
                </RateCenters>
                <Cities>
                    <CityWithCount>
                        <City>LADYSMITH</City>
                        <Count>1</Count>
                    </CityWithCount>
                </Cities>
                <Tiers>
                    <TierWithCount>
                        <Tier>0</Tier>
                        <Count>1</Count>
                    </TierWithCount>
                </Tiers>
                <Vendors>
                    <VendorWithCount>
                        <VendorId>49</VendorId>
                        <VendorName>Bandwidth CLEC</VendorName>
                        <Count>1</Count>
                    </VendorWithCount>
                </Vendors>
            </TelephoneNumberDetails>
            <userId>bwc_user</userId>
        </OrderIdUserIdDate>
        <OrderIdUserIdDate>
            <CountOfTNs>0</CountOfTNs>
            <lastModifiedDate>2013-11-05T17</lastModifiedDate>
            <OrderDate>2013-11-05T17</OrderDate>
            <orderId>27da9f39-81f3-44ed-80ce-05ddf2db612d</orderId>
            <OrderStatus>FAILED</OrderStatus>
            <userId>wandedemo_user</userId>
        </OrderIdUserIdDate>
        <OrderIdUserIdDate>
            <CountOfTNs>1</CountOfTNs>
            <lastModifiedDate>2013-12-11T20</lastModifiedDate>
            <OrderDate>2013-12-11T20</OrderDate>
            <orderId>2bab589e-2cda-453b-9999-8f35441d4a1a</orderId>
            <OrderStatus>COMPLETE</OrderStatus>
            <userId>bwc_user</userId>
        </OrderIdUserIdDate>
    </ListOrderIdUserIdDate>
</ResponseSelectWrapper>";

        public static string loasFileUploadResponse = @"<fileUploadResponse>
    <filename>63097af1-37ae-432f-8a0d-9b0e6517a35b-1429550165581.pdf</filename>
    <resultCode>0</resultCode>
    <resultMessage>LOA file uploaded successfully for order 63097af1-37ae-432f-8a0d-9b0e6517a35b</resultMessage>
</fileUploadResponse>";

        public static string loasFileListResponse = @"<fileListResponse>
    <fileCount>2</fileCount>
    <fileNames>803f3cc5-beae-469e-bd65-e9891ccdffb9-1092874634747.pdf</fileNames>
    <fileNames>803f3cc5-beae-469e-bd65-e9891ccdffb9-1430814967669.pdf</fileNames>
    <resultCode>0</resultCode>
    <resultMessage>LOA file list successfully returned</resultMessage>
</fileListResponse>";

        public static string notesResponse2 = @"<Notes>
    <Note>
        <Id>87037</Id>
        <UserId>jbm</UserId>
        <Description>This is a test note</Description>
        <LastDateModifier>2014-11-16T04:01:10.000Z</LastDateModifier>
    </Note>
    <Note>
        <Id>87039</Id>
        <UserId>smckinnon</UserId>
        <Description>This is a second test note</Description>
        <LastDateModifier>2014-11-16T04:08:46.000Z</LastDateModifier>
    </Note>
</Notes>";

        public static string csrResponse = @"<CsrResponse>
    <CustomerOrderId>TEST BWDB-6506</CustomerOrderId>
    <LastModifiedBy>systemUser</LastModifiedBy>
    <OrderCreateDate>2020-01-13T21:14:35Z</OrderCreateDate>
    <AccountId>14</AccountId>
    <OrderId>5c3b8240-52b5-45a5-8d7d-42a71ebcd1ba</OrderId>
    <LastModifiedDate>2020-01-13T16:51:21.920Z</LastModifiedDate>
    <Status>COMPLETE</Status>
    <AccountNumber>987654321</AccountNumber>
    <AccountTelephoneNumber>9196194444</AccountTelephoneNumber>
    <EndUserName>bandwidthGuy</EndUserName>
    <AuthorizingUserName>importantAuthGuy</AuthorizingUserName>
    <CustomerCode>123</CustomerCode>
    <EndUserPIN>12345</EndUserPIN>
    <EndUserPassword>enduserpassword123</EndUserPassword>
    <AddressLine1>900 Main Campus Dr</AddressLine1>
    <City>Raleigh</City>
    <State>NC</State>
    <ZIPCode>27612</ZIPCode>
    <TypeOfService>residential</TypeOfService>
    <CsrData>
        <AccountNumber>123456789</AccountNumber>
        <CustomerName>JOHN SMITH</CustomerName>
        <ServiceAddress>
            <UnparsedAddress>900 MAIN CAMPUS DR</UnparsedAddress>
            <City>RALEIGH</City>
            <State>NC</State>
            <Zip>27616</Zip>
        </ServiceAddress>
        <WorkingTelephoneNumber>9196191156</WorkingTelephoneNumber>
        <WorkingTelephoneNumbersOnAccount>
            <TelephoneNumber>9196191156</TelephoneNumber>
        </WorkingTelephoneNumbersOnAccount>
    </CsrData>
</CsrResponse>";

        public static string csrRequest = @"<Csr>
    <AccountNumber>987654321</AccountNumber>
    <AccountTelephoneNumber>9196194444</AccountTelephoneNumber>
    <EndUserName>bandwidthGuy</EndUserName>
    <AuthorizingUserName>importantAuthGuy</AuthorizingUserName>
    <CustomerCode>123</CustomerCode>
    <EndUserPIN>12345</EndUserPIN>
    <EndUserPassword>enduserpassword123</EndUserPassword>
    <AddressLine1>900 Main Campus Dr</AddressLine1>
    <City>Raleigh</City>
    <State>NC</State>
    <ZIPCode>27612</ZIPCode>
    <TypeOfService>residential</TypeOfService>
    <Status>REQUESTED_CANCEL</Status>
</Csr>";


        public static string ImportTnCheckerPayload = @"<ImportTnCheckerPayload>
                                                            <TelephoneNumbers>
                                                                <TelephoneNumber>3032281000</TelephoneNumber>
                                                            </TelephoneNumbers>
                                                            <ImportTnErrors>
                                                                <ImportTnError>
                                                                    <Code>19006</Code>
                                                                    <Description>Bandwidth numbers cannot be imported by this account at this time.</Description>
                                                                    <TelephoneNumbers>
                                                                        <TelephoneNumber>4109235436</TelephoneNumber>
                                                                        <TelephoneNumber>4104685864</TelephoneNumber>
                                                                    </TelephoneNumbers>
                                                                </ImportTnError>
                                                            </ImportTnErrors>
                                                        </ImportTnCheckerPayload>";

        public static string ImportTnCheckerResponse = $"<ImportTnCheckerResponse>{ImportTnCheckerPayload}</ImportTnCheckerResponse>";

        public static string RemoveImportedOrder = @"<RemoveImportedTnOrder>
                                                        <CustomerOrderId>SJM000001</CustomerOrderId>
                                                        <OrderCreateDate>2018-01-20T02:59:54.000Z</OrderCreateDate>
                                                        <AccountId>9900012</AccountId>
                                                        <CreatedByUser>smckinnon</CreatedByUser>
                                                        <OrderId>b05de7e6-0cab-4c83-81bb-9379cba8efd0</OrderId>
                                                        <LastModifiedDate>2018-01-20T02:59:54.000Z</LastModifiedDate>
                                                        <TelephoneNumbers>
                                                            <TelephoneNumber>9199918388</TelephoneNumber>
                                                            <TelephoneNumber>4158714245</TelephoneNumber>
                                                            <TelephoneNumber>4352154439</TelephoneNumber>
                                                            <TelephoneNumber>4352154466</TelephoneNumber>
                                                        </TelephoneNumbers>
                                                        <ProcessingStatus>PROCESSING</ProcessingStatus>
                                                        <Errors/>
                                                    </RemoveImportedTnOrder>";

        public static string RemoveImportedOrderResponse = $"<RemoveImportedTnOrderResponse>{RemoveImportedOrder}</RemoveImportedTnOrderResponse>";

        public static string RemoveImportedTnOrders = @"<RemoveImportedTnOrders>
                                                            <TotalCount>7</TotalCount>
                                                            <RemoveImportedTnOrderSummary>
                                                                <accountId>9900778</accountId>
                                                                <CountOfTNs>2</CountOfTNs>
                                                                <CustomerOrderId>custom string</CustomerOrderId>
                                                                <userId>jmulford-api</userId>
                                                                <lastModifiedDate>2020-02-03T18:08:44.256Z</lastModifiedDate>
                                                                <OrderDate>2020-02-03T18:08:44.199Z</OrderDate>
                                                                <OrderType>remove_imported_tn_orders</OrderType>
                                                                <OrderStatus>FAILED</OrderStatus>
                                                                <OrderId>5bb3b642-cbbb-4438-9a44-56069550d603</OrderId>
                                                            </RemoveImportedTnOrderSummary>
                                                            <RemoveImportedTnOrderSummary>
                                                                <accountId>9900778</accountId>
                                                                <CountOfTNs>2</CountOfTNs>
                                                                <CustomerOrderId>custom string</CustomerOrderId>
                                                                <userId>jmulford-api</userId>
                                                                <lastModifiedDate>2020-02-03T18:08:19.955Z</lastModifiedDate>
                                                                <OrderDate>2020-02-03T18:08:19.927Z</OrderDate>
                                                                <OrderType>remove_imported_tn_orders</OrderType>
                                                                <OrderStatus>FAILED</OrderStatus>
                                                                <OrderId>384ff500-ff33-4580-a910-45eff3d51f0d</OrderId>
                                                            </RemoveImportedTnOrderSummary>
                                                            <RemoveImportedTnOrderSummary>
                                                                <accountId>9900778</accountId>
                                                                <CountOfTNs>2</CountOfTNs>
                                                                <CustomerOrderId>custom string</CustomerOrderId>
                                                                <userId>jmulford-api</userId>
                                                                <lastModifiedDate>2020-02-03T15:51:14.496Z</lastModifiedDate>
                                                                <OrderDate>2020-02-03T15:51:14.471Z</OrderDate>
                                                                <OrderType>remove_imported_tn_orders</OrderType>
                                                                <OrderStatus>FAILED</OrderStatus>
                                                                <OrderId>20943d4a-600c-44e0-ac97-dd3d6f1f2af5</OrderId>
                                                            </RemoveImportedTnOrderSummary>
                                                            <RemoveImportedTnOrderSummary>
                                                                <accountId>9900778</accountId>
                                                                <CountOfTNs>2</CountOfTNs>
                                                                <CustomerOrderId>custom string</CustomerOrderId>
                                                                <userId>jmulford-api</userId>
                                                                <lastModifiedDate>2020-02-03T15:48:28.440Z</lastModifiedDate>
                                                                <OrderDate>2020-02-03T15:48:28.418Z</OrderDate>
                                                                <OrderType>remove_imported_tn_orders</OrderType>
                                                                <OrderStatus>FAILED</OrderStatus>
                                                                <OrderId>d3ed8a2e-7927-4fbc-8e6c-9c8408d443d5</OrderId>
                                                            </RemoveImportedTnOrderSummary>
                                                            <RemoveImportedTnOrderSummary>
                                                                <accountId>9900778</accountId>
                                                                <CountOfTNs>2</CountOfTNs>
                                                                <CustomerOrderId>custom string</CustomerOrderId>
                                                                <userId>jmulford-api</userId>
                                                                <lastModifiedDate>2020-01-31T21:12:23.731Z</lastModifiedDate>
                                                                <OrderDate>2020-01-31T21:12:23.707Z</OrderDate>
                                                                <OrderType>remove_imported_tn_orders</OrderType>
                                                                <OrderStatus>FAILED</OrderStatus>
                                                                <OrderId>6ddec230-ca5f-4502-8273-15ba8968dc8c</OrderId>
                                                            </RemoveImportedTnOrderSummary>
                                                            <RemoveImportedTnOrderSummary>
                                                                <accountId>9900778</accountId>
                                                                <CountOfTNs>2</CountOfTNs>
                                                                <CustomerOrderId>custom string</CustomerOrderId>
                                                                <userId>jmulford-api</userId>
                                                                <lastModifiedDate>2020-01-31T21:12:08.200Z</lastModifiedDate>
                                                                <OrderDate>2020-01-31T21:12:08.183Z</OrderDate>
                                                                <OrderType>remove_imported_tn_orders</OrderType>
                                                                <OrderStatus>FAILED</OrderStatus>
                                                                <OrderId>dc2ae1c7-b7db-44e7-bbb2-eb2b17e18413</OrderId>
                                                            </RemoveImportedTnOrderSummary>
                                                            <RemoveImportedTnOrderSummary>
                                                                <accountId>9900778</accountId>
                                                                <CountOfTNs>2</CountOfTNs>
                                                                <CustomerOrderId>custom string</CustomerOrderId>
                                                                <userId>jmulford-api</userId>
                                                                <lastModifiedDate>2020-01-31T21:11:46.368Z</lastModifiedDate>
                                                                <OrderDate>2020-01-31T21:11:46.343Z</OrderDate>
                                                                <OrderType>remove_imported_tn_orders</OrderType>
                                                                <OrderStatus>FAILED</OrderStatus>
                                                                <OrderId>1bcfe0bd-6998-4198-b734-abd1fffe346a</OrderId>
                                                            </RemoveImportedTnOrderSummary>
                                                        </RemoveImportedTnOrders>";

        public static string OrderHistoryWrapper = @"<OrderHistoryWrapper>
                                                        <OrderHistory>
                                                            <OrderDate>2020-02-04T14:09:07.824Z</OrderDate>
                                                            <Note>Import TN order has been received by the system.</Note>
                                                            <Author>jmulford-api</Author>
                                                            <Status>received</Status>
                                                        </OrderHistory>
                                                        <OrderHistory>
                                                            <OrderDate>2020-02-04T14:09:08.937Z</OrderDate>
                                                            <Note>Import TN order has failed.</Note>
                                                            <Author>jmulford-api</Author>
                                                            <Status>failed</Status>
                                                        </OrderHistory>
                                                    </OrderHistoryWrapper>";

        public static string ImportTnOrders = @"<ImportTnOrders>
                                                    <TotalCount>14</TotalCount>
                                                    <ImportTnOrderSummary>
                                                        <accountId>9900778</accountId>
                                                        <CountOfTNs>1</CountOfTNs>
                                                        <CustomerOrderId>id</CustomerOrderId>
                                                        <userId>jmulford-api</userId>
                                                        <lastModifiedDate>2020-02-04T14:09:08.937Z</lastModifiedDate>
                                                        <OrderDate>2020-02-04T14:09:07.824Z</OrderDate>
                                                        <OrderType>import_tn_orders</OrderType>
                                                        <OrderStatus>FAILED</OrderStatus>
                                                        <OrderId>fbd17609-be44-48e7-a301-90bd6cf42248</OrderId>
                                                    </ImportTnOrderSummary>
                                                    <ImportTnOrderSummary>
                                                        <accountId>9900778</accountId>
                                                        <CountOfTNs>1</CountOfTNs>
                                                        <CustomerOrderId>id</CustomerOrderId>
                                                        <userId>jmulford-api</userId>
                                                        <lastModifiedDate>2020-02-03T18:08:43.246Z</lastModifiedDate>
                                                        <OrderDate>2020-02-03T18:08:43.220Z</OrderDate>
                                                        <OrderType>import_tn_orders</OrderType>
                                                        <OrderStatus>FAILED</OrderStatus>
                                                        <OrderId>03012d35-f4ef-495d-9d2b-f05f60a98995</OrderId>
                                                    </ImportTnOrderSummary>
                                                    <ImportTnOrderSummary>
                                                        <accountId>9900778</accountId>
                                                        <CountOfTNs>1</CountOfTNs>
                                                        <CustomerOrderId>id</CustomerOrderId>
                                                        <userId>jmulford-api</userId>
                                                        <lastModifiedDate>2020-02-03T18:08:18.968Z</lastModifiedDate>
                                                        <OrderDate>2020-02-03T18:08:18.941Z</OrderDate>
                                                        <OrderType>import_tn_orders</OrderType>
                                                        <OrderStatus>FAILED</OrderStatus>
                                                        <OrderId>dde545c2-fab7-4f09-ba05-94270dc846c6</OrderId>
                                                    </ImportTnOrderSummary>
                                                    <ImportTnOrderSummary>
                                                        <accountId>9900778</accountId>
                                                        <CountOfTNs>1</CountOfTNs>
                                                        <CustomerOrderId>id</CustomerOrderId>
                                                        <userId>jmulford-api</userId>
                                                        <lastModifiedDate>2020-02-03T18:07:33.833Z</lastModifiedDate>
                                                        <OrderDate>2020-02-03T18:07:33.783Z</OrderDate>
                                                        <OrderType>import_tn_orders</OrderType>
                                                        <OrderStatus>FAILED</OrderStatus>
                                                        <OrderId>b6ce414c-efec-4cb7-878f-e55c5a1bb60a</OrderId>
                                                    </ImportTnOrderSummary>
                                                    <ImportTnOrderSummary>
                                                        <accountId>9900778</accountId>
                                                        <CountOfTNs>1</CountOfTNs>
                                                        <CustomerOrderId>id</CustomerOrderId>
                                                        <userId>jmulford-api</userId>
                                                        <lastModifiedDate>2020-02-03T18:07:09.875Z</lastModifiedDate>
                                                        <OrderDate>2020-02-03T18:07:09.831Z</OrderDate>
                                                        <OrderType>import_tn_orders</OrderType>
                                                        <OrderStatus>FAILED</OrderStatus>
                                                        <OrderId>a49cf67e-70d1-4239-8de8-47e5071c0f5a</OrderId>
                                                    </ImportTnOrderSummary>
                                                    <ImportTnOrderSummary>
                                                        <accountId>9900778</accountId>
                                                        <CountOfTNs>1</CountOfTNs>
                                                        <CustomerOrderId>id</CustomerOrderId>
                                                        <userId>jmulford-api</userId>
                                                        <lastModifiedDate>2020-02-03T18:06:31.635Z</lastModifiedDate>
                                                        <OrderDate>2020-02-03T18:06:31.595Z</OrderDate>
                                                        <OrderType>import_tn_orders</OrderType>
                                                        <OrderStatus>FAILED</OrderStatus>
                                                        <OrderId>589aecb2-e25e-42ca-94b6-3b6095ab0e24</OrderId>
                                                    </ImportTnOrderSummary>
                                                    <ImportTnOrderSummary>
                                                        <accountId>9900778</accountId>
                                                        <CountOfTNs>1</CountOfTNs>
                                                        <CustomerOrderId>id</CustomerOrderId>
                                                        <userId>jmulford-api</userId>
                                                        <lastModifiedDate>2020-02-03T18:06:11.904Z</lastModifiedDate>
                                                        <OrderDate>2020-02-03T18:06:11.866Z</OrderDate>
                                                        <OrderType>import_tn_orders</OrderType>
                                                        <OrderStatus>FAILED</OrderStatus>
                                                        <OrderId>2f6fdf1f-2288-4a6e-b7fa-d9900902059e</OrderId>
                                                    </ImportTnOrderSummary>
                                                    <ImportTnOrderSummary>
                                                        <accountId>9900778</accountId>
                                                        <CountOfTNs>1</CountOfTNs>
                                                        <CustomerOrderId>id</CustomerOrderId>
                                                        <userId>jmulford-api</userId>
                                                        <lastModifiedDate>2020-02-03T18:05:58.826Z</lastModifiedDate>
                                                        <OrderDate>2020-02-03T18:05:58.796Z</OrderDate>
                                                        <OrderType>import_tn_orders</OrderType>
                                                        <OrderStatus>FAILED</OrderStatus>
                                                        <OrderId>1691c7c0-53a5-4196-b46a-02b92f278bc5</OrderId>
                                                    </ImportTnOrderSummary>
                                                    <ImportTnOrderSummary>
                                                        <accountId>9900778</accountId>
                                                        <CountOfTNs>1</CountOfTNs>
                                                        <CustomerOrderId>id</CustomerOrderId>
                                                        <userId>jmulford-api</userId>
                                                        <lastModifiedDate>2020-02-03T18:05:31.226Z</lastModifiedDate>
                                                        <OrderDate>2020-02-03T18:05:31.189Z</OrderDate>
                                                        <OrderType>import_tn_orders</OrderType>
                                                        <OrderStatus>FAILED</OrderStatus>
                                                        <OrderId>5ab5ef80-f14d-47c9-b612-764120ccdcb0</OrderId>
                                                    </ImportTnOrderSummary>
                                                    <ImportTnOrderSummary>
                                                        <accountId>9900778</accountId>
                                                        <CountOfTNs>1</CountOfTNs>
                                                        <CustomerOrderId>id</CustomerOrderId>
                                                        <userId>jmulford-api</userId>
                                                        <lastModifiedDate>2020-02-03T18:04:19.615Z</lastModifiedDate>
                                                        <OrderDate>2020-02-03T18:04:19.569Z</OrderDate>
                                                        <OrderType>import_tn_orders</OrderType>
                                                        <OrderStatus>FAILED</OrderStatus>
                                                        <OrderId>d0574c61-368a-49e9-91df-e95fcec6216a</OrderId>
                                                    </ImportTnOrderSummary>
                                                    <ImportTnOrderSummary>
                                                        <accountId>9900778</accountId>
                                                        <CountOfTNs>1</CountOfTNs>
                                                        <CustomerOrderId>id</CustomerOrderId>
                                                        <userId>jmulford-api</userId>
                                                        <lastModifiedDate>2020-02-03T18:03:20.888Z</lastModifiedDate>
                                                        <OrderDate>2020-02-03T18:03:20.852Z</OrderDate>
                                                        <OrderType>import_tn_orders</OrderType>
                                                        <OrderStatus>FAILED</OrderStatus>
                                                        <OrderId>c3de02b3-9215-408f-870f-ceff2ce7bdc8</OrderId>
                                                    </ImportTnOrderSummary>
                                                    <ImportTnOrderSummary>
                                                        <accountId>9900778</accountId>
                                                        <CountOfTNs>1</CountOfTNs>
                                                        <CustomerOrderId>id</CustomerOrderId>
                                                        <userId>jmulford-api</userId>
                                                        <lastModifiedDate>2020-02-03T18:01:42.214Z</lastModifiedDate>
                                                        <OrderDate>2020-02-03T18:01:42.152Z</OrderDate>
                                                        <OrderType>import_tn_orders</OrderType>
                                                        <OrderStatus>FAILED</OrderStatus>
                                                        <OrderId>9796b4f6-90c7-4265-9919-e0bbaa42453b</OrderId>
                                                    </ImportTnOrderSummary>
                                                    <ImportTnOrderSummary>
                                                        <accountId>9900778</accountId>
                                                        <CountOfTNs>1</CountOfTNs>
                                                        <CustomerOrderId>id</CustomerOrderId>
                                                        <userId>jmulford-api</userId>
                                                        <lastModifiedDate>2020-01-31T21:04:03.284Z</lastModifiedDate>
                                                        <OrderDate>2020-01-31T21:04:03.244Z</OrderDate>
                                                        <OrderType>import_tn_orders</OrderType>
                                                        <OrderStatus>FAILED</OrderStatus>
                                                        <OrderId>3cfcc5c3-230e-4ef6-9aec-64d5066dbaae</OrderId>
                                                    </ImportTnOrderSummary>
                                                    <ImportTnOrderSummary>
                                                        <accountId>9900778</accountId>
                                                        <CountOfTNs>1</CountOfTNs>
                                                        <CustomerOrderId>jmulford</CustomerOrderId>
                                                        <userId>jmulford-api</userId>
                                                        <lastModifiedDate>2020-01-31T20:49:54.560Z</lastModifiedDate>
                                                        <OrderDate>2020-01-31T20:49:54.511Z</OrderDate>
                                                        <OrderType>import_tn_orders</OrderType>
                                                        <OrderStatus>FAILED</OrderStatus>
                                                        <OrderId>e5d2cd6d-664d-40ac-8c45-ff9916fcb8e0</OrderId>
                                                    </ImportTnOrderSummary>
                                                </ImportTnOrders>";

        public static string ImportTnOrder = @"<ImportTnOrder>
                                                <CustomerOrderId>SJM000001</CustomerOrderId>
                                                <OrderCreateDate>2018-01-20T02:59:54.000Z</OrderCreateDate>
                                                <AccountId>9900012</AccountId>
                                                <CreatedByUser>smckinnon</CreatedByUser>
                                                <OrderId>b05de7e6-0cab-4c83-81bb-9379cba8efd0</OrderId>
                                                <LastModifiedDate>2018-01-20T02:59:54.000Z</LastModifiedDate>
                                                <SiteId>202</SiteId>
                                                <SipPeerId>520565</SipPeerId>
                                                <Subscriber>
                                                    <Name>ABC Inc.</Name>
                                                    <ServiceAddress>
                                                        <HouseNumber>11235</HouseNumber>
                                                        <StreetName>Back</StreetName>
                                                        <City>Denver</City>
                                                        <StateCode>CO</StateCode>
                                                        <Zip>27541</Zip>
                                                        <County>Canyon</County>
                                                    </ServiceAddress>
                                                </Subscriber>
                                                <LoaAuthorizingPerson>The Authguy</LoaAuthorizingPerson>
                                                <TelephoneNumbers>
                                                    <TelephoneNumber>9199918388</TelephoneNumber>
                                                    <TelephoneNumber>4158714245</TelephoneNumber>
                                                    <TelephoneNumber>4352154439</TelephoneNumber>
                                                    <TelephoneNumber>4352154466</TelephoneNumber>
                                                </TelephoneNumbers>
                                                <ProcessingStatus>PROCESSING</ProcessingStatus>
                                                <Errors/>
                                                </ImportTnOrder>";

        public static string ImportTnOrderResponse = $@"<ImportTnOrderResponse>
                                                            {ImportTnOrder}
                                                       </ImportTnOrderResponse>";

        
        public static string SiteWithAddressPostDirectional = @"<SiteResponse>
                                                                    <Site>
                                                                        <Name>Raleigh</Name>
                                                                        <Description>Test Gateway</Description>
                                                                        <CustomerName>BW</CustomerName>
                                                                        <Address>
                                                                            <HouseNumber>1600</HouseNumber>
                                                                            <StreetName>PENNSYLVANIA</StreetName>
                                                                            <StreetSuffix>AVE</StreetSuffix>
                                                                            <PostDirectional>NW</PostDirectional>
                                                                            <City>WASHINGTON</City>
                                                                            <StateCode>DC</StateCode>
                                                                            <Zip>20006</Zip>
                                                                            <Country>US</Country>
                                                                        </Address>
                                                                        <UcTrunkingConfiguration>
                                                                            <Type>Seats</Type>
                                                                            <UsageCategory>UC500</UsageCategory>
                                                                        </UcTrunkingConfiguration>
                                                                    </Site>
                                                                </SiteResponse>";

        public static string RemoveApplicationResponse = "<ApplicationsSettings>remove</ApplicationsSettings>";

        public static string ApplicationSettings = @"<ApplicationsSettings>
                                                        <HttpMessagingV2AppId>4a4ca6c1-156b-4fca-84e9-34e35e2afc32</HttpMessagingV2AppId>
                                                    </ApplicationsSettings>";

        public static string ApplicationSettingsResponse = $@"<ApplicationsSettingsResponse>
                                                                    {ApplicationSettings}
                                                              </ApplicationsSettingsResponse>";

        public static string MmsFeature = @"<MmsFeature>
                                                <MmsSettings>
                                                    <Protocol>MM4</Protocol>
                                                </MmsSettings>
                                                <Protocols>
                                                    <MM4>
                                                        <!--Tls element is optional. If not included default value OFF will be used.-->
                                                        <Tls>OFF</Tls>
                                                        <MmsMM4TermHosts>
                                                            <TermHosts>
                                                                <TermHost>
                                                                    <HostName>206.107.248.58</HostName>
                                                                </TermHost>
                                                            </TermHosts>
                                                        </MmsMM4TermHosts>
                                                        <MmsMM4OrigHosts>
                                                            <OrigHosts>
                                                                <OrigHost>
                                                                    <!--If Tls is ON you can pass only FQDN as hostname. If Tls is OFF you can send either FQDN or IP address.-->
                                                                    <HostName>30.239.72.55</HostName>
                                                                    <!--Port element is optional. If not included port will be set based on TLS value (25 for OFF and 587 for ON).-->
                                                                    <Port>8726</Port>
                                                                    <Priority>0</Priority>
                                                                </OrigHost>
                                                                <OrigHost>
                                                                    <HostName>25.231.123.32</HostName>
                                                                    <Priority>0</Priority>
                                                                </OrigHost>
                                                            </OrigHosts>
                                                        </MmsMM4OrigHosts>
                                                    </MM4>
                                                </Protocols>
                                            </MmsFeature>";

        public static string MmsFeatureResponse = $@"<MmsFeatureResponse>
                                                        {MmsFeature}
                                                    </MmsFeatureResponse>";



        public static string SipPeerSmsFeature = @"<SipPeerSmsFeature>
                                                    <SipPeerSmsFeatureSettings>
                                                        <TollFree>true</TollFree>
                                                        <ShortCode>true</ShortCode>
                                                        <A2pLongCode>DefaultOff</A2pLongCode>
                                                        <A2pMessageClass>SomeMessageClass</A2pMessageClass>
                                                        <A2pCampaignId>SomeCampaignId</A2pCampaignId>
                                                        <Protocol>SMPP</Protocol>
                                                        <Zone1>true</Zone1>
                                                        <Zone2>true</Zone2>
                                                        <Zone3>true</Zone3>
                                                        <Zone4>true</Zone4>
                                                        <Zone5>true</Zone5>
                                                    </SipPeerSmsFeatureSettings>
                                                    <SmppHosts>
                                                        <SmppHost>
                                                            <HostName>54.10.88.146</HostName>
                                                            <Priority>0</Priority>
                                                            <ConnectionType>RECEIVER_ONLY</ConnectionType>
                                                        </SmppHost>
                                                        <SmppHost>
                                                            <HostName>47.123.17.16/30</HostName>
                                                            <Priority>0</Priority>
                                                            <ConnectionType>RECEIVER_ONLY</ConnectionType>
                                                        </SmppHost>
                                                    </SmppHosts>
                                                </SipPeerSmsFeature>";

        public static string SipPeerSmsFeatureResponse = $@"<SipPeerSmsFeatureResponse>
                                                                {SipPeerSmsFeature}
                                                            </SipPeerSmsFeatureResponse>";

        public static string SipPeerTerminationSettings = @"<SipPeerTerminationSettings>
                                                                <VoiceProtocol>HTTP</VoiceProtocol>
                                                                <HttpSettings>
                                                                    <HttpVoiceV2AppId>469ebbac-4459-4d98-bc19-a038960e787f</HttpVoiceV2AppId>
                                                                </HttpSettings>
                                                            </SipPeerTerminationSettings>";

        public static string SipPeerTerminationSettingResponse = $@"<SipPeerTerminationSettingsResponse>
                                                                        {SipPeerTerminationSettings}
                                                                    </SipPeerTerminationSettingsResponse>";

        public static string SipPeerOriginationSettings = @"<SipPeerOriginationSettings>
                                                                <VoiceProtocol>HTTP</VoiceProtocol>
                                                                <HttpSettings>
                                                                    <HttpVoiceV2AppId>469ebbac-4459-4d98-bc19-a038960e787f</HttpVoiceV2AppId>
                                                                </HttpSettings>
                                                            </SipPeerOriginationSettings>";

        public static string SipPeerOriginationSettingsResponse = $@"<SipPeerOriginationSettingsResponse>
                                                                        {SipPeerOriginationSettings}
                                                                    </SipPeerOriginationSettingsResponse>";

        public static string multiApplicationProvisionResponse = @"<ApplicationProvisioningResponse xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
                                                                        <ApplicationList>
                                                                        <Application>
                                                                            <ApplicationId>2cfcb382-161c-46d4-8c67-87ca09a72c85</ApplicationId>
                                                                            <ServiceType>Messaging-V2</ServiceType>
                                                                            <AppName>app1</AppName>
                                                                            <MsgCallbackUrl>http://a.com</MsgCallbackUrl>
                                                                        </Application>
                                                                        <Application>
                                                                            <ApplicationId>0cb0112b-5998-4c81-999a-0d3fb5e3f8e2</ApplicationId>
                                                                            <ServiceType>Voice-V2</ServiceType>
                                                                            <AppName>app2</AppName>
                                                                            <MsgCallbackUrl>http://b.com</MsgCallbackUrl>
                                                                            <CallbackCreds>
                                                                            <UserId>15jPWZmXdm</UserId>
                                                                            </CallbackCreds>
                                                                        </Application>
                                                                        </ApplicationList>
                                                                    </ApplicationProvisioningResponse>";

        public static string singleApplicationProvisionResponse = @"<ApplicationProvisioningResponse  xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
                                                                      <Application>
                                                                        <ApplicationId>d3e418e9-1833-49c1-b6c7-ca1700f79586</ApplicationId>
                                                                        <ServiceType>Voice-V2</ServiceType>
                                                                        <AppName>v1</AppName>
                                                                        <CallbackCreds>
                                                                          <UserId>login123</UserId>
                                                                        </CallbackCreds>
                                                                        <CallStatusMethod>GET</CallStatusMethod>
                                                                        <CallInitiatedMethod>GET</CallInitiatedMethod>
                                                                        <CallInitiatedCallbackUrl>https://a.com</CallInitiatedCallbackUrl>
                                                                        <CallStatusCallbackUrl>https://b.com</CallStatusCallbackUrl>
                                                                      </Application>
                                                                    </ApplicationProvisioningResponse>";

        public static string singleApplicationRequest = @"<Application>
                                                            <ApplicationId>d3e418e9-1833-49c1-b6c7-ca1700f79586</ApplicationId>
                                                            <ServiceType>Voice-V2</ServiceType>
                                                            <AppName>v1</AppName>
                                                            <CallbackCreds>
                                                                <UserId>login123</UserId>
                                                            </CallbackCreds>
                                                            <CallStatusMethod>GET</CallStatusMethod>
                                                            <CallInitiatedMethod>GET</CallInitiatedMethod>
                                                            <CallInitiatedCallbackUrl>https://a.com</CallInitiatedCallbackUrl>
                                                            <CallStatusCallbackUrl>https://b.com</CallStatusCallbackUrl>
                                                          </Application>";

        public static string associatedSipPeerResponse = @"<AssociatedSipPeersResponse xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
                                                                <AssociatedSipPeers>
                                                                    <AssociatedSipPeer>
                                                                        <SiteId>13651</SiteId>
                                                                        <SiteName>Prod Sub-account</SiteName>
                                                                        <PeerId>540341</PeerId>
                                                                        <PeerName>Prod</PeerName>
                                                                    </AssociatedSipPeer>
                                                                    <AssociatedSipPeer>
                                                                        <SiteId>13622</SiteId>
                                                                        <SiteName>Dev Sub-zccount</SiteName>
                                                                        <PeerId>540349</PeerId>
                                                                        <PeerName>Dev</PeerName>
                                                                    </AssociatedSipPeer>
                                                                </AssociatedSipPeers>
                                                            </AssociatedSipPeersResponse>";

        public static string associatedSipPeerResponse400 = @"<AssociatedSipPeersResponse xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
                                                                <ResponseStatus>
                                                                    <ErrorCode>12103</ErrorCode>
                                                                    <Description> Current 1 Account have no Catapult association </Description>
                                                                </ResponseStatus>
                                                            </AssociatedSipPeersResponse>";

        public static string associatedSipPeerResponse404 = @"<AssociatedSipPeersResponse xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
                                                                <ResponseStatus>
                                                                    <ErrorCode>13629</ErrorCode>
                                                                    <Description> Application with id 'non_existing' not found </Description>
                                                                </ResponseStatus>
                                                            </AssociatedSipPeersResponse>";

        public static string xmlError = " <?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?><SiteResponse><ResponseStatus><ErrorCode>12112</ErrorCode><Description>The verifiable address closest to the submitted access is included below. Please use that street address or another valid street address in your next request</Description></ResponseStatus><AddressErrorDescription><AddressInconsistencies>Some adjustments are required to allow the address to pass geocoding:Specified value - Street Suffix : \"PARKWAY\" Valid value - \"PKWY\"</AddressInconsistencies><RecommendedAddress><AddressLine1>320 DIVERSEY PKWY</AddressLine1><HouseNumber>320</HouseNumber><StreetName>DIVERSEY</StreetName><StreetSuffix>PKWY</StreetSuffix><City>WEST CHICAGO</City><StateCode>IL</StateCode><Zip>60185</Zip><PlusFour>6204</PlusFour><Country>US</Country></RecommendedAddress></AddressErrorDescription><Site><Name>Csharp Test Site</Name><Description>A site from the C# Example</Description><Address><HouseNumber>320</HouseNumber><StreetName>Diversey Parkway</StreetName><City>West Chicago</City><StateCode>IL</StateCode><Zip>60185</Zip><AddressType>Service</AddressType></Address></Site></SiteResponse>";
        public static string xmlLNPResponseWrapper = "<LNPResponseWrapper><TotalCount>3176</TotalCount><Links><first> -- link -- </first><next> -- link -- </next></Links><lnpPortInfoForGivenStatus><CountOfTNs>1</CountOfTNs><userId>Neustar</userId><lastModifiedDate>2014-11-21T14:00:33.836Z</lastModifiedDate><OrderDate>2014-11-05T19:34:53.176Z</OrderDate><OrderId>982e3c10-3840-4251-abdd-505cd8610788</OrderId><OrderType>port_out</OrderType><ErrorCode>200</ErrorCode><ErrorMessage>Port out successful.</ErrorMessage><FullNumber>9727717577</FullNumber><ProcessingStatus>COMPLETE</ProcessingStatus><RequestedFOCDate>2014-11-20T00:00:00.000Z</RequestedFOCDate><VendorId>512E</VendorId></lnpPortInfoForGivenStatus><Snip>   ---   </Snip><lnpPortInfoForGivenStatus><CountOfTNs>1</CountOfTNs><userId>Neustar</userId><lastModifiedDate>2015-03-30T14:01:59.049Z</lastModifiedDate><OrderDate>2015-03-24T17:47:17.605Z</OrderDate><OrderId>f8f02d0a-d1a4-42eb-8f45-ce8187cd73ff</OrderId><OrderType>port_out</OrderType><ErrorCode>200</ErrorCode><ErrorMessage>Port out successful.</ErrorMessage><FullNumber>2092660315</FullNumber><ProcessingStatus>COMPLETE</ProcessingStatus><RequestedFOCDate>2015-03-27T00:00:00.000Z</RequestedFOCDate><VendorId>512E</VendorId></lnpPortInfoForGivenStatus></LNPResponseWrapper>";

        public static string xmlNumberPortabilityResponseWithPortabilityErrros = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?><NumberPortabilityResponse><PortabilityErrors><Error><Code>7378</Code><Description>test description</Description><TelephoneNumbers><Tn>9199199999</Tn><Tn>9199199999</Tn></TelephoneNumbers></Error><Error><Code>7478</Code><Description>test description</Description><TelephoneNumbers><Tn>9199199999</Tn></TelephoneNumbers></Error></PortabilityErrors><PortableNumbers><Tn>9195551212</Tn><Tn>9195551213</Tn></PortableNumbers><SupportedRateCenters><RateCenterGroup><RateCenter>RALEIGH</RateCenter><City>RALEIGH</City><State>NC</State><LATA>426</LATA><Tiers><Tier>0</Tier></Tiers><TnList><Tn>9195551212</Tn><Tn>9195551213</Tn></TnList></RateCenterGroup></SupportedRateCenters><UnsupportedRateCenters/><SupportedLosingCarriers><LosingCarrierTnList><LosingCarrierSPID>6214</LosingCarrierSPID><LosingCarrierName>Cingular</LosingCarrierName><TnList><Tn>9195551212</Tn><Tn>9195551213</Tn></TnList></LosingCarrierTnList></SupportedLosingCarriers><UnsupportedLosingCarriers/></NumberPortabilityResponse>";
        public static string LocalAreaSearchResultXml = "<SearchResult><ResultCount>2</ResultCount><TelephoneNumberDetailList><TelephoneNumberDetail><City>JERSEY CITY</City><LATA>224</LATA><RateCenter>JERSEYCITY</RateCenter><State>NJ</State><TelephoneNumber>2012001555</TelephoneNumber></TelephoneNumberDetail><TelephoneNumberDetail><City>JERSEY CITY</City><LATA>224</LATA><RateCenter>JERSEYCITY</RateCenter><State>NJ</State><TelephoneNumber>123123123</TelephoneNumber></TelephoneNumberDetail></TelephoneNumberDetailList></SearchResult>";
        public static string xmlLnpOrderResponse = "<LnpOrderResponse><Errors><Code>0000</Code><Description>Generic Error</Description></Errors><Errors><Code>7205</Code><Description>Telephone number is already being processed on another order</Description></Errors><ProcessingStatus>CANCELLED</ProcessingStatus><RequestedFocDate>2014-08-04T13:37:08.676Z</RequestedFocDate><CustomerOrderId>SJM00002</CustomerOrderId><LoaAuthorizingPerson>The Authguy</LoaAuthorizingPerson><Subscriber><SubscriberType>BUSINESS</SubscriberType><FirstName>First</FirstName><LastName>Last</LastName><ServiceAddress><HouseNumber>11235</HouseNumber><StreetName>Back</StreetName><City>Denver</City><StateCode>CO</StateCode><Zip>27541</Zip><County>Canyon</County><Country>United States</Country><AddressType>Service</AddressType></ServiceAddress></Subscriber><WirelessInfo><AccountNumber>771297665AABC</AccountNumber><PinNumber>1234</PinNumber></WirelessInfo><TnAttributes><TnAttribute>Protected</TnAttribute></TnAttributes><BillingTelephoneNumber>9195551234</BillingTelephoneNumber><NewBillingTelephoneNumber>9175131245</NewBillingTelephoneNumber><ListOfPhoneNumbers><PhoneNumber>9194809871</PhoneNumber></ListOfPhoneNumbers><AlternateSpid>Foo</AlternateSpid><AccountId>20</AccountId><SiteId>2857</SiteId><PeerId>317771</PeerId><LosingCarrierName>Mock Carrier</LosingCarrierName><VendorName>Bandwidth CLEC</VendorName><OrderCreateDate>2014-08-04T13:37:06.323Z</OrderCreateDate><LastModifiedDate>2014-08-04T13:37:08.676Z</LastModifiedDate><userId>jbm</userId><LastModifiedBy>jbm</LastModifiedBy><PartialPort>false</PartialPort><Triggered>false</Triggered><PortType>AUTOMATED</PortType><!-- [ AUTOMATED | INTERNAL | MANUALOFFNET ] --></LnpOrderResponse>";

        public static string xmlLnpOrderResponseNoErrors = "<LnpOrderResponse><ProcessingStatus>CANCELLED</ProcessingStatus><RequestedFocDate>2014-08-04T13:37:08.676Z</RequestedFocDate><CustomerOrderId>SJM00002</CustomerOrderId><LoaAuthorizingPerson>The Authguy</LoaAuthorizingPerson><Subscriber><SubscriberType>BUSINESS</SubscriberType><FirstName>First</FirstName><LastName>Last</LastName><ServiceAddress><HouseNumber>11235</HouseNumber><StreetName>Back</StreetName><City>Denver</City><StateCode>CO</StateCode><Zip>27541</Zip><County>Canyon</County><Country>United States</Country><AddressType>Service</AddressType></ServiceAddress></Subscriber><WirelessInfo><AccountNumber>771297665AABC</AccountNumber><PinNumber>1234</PinNumber></WirelessInfo><TnAttributes><TnAttribute>Protected</TnAttribute></TnAttributes><BillingTelephoneNumber>9195551234</BillingTelephoneNumber><NewBillingTelephoneNumber>9175131245</NewBillingTelephoneNumber><ListOfPhoneNumbers><PhoneNumber>9194809871</PhoneNumber></ListOfPhoneNumbers><AlternateSpid>Foo</AlternateSpid><AccountId>20</AccountId><SiteId>2857</SiteId><PeerId>317771</PeerId><LosingCarrierName>Mock Carrier</LosingCarrierName><VendorName>Bandwidth CLEC</VendorName><OrderCreateDate>2014-08-04T13:37:06.323Z</OrderCreateDate><LastModifiedDate>2014-08-04T13:37:08.676Z</LastModifiedDate><userId>jbm</userId><LastModifiedBy>jbm</LastModifiedBy><PartialPort>false</PartialPort><Triggered>false</Triggered><PortType>AUTOMATED</PortType><!-- [ AUTOMATED | INTERNAL | MANUALOFFNET ] --></LnpOrderResponse>";

        /**
         * 
         * Sites Xmls
         */
        public static string ValidSitesResponseXml = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?><SitesResponse><Sites><Site><Id>1</Id><Name>Test Site</Name><Description>A site description</Description></Site></Sites></SitesResponse>";
        public static string ValidSiteResponseXml = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?><SiteResponse><Site><Id>1</Id><Name>Test Site</Name><Description>A Site Description</Description><Address><HouseNumber>900</HouseNumber><StreetName>Main Campus Drive</StreetName><City>Raleigh</City><StateCode>NC</StateCode><Zip>27615</Zip><Country>United States</Country><AddressType>Service</AddressType></Address></Site></SiteResponse>";

        /**
         * Sip Peer Xmls
         */
        public static string ValidSipPeersResponseXml = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?><TNSipPeersResponse><SipPeers><SipPeer xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xsi:type=\"SipPeer\"><PeerId>12345</PeerId><PeerName>SIP Peer 1</PeerName><Description>Sip Peer 1 description</Description><IsDefaultPeer>true</IsDefaultPeer><ShortMessagingProtocol>SIP</ShortMessagingProtocol><VoiceHosts><Host><HostName>70.62.112.156</HostName></Host></VoiceHosts><VoiceHostGroups/><SmsHosts><Host><HostName>70.62.112.156</HostName></Host></SmsHosts><TerminationHosts><TerminationHost><HostName>70.62.112.156</HostName><Port>5060</Port><CustomerTrafficAllowed>DOMESTIC</CustomerTrafficAllowed><DataAllowed>true</DataAllowed></TerminationHost></TerminationHosts><CallingName><Display>true</Display><Enforced>false</Enforced></CallingName></SipPeer></SipPeers></TNSipPeersResponse>";
        public static string ValidSipPeerResponseXml = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?><SipPeerResponse><SipPeer><PeerId>10</PeerId><PeerName>SIP Peer 1</PeerName><Description>Sip Peer 1 description</Description><IsDefaultPeer>true</IsDefaultPeer><ShortMessagingProtocol>SIP</ShortMessagingProtocol><VoiceHosts/><VoiceHostGroups/><SmsHosts/><TerminationHosts/><CallingName><Display>true</Display><Enforced>false</Enforced></CallingName></SipPeer></SipPeerResponse>";
        public static string ValidSipPeerTnResponseXml = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?><SipPeerTelephoneNumberResponse><SipPeerTelephoneNumber><FullNumber>9195551212</FullNumber></SipPeerTelephoneNumber></SipPeerTelephoneNumberResponse>";
        public static string ValidSipPeerTnsResponseXml = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?><SipPeerTelephoneNumbersResponse><SipPeerTelephoneNumbers><SipPeerTelephoneNumber><FullNumber>3034162216</FullNumber></SipPeerTelephoneNumber><SipPeerTelephoneNumber><FullNumber>3034162218</FullNumber></SipPeerTelephoneNumber><SipPeerTelephoneNumber><FullNumber>3034162227</FullNumber></SipPeerTelephoneNumber><SipPeerTelephoneNumber><FullNumber>7025097265</FullNumber></SipPeerTelephoneNumber><SipPeerTelephoneNumber><FullNumber>3034162212</FullNumber></SipPeerTelephoneNumber><SipPeerTelephoneNumber><FullNumber>7024759964</FullNumber></SipPeerTelephoneNumber><SipPeerTelephoneNumber><FullNumber>3034162226</FullNumber></SipPeerTelephoneNumber><SipPeerTelephoneNumber><FullNumber>3034162231</FullNumber></SipPeerTelephoneNumber><SipPeerTelephoneNumber><FullNumber>3034162223</FullNumber></SipPeerTelephoneNumber><SipPeerTelephoneNumber><FullNumber>2143770078</FullNumber></SipPeerTelephoneNumber><SipPeerTelephoneNumber><FullNumber>3302699968</FullNumber></SipPeerTelephoneNumber><SipPeerTelephoneNumber><FullNumber>8665711365</FullNumber></SipPeerTelephoneNumber><SipPeerTelephoneNumber><FullNumber>9284448929</FullNumber></SipPeerTelephoneNumber><SipPeerTelephoneNumber><FullNumber>3302710174</FullNumber></SipPeerTelephoneNumber><SipPeerTelephoneNumber><FullNumber>7024797571</FullNumber></SipPeerTelephoneNumber><SipPeerTelephoneNumber><FullNumber>7025091999</FullNumber></SipPeerTelephoneNumber><SipPeerTelephoneNumber><FullNumber>7022579119</FullNumber></SipPeerTelephoneNumber></SipPeerTelephoneNumbers></SipPeerTelephoneNumbersResponse>";
        /**
         * Order Xmls
         */
        public static string ValidOrderResponseXml = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?><OrderResponse><Order><Name>A New Order</Name><OrderCreateDate>2014-10-14T17:58:15.299Z</OrderCreateDate><BackOrderRequested>false</BackOrderRequested><id>1</id><ExistingTelephoneNumberOrderType><TelephoneNumberList><TelephoneNumber>2052865046</TelephoneNumber></TelephoneNumberList></ExistingTelephoneNumberOrderType><PartialAllowed>false</PartialAllowed><SiteId>2858</SiteId></Order></OrderResponse>";

        /**
         * Reservation Xmls
         */

        public static string ValidReservationResponseXml = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?><ReservationResponse><Reservation><ReservationId>1</ReservationId><AccountId>accountId</AccountId><ReservationExpires>30</ReservationExpires><ReservedTn>9195551212</ReservedTn></Reservation></ReservationResponse>";
        public static string InvalidReservationResponseXml = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?><ReservationResponse><ResponseStatus><ErrorCode>5041</ErrorCode><Description>Reservation failed: telephone number 9195551212 is not available.</Description></ResponseStatus></ReservationResponse>";
        public static string ValidCreatePostInResponse = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?><LnpOrderResponse><OrderId>d28b36f7-fa96-49eb-9556-a40fca49f7c6</OrderId><Status><Code>201</Code><Description>Order request received. Please use the order id to check the status of your order later.</Description></Status><ProcessingStatus>PENDING_DOCUMENTS</ProcessingStatus><LoaAuthorizingPerson>John Doe</LoaAuthorizingPerson><Subscriber><SubscriberType>BUSINESS</SubscriberType><BusinessName>Acme Corporation</BusinessName><ServiceAddress><HouseNumber>1623</HouseNumber><StreetName>Brockton Ave #1</StreetName><City>Los Angeles</City><StateCode>CA</StateCode><Zip>90025</Zip><Country>USA</Country></ServiceAddress></Subscriber><BillingTelephoneNumber>6882015002</BillingTelephoneNumber><ListOfPhoneNumbers><PhoneNumber>6882015025</PhoneNumber><PhoneNumber>6882015026</PhoneNumber></ListOfPhoneNumbers><Triggered>false</Triggered><BillingType>PORTIN</BillingType></LnpOrderResponse>";


        public static string FileListResponse = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?><fileListResponse><fileCount>6</fileCount><fileData><FileName>d28b36f7-fa96-49eb-9556-a40fca49f7c6-1416231534986.txt</FileName><FileMetaData><DocumentType>LOA</DocumentType></FileMetaData></fileData><fileData><FileName>d28b36f7-fa96-49eb-9556-a40fca49f7c6-1416231558768.txt</FileName><FileMetaData><DocumentType>LOA</DocumentType></FileMetaData></fileData><fileData><FileName>d28b36f7-fa96-49eb-9556-a40fca49f7c6-1416231581134.txt</FileName><FileMetaData><DocumentType>LOA</DocumentType></FileMetaData></fileData><fileData><FileName>d28b36f7-fa96-49eb-9556-a40fca49f7c6-1416231629005.txt</FileName><FileMetaData><DocumentType>LOA</DocumentType></FileMetaData></fileData><fileData><FileName>d28b36f7-fa96-49eb-9556-a40fca49f7c6-1416231699462.txt</FileName><FileMetaData><DocumentType>LOA</DocumentType></FileMetaData></fileData><fileData><FileName>d28b36f7-fa96-49eb-9556-a40fca49f7c6-1416232756923.txt</FileName><FileMetaData><DocumentType>LOA</DocumentType></FileMetaData></fileData><resultCode>0</resultCode><resultMessage>LOA file list successfully returned</resultMessage></fileListResponse>";
        public static string FileMetadataResponse = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?><FileMetaData><DocumentType>LOA</DocumentType></FileMetaData>";
        public static string FileMetadataPut = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n<FileMetaData xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">\r\n  <DocumentType>INVOICE</DocumentType>\r\n  <DocumentName>docName</DocumentName>\r\n</FileMetaData>";

        public static string LnpCheckResponse = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?><NumberPortabilityResponse><PortableNumbers><Tn>9195551212</Tn><Tn>9195551213</Tn></PortableNumbers><SupportedRateCenters><RateCenterGroup><RateCenter>RALEIGH</RateCenter><City>RALEIGH</City><State>NC</State><LATA>426</LATA><Tiers><Tier>0</Tier></Tiers><TnList><Tn>9195551212</Tn><Tn>9195551213</Tn></TnList></RateCenterGroup></SupportedRateCenters><UnsupportedRateCenters/><SupportedLosingCarriers><LosingCarrierTnList><LosingCarrierSPID>6214</LosingCarrierSPID><LosingCarrierName>Cingular</LosingCarrierName><TnList><Tn>9195551212</Tn><Tn>9195551213</Tn></TnList></LosingCarrierTnList></SupportedLosingCarriers><UnsupportedLosingCarriers/></NumberPortabilityResponse>";
        public static string NotesResponse = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?><Notes><Note><Id>11299</Id><UserId>customer</UserId><Description>Test</Description><LastDateModifier>2014-11-20T07:08:47.000Z</LastDateModifier></Note><Note><Id>11301</Id><UserId>customer</UserId><Description>Test1</Description><LastDateModifier>2014-11-20T07:11:36.000Z</LastDateModifier></Note></Notes>";
        public static string SubscriptionResponse = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?><SubscriptionsResponse><Subscriptions><Subscription><SubscriptionId>1</SubscriptionId><OrderType>orders</OrderType><OrderId>8684b1c8-7d41-4877-bfc2-6bd8ea4dc89f</OrderId><EmailSubscription><Email>test@test</Email><DigestRequested>NONE</DigestRequested></EmailSubscription></Subscription></Subscriptions></SubscriptionsResponse>";
        public static string RateCentersResponse = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?><RateCenterResponse><ResultCount>2</ResultCount><RateCenters><RateCenter><Abbreviation>ACME</Abbreviation><Name>ACME</Name></RateCenter><RateCenter><Abbreviation>AHOSKIE</Abbreviation><Name>AHOSKIE</Name></RateCenter><RateCenter><Abbreviation>ALBEMARLE</Abbreviation><Name>ALBEMARLE</Name></RateCenter></RateCenters></RateCenterResponse>";
        public static string CoveredRateCentersResponse = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?><CoveredRateCenters><CoveredRateCenter><Abbreviation>ACME</Abbreviation><Name>ACME</Name></CoveredRateCenter></CoveredRateCenters>";
        public static string CitiesResponse = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?><CityResponse><ResultCount>2</ResultCount><Cities><City><RcAbbreviation>SOUTHEPINS</RcAbbreviation><Name>ABERDEEN</Name></City><City><RcAbbreviation>JULIAN</RcAbbreviation><Name>ADVANCE</Name></City></Cities></CityResponse>";
        public static string TnResponse = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?><TelephoneNumberResponse><TelephoneNumber>1234</TelephoneNumber><Status>Inservice</Status><LastModifiedDate>2014-05-09T21:12:03.000Z</LastModifiedDate><OrderCreateDate>2014-05-09T21:12:03.835Z</OrderCreateDate><OrderId>5f3a4dab-aac7-4b0a-8ee4-1b6a67ae04be</OrderId><OrderType>NEW_NUMBER_ORDER</OrderType><SiteId>1091</SiteId><AccountId>9500149</AccountId></TelephoneNumberResponse>";
        public static string TnsListResponse = "<?xml version=\"1.0\"?><TelephoneNumbersResponse><TelephoneNumberCount>5</TelephoneNumberCount><Links><first></first><next></next></Links><TelephoneNumbers><TelephoneNumber><City>CARY</City><Lata>426</Lata><State>NC</State><FullNumber>9192381138</FullNumber><Tier>0</Tier><VendorId>49</VendorId><VendorName>Bandwidth CLEC</VendorName><RateCenter>CARY</RateCenter><Status>Inservice</Status><AccountId>9900008</AccountId><LastModified>2013-12-05T05:58:27.000Z</LastModified></TelephoneNumber><TelephoneNumber><City>CARY</City><Lata>426</Lata><FullNumber>9192381139</FullNumber><Tier>0</Tier><VendorId>49</VendorId><VendorName>Bandwidth CLEC</VendorName><RateCenter>CARY</RateCenter><Status>Inservice</Status><AccountId>9900000</AccountId><LastModified>2013-12-05T05:58:27.000Z</LastModified></TelephoneNumber></TelephoneNumbers></TelephoneNumbersResponse>";
        public static string TnSitesResponse = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?><Site><Id>1435</Id><Name>Sales Training</Name></Site>";
        public static string TnSipPeersResponse = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?><SipPeer><Id>4064</Id><Name>Sales</Name></SipPeer>";
        public static string TnRateCenterResponse = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?><TelephoneNumberResponse><TelephoneNumberDetails><State>CO</State><RateCenter>DENVER</RateCenter></TelephoneNumberDetails></TelephoneNumberResponse>";
        public static string TnLataResponse = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?><TelephoneNumberResponse><TelephoneNumberDetails><Lata>656</Lata></TelephoneNumberDetails></TelephoneNumberResponse>";
        public static string TnDetailsResponse = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?><TelephoneNumberResponse><TelephoneNumberDetails><City>DENVER</City><Lata>656</Lata><State>CO</State><FullNumber>1234</FullNumber><Tier>0</Tier><VendorId>49</VendorId><VendorName>Bandwidth CLEC</VendorName><RateCenter>DENVER    </RateCenter><Status>Inservice</Status><AccountId>9500149</AccountId><LastModified>2014-11-14T20:35:32.000Z</LastModified></TelephoneNumberDetails></TelephoneNumberResponse>";
        public static string AccountResponse = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?><AccountResponse><Account><AccountId>14</AccountId><CompanyName>CWI Hosting</CompanyName><AccountType>Business</AccountType><NenaId></NenaId><Tiers><Tier>0</Tier></Tiers><Address><HouseNumber>60</HouseNumber><HouseSuffix></HouseSuffix><PreDirectional></PreDirectional><StreetName>Pine</StreetName><StreetSuffix>St</StreetSuffix><PostDirectional></PostDirectional><AddressLine2></AddressLine2><City>Denver</City><StateCode>CO</StateCode><Zip>80016</Zip><PlusFour></PlusFour><Country>United States</Country><AddressType>Service</AddressType></Address><Contact><FirstName>Sanjay</FirstName><LastName>Rao</LastName><Phone>9195441234</Phone><Email>srao@bandwidth.com</Email></Contact><ReservationAllowed>true</ReservationAllowed><LnpEnabled>true</LnpEnabled><AltSpid>X455</AltSpid><SPID>9999</SPID><PortCarrierType>WIRELINE</PortCarrierType></Account></AccountResponse>";
        public static string DiscNumberResponse = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?><TNs><TotalCount>4</TotalCount><Links><first></first></Links><TelephoneNumbers><Count>2</Count><TelephoneNumber>4158714245</TelephoneNumber><TelephoneNumber>4352154439</TelephoneNumber></TelephoneNumbers></TNs>";
        public static string Dldas = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\" ?><ResponseSelectWrapper><ListOrderIdUserIdDate><TotalCount>3</TotalCount><OrderIdUserIdDate><accountId>14</accountId><CountOfTNs>2</CountOfTNs><userId>team_ua</userId><lastModifiedDate>2014-07-07T10:06:43.427Z</lastModifiedDate><OrderType>dlda</OrderType><OrderDate>2014-07-07T10:06:43.427Z</OrderDate><orderId>37a6447c-1a0b-4be9-ba89-3f5cb0aea142</orderId><OrderStatus>FAILED</OrderStatus></OrderIdUserIdDate><OrderIdUserIdDate><accountId>14</accountId><CountOfTNs>2</CountOfTNs><userId>team_ua</userId><lastModifiedDate>2014-07-07T10:05:56.595Z</lastModifiedDate><OrderType>dlda</OrderType><OrderDate>2014-07-07T10:05:56.595Z</OrderDate><orderId>743b0e64-3350-42e4-baa6-406dac7f4a85</orderId><OrderStatus>RECEIVED</OrderStatus></OrderIdUserIdDate><OrderIdUserIdDate><accountId>14</accountId><CountOfTNs>2</CountOfTNs><userId>team_ua</userId><lastModifiedDate>2014-07-07T09:32:17.234Z</lastModifiedDate><OrderType>dlda</OrderType><OrderDate>2014-07-07T09:32:17.234Z</OrderDate><orderId>f71eb4d2-bfef-4384-957f-45cd6321185e</orderId><OrderStatus>RECEIVED</OrderStatus></OrderIdUserIdDate></ListOrderIdUserIdDate></ResponseSelectWrapper>";
        public static string Dlda = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\" ?><DldaOrderResponse><DldaOrder><CustomerOrderId>5a88d16d-f8a9-45c5-a5db-137d700c6a22</CustomerOrderId><OrderCreateDate>2014-07-10T12:38:11.833Z</OrderCreateDate><AccountId>14</AccountId><CreatedByUser>jbm</CreatedByUser><OrderId>ea9e90c2-77a4-4f82-ac47-e1c5bb1311f4</OrderId><LastModifiedDate>2014-07-10T12:38:11.833Z</LastModifiedDate><ProcessingStatus>RECEIVED</ProcessingStatus><DldaTnGroups><DldaTnGroup><TelephoneNumbers><TelephoneNumber>2053778335</TelephoneNumber><TelephoneNumber>2053865784</TelephoneNumber></TelephoneNumbers><AccountType>BUSINESS</AccountType><ListingType>LISTED</ListingType><ListingName><FirstName>Joe</FirstName><LastName>Smith</LastName></ListingName><ListAddress>true</ListAddress><Address><HouseNumber>12</HouseNumber><StreetName>ELM</StreetName><City>New York</City><StateCode>NY</StateCode><Zip>10007</Zip><Country>United States</Country><AddressType>Dlda</AddressType></Address></DldaTnGroup></DldaTnGroups></DldaOrder></DldaOrderResponse>";
        public static string OrderAreaCodes = "<TelephoneDetailsReports><TelephoneDetailsReport><AreaCode>888</AreaCode><Count>1</Count></TelephoneDetailsReport></TelephoneDetailsReports>";
        public static string OrderNpaNxx = "<?xml version=\"1.0\" encoding=\"utf-8\"?><TelephoneDetailsReports><TelephoneDetailsReport><NPA-NXX>888424</NPA-NXX><Count>1</Count></TelephoneDetailsReport></TelephoneDetailsReports>";
        public static string OrderTotals = "<?xml version=\"1.0\" encoding=\"utf-8\"?><TelephoneDetailsReports><TelephoneDetailsReport><NPA-NXX>888424</NPA-NXX><Count>1</Count></TelephoneDetailsReport></TelephoneDetailsReports>";
        public static string OrderTns = "<?xml version=\"1.0\" encoding=\"utf-8\"?><TelephoneNumbers><Count>2</Count><TelephoneNumber>8042105666</TelephoneNumber><TelephoneNumber>8042105667</TelephoneNumber></TelephoneNumbers>";
        public static string OrderHistory = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?><OrderHistoryWrapper><OrderHistory><OrderDate>2014-05-20T14:21:43.937Z</OrderDate><Note>Order backordered - awaiting additional numbers</Note><Status>BACKORDERED</Status></OrderHistory><OrderHistory><OrderDate>2014-05-20T14:24:43.428Z</OrderDate><Note>Order backordered - awaiting additional numbers</Note><Author>System</Author><Status>BACKORDERED</Status><Difference></Difference></OrderHistory></OrderHistoryWrapper>";
        public static string InServiceNumbers = "<?xml version=\"1.0\"?><TNs><TotalCount>59</TotalCount><Links><first> ( a link goes here ) </first></Links><TelephoneNumbers><Count>59</Count><TelephoneNumber>8043024183</TelephoneNumber><TelephoneNumber>8042121778</TelephoneNumber><TelephoneNumber>8042146066</TelephoneNumber><TelephoneNumber>8043814903</TelephoneNumber><TelephoneNumber>8043814905</TelephoneNumber><TelephoneNumber>8043814864</TelephoneNumber><TelephoneNumber>8043326094</TelephoneNumber><TelephoneNumber>8042121771</TelephoneNumber><TelephoneNumber>8043024182</TelephoneNumber><!-- SNIP --><TelephoneNumber>8043814900</TelephoneNumber><TelephoneNumber>8047672642</TelephoneNumber><TelephoneNumber>8043024368</TelephoneNumber><TelephoneNumber>8042147950</TelephoneNumber><TelephoneNumber>8043169931</TelephoneNumber><TelephoneNumber>8043325302</TelephoneNumber></TelephoneNumbers></TNs>";
        public static string InServiceNumbersTotals = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?><Quantity><Count>3</Count></Quantity>";
        public static string Lidbs = "<?xml version=\"1.0\"?><ResponseSelectWrapper><ListOrderIdUserIdDate><TotalCount>2122</TotalCount><OrderIdUserIdDate><accountId>9999999</accountId><CountOfTNs>0</CountOfTNs><lastModifiedDate>2014-02-25T16:02:43.195Z</lastModifiedDate><OrderType>lidb</OrderType><OrderDate>2014-02-25T16:02:43.195Z</OrderDate><orderId>abe36738-6929-4c6f-926c-88e534e2d46f</orderId><OrderStatus>FAILED</OrderStatus><TelephoneNumberDetails/><userId>team_ua</userId></OrderIdUserIdDate><!-- ...SNIP... --><OrderIdUserIdDate><accountId>9999999</accountId><CountOfTNs>0</CountOfTNs><lastModifiedDate>2014-02-25T16:02:39.021Z</lastModifiedDate><OrderType>lidb</OrderType><OrderDate>2014-02-25T16:02:39.021Z</OrderDate><orderId>ba5b6297-139b-4430-aab0-9ff02c4362f4</orderId><OrderStatus>FAILED</OrderStatus><userId>team_ua</userId></OrderIdUserIdDate></ListOrderIdUserIdDate></ResponseSelectWrapper>";
        public static string Lidb = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?><LidbOrder><CustomerOrderId>testCustomerOrderId</CustomerOrderId><orderId>255bda29-fc57-44e8-a6c2-59b45388c6d0</orderId>    <OrderCreateDate>2014-05-28T14:46:21.724Z</OrderCreateDate><ProcessingStatus>RECEIVED</ProcessingStatus><CreatedByUser>jbm</CreatedByUser><LastModifiedDate>2014-02-20T19:33:17.600Z</LastModifiedDate><OrderCompleteDate>2014-02-20T19:33:17.600Z</OrderCompleteDate><ErrorList/><LidbTnGroups><LidbTnGroup><TelephoneNumbers><TelephoneNumber>4082213311</TelephoneNumber></TelephoneNumbers><FullNumber>8042105618</FullNumber><SubscriberInformation>Fred</SubscriberInformation><UseType>BUSINESS</UseType><Visibility>PRIVATE</Visibility></LidbTnGroup><LidbTnGroup><TelephoneNumbers><TelephoneNumber>4082212850</TelephoneNumber><TelephoneNumber>4082213310</TelephoneNumber></TelephoneNumbers><FullNumber>8042105760</FullNumber><SubscriberInformation>Fred</SubscriberInformation><UseType>RESIDENTIAL</UseType><Visibility>PUBLIC</Visibility></LidbTnGroup></LidbTnGroups></LidbOrder>";
        public static string LsrOrder = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?><LsrOrder><CustomerOrderId>MyId5</CustomerOrderId><LastModifiedBy>System</LastModifiedBy><OrderCreateDate>2015-03-03T13:54:00.450Z</OrderCreateDate><AccountId>9999999</AccountId><OrderId>00cf7e08-cab0-4515-9a77-2d0a7da09415</OrderId><LastModifiedDate>2015-03-03T14:07:19.926Z</LastModifiedDate><OrderStatus>FAILED</OrderStatus><SPID>123C</SPID><BillingTelephoneNumber>9192381468</BillingTelephoneNumber><Pon>testpon1002</Pon><PonVersion>0</PonVersion><RequestedFocDate>2015-11-15</RequestedFocDate><AuthorizingPerson>Jim Hopkins</AuthorizingPerson><Subscriber><SubscriberType>BUSINESS</SubscriberType><BusinessName>BusinessName</BusinessName><AccountNumber>123463</AccountNumber><PinNumber>1231</PinNumber><ServiceAddress><HouseNumber>11</HouseNumber><StreetName>Park</StreetName><StreetSuffix>Ave</StreetSuffix><City>New York</City><StateCode>NY</StateCode><Zip>90025</Zip></ServiceAddress></Subscriber><ListOfTelephoneNumbers><TelephoneNumber>9192381467</TelephoneNumber><TelephoneNumber>9192381468</TelephoneNumber></ListOfTelephoneNumbers><CountOfTNs>2</CountOfTNs></LsrOrder>";
        public static string LsrOrders = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?><LsrOrders><TotalCount>5</TotalCount><LsrOrderSummary><accountId>9999999</accountId><CountOfTNs>2</CountOfTNs><CustomerOrderId>FineCustomerid</CustomerOrderId><userId>team_ua</userId><lastModifiedDate>2015-03-02T09:10:16.193Z</lastModifiedDate><OrderType>lsr</OrderType><OrderDate>2015-03-25T11:44:42.941Z</OrderDate><OrderStatus>PENDING</OrderStatus><ActualFocDate>2015-03-25</ActualFocDate><BillingTelephoneNumber>2526795000</BillingTelephoneNumber><CreatedByUser>lsrOnlyUser</CreatedByUser><OrderId>7d644c88-ef23-4307-96ab-20253666d0c7</OrderId><Pon>ATT-011515-324234</Pon><PonVersion>0</PonVersion><RequestedFocDate>2015-11-15</RequestedFocDate></LsrOrderSummary><!-- SNIP --><LsrOrderSummary><accountId>9999999</accountId><CountOfTNs>2</CountOfTNs><CustomerOrderId>MyId5</CustomerOrderId><lastModifiedDate>2015-03-03T14:07:19.926Z</lastModifiedDate><OrderType>lsr</OrderType><OrderDate>2015-03-25T11:44:42.941Z</OrderDate><OrderStatus>NEW</OrderStatus><ActualFocDate>2015-03-25</ActualFocDate><BillingTelephoneNumber>2526795000</BillingTelephoneNumber><CreatedByUser>lsrOnlyUser</CreatedByUser><OrderId>00cf7e08-cab0-4515-9a77-2d0a7da09415</OrderId><Pon>testpon1002</Pon><PonVersion>0</PonVersion><RequestedFocDate>2015-11-15</RequestedFocDate></LsrOrderSummary></LsrOrders>";
        public static string LineOption = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?><LineOptionOrderResponse> <LineOptions> <CompletedNumbers> <TelephoneNumber>2013223685</TelephoneNumber> </CompletedNumbers> </LineOptions> </LineOptionOrderResponse>";
        public static string Hosts = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?><SiteHostsResponse><SiteHosts> <SiteHost> <SiteId>{id}</SiteId> <SipPeerHosts> <SipPeerHost> <SipPeerId>{id}</SipPeerId> <SmsHosts> <Host> <HostName>{IP | host name}</HostName> <Port>8888</Port> </Host></SmsHosts> <VoiceHosts></VoiceHosts> <TerminationHosts></TerminationHosts> </SipPeerHost></SipPeerHosts></SiteHost></SiteHosts></SiteHostsResponse>";
    }
}
