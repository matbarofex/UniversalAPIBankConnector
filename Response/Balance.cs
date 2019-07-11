using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiBank.Connector.Response {
    public class Balance
    {
        public String currency { get; set; }
        public Decimal amount { get; set; }
    }
}
