using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiBank.Connector.Request {
    public class ConfigRequest {

        public String Url { get; }
        public String User { get; }
        public String Password { get; }
        public BanksEnums Bank { get; }
        public String Terminal { get; }
        public String Channel { get; }
        public String Identification { get; }

        public ConfigRequest(String Url, String User, String Password, BanksEnums Bank)
        {
            this.Url = Url;
            this.User = User;
            this.Password = Password;
            this.Bank = Bank;
        }

        public ConfigRequest(String Url, String User, String Password, BanksEnums Bank, String Terminal, String Channel, String Identification) {
            this.Url = Url;
            this.User = User;
            this.Password = Password;
            this.Bank = Bank;
            this.Terminal = Terminal;
            this.Channel = Channel;
            this.Identification = Identification;
        }

    }
}
