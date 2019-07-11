using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiBank.Connector.Banks.ValoresResponse {
    internal class ValoresLoginResponse {
        public bool success { get; set; }
        public object message { get; set; }
        public string sessionId { get; set; }
        public string entityId { get; set; }
        public string customerId { get; set; }
        public string culture { get; set; }
        public string profile { get; set; }
        public object login { get; set; }
        public UserEntityInformationResponse userInformation { get; set; }
    }

    internal class UserEntityInformationResponse {
        public bool success { get; set; }
        public object message { get; set; }
        public string entityId { get; set; }
        public object customerId { get; set; }
        public object hasEntity { get; set; }
        public object hasLogin { get; set; }
        public string completeName { get; set; }
        public string entityType { get; set; }
        public string identityCard { get; set; }
        public string office { get; set; }
        public DateTime regDate { get; set; }
        public string official { get; set; }
        public string firstSurName { get; set; }
        public string secondSurName { get; set; }
        public object birthDate { get; set; }
        public object firstName { get; set; }
        public DateTime modDate { get; set; }
    }
}
