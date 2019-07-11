using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiBank.Connector.Response {
    public class BankRouting {
        public String scheme { get; set; }
        public String address { get; set; }
        public String code { get; set; }
    }
}
