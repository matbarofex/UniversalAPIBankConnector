using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiBank.Connector.Response {
    public class Banks {
        public String Type { get; set; }
        public Boolean is_active { get; set; }
        public String currency { get; set; }
        public BankRouting bank_routing { get; set; }
    }
}
