using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiBank.Connector.Response {
    public class AccountAlias {
        public List<Owners> owners { get; set; }
        public String Type { get; set; }
        public Boolean is_active { get; set; }
        public String currency { get; set; }
        public String label { get; set; }
        public AccountRouting account_routing { get; set; }
        public BankRouting bank_routing { get; set; }
    }
}
