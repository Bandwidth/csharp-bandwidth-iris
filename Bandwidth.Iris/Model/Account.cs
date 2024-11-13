using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Bandwidth.Iris.Model
{
    public class Account
    {
        public async static Task<Account> Get(Client client)
        {
            return (await client.MakeGetRequest<AccountResponse>(client.ConcatAccountPath(""))).Account;
        }
#if !PCL
        public static Task<Account> Get()
        {
            return Get(Client.GetInstance());
        }
#endif
        public string Id
        {
            get { return AccountId; }
            set { AccountId = value; }
        }

        public string AccountId { get; set; }
        public string CompanyName { get; set; }
        public string AccountType { get; set; }
        public string NenaId { get; set; }

        [XmlArrayItem("Tier")]
        public int[] Tiers { get; set; }

        public bool ReservationAllowed { get; set; }
        public bool LnpEnabled { get; set; }
        public string AltSpid { get; set; }

        [XmlElement("SPID")]
        public string Spid { get; set; }
        public string PortCarrierType { get; set; }

        public Contact Contact { get; set; }
        public Address Address { get; set; }
    }

    public class AccountResponse
    {
        public Account Account { get; set; }
    }

    public class Contact
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
