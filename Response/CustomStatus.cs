using ApiBank.Connector.BankExceptionsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiBank.Connector.Response {
    public class CustomStatus {

        public string code { get; set; }
        public string message { get; set; }
        public string more_info { get; set; }
        public string process { get; set; }
        public CustomStatus(string code, string message = "", string more_info = "", string process = "") {
            this.code = code;
            this.message = message;
            this.more_info = more_info;
            this.process = process;
        }

        public CustomStatus() { }

        private CustomStatus _status;
        public CustomStatus executeStatus
        {
            get
            {
                return _status;
            }
            set
            {
                if (value.code == null)
                {
                    _status = new CustomStatus();
                    _status.code = "OK";
                }
                else
                {
                    _status = value;
                }

            }
        }

    }
}
