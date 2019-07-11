using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiBank.Connector.BankExceptionsModels {
    internal class BindException {
        public string code { get; set; }
        public string message { get; set; }
        public string more_info { get; set; }
        public string process { get; set; }
    }
}
