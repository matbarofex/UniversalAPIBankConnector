using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiBank.Connector.Response
{

    public class MovementsResponse : CustomStatus
    {
        public List<Transactions> transactions { get; set; }
    }

    public class Transactions : CustomStatus
    {
        public String id { get; set; }
        public CunterParty counterparty { get; set; }
        public Details details { get; set; }
        public ThisAccount this_account { get; set; }

    }

    public class ThisAccount{
        public String id { get; set; }
        public String kind { get; set; }
        public BankRouting bank_routing { get; set; }
        public AccountRouting account_routing { get; set; }
    }

    public class Details
    {
        public String type { get; set; }
        public String description { get; set; }
        public String posted { get; set; }
        public String completed { get; set; }
        public Value value { get; set; }
        public String motive { get; set; }
        public String reference_number { get; set; }
        public NewBalance new_balance { get; set; }
    }

    public class NewBalance
    {
        public String currency { get; set; }
        public decimal amount { get; set; }
    }

    public class Value {
        public String currency { get; set; }
        public decimal amount { get; set; }
    }

    public class CunterParty {
        public String id { get; set; }
        public String name { get; set; }
        public String id_type { get; set; }
        public BankRouting bank_routing { get; set; }
        public AccountRouting account_routing { get; set; }
    }

}
