using ApiBank.Connector.BankExceptionsModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiBank.Connector.Response {
    public class Person : CustomStatus {
        public Boolean has_any_account { get; set; }
        public List<Banks> accounts { get; set; }
        
    }
}
