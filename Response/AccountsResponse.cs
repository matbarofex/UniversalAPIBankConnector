using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiBank.Connector.Response {

    public class AccountsResponse : CustomStatus
    {
        public List<Account> accounts { get; set; }
    }

    public class Account : CustomStatus
    {
        public String id { get; set; }
        public String label { get; set; }
        public String number { get; set; }
        public String Type { get; set; }
        public String status { get; set; }
        public List<Owners> owners { get; set; }
        public Balance balance { get; set; }
        public String bank_id { get; set; }        
        public AccountRouting account_routing { get; set; }
    }
}
