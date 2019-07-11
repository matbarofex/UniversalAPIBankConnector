using ApiBank.Connector.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ApiBank.Connector.BankExceptionsModels {

    [Serializable]
    public class CustomException: Exception {
        public string code { get; set; }
        public string message { get; set; }
        public string more_info { get; set; }
        public string process { get; set; }
        public CustomException(string code, string message = "", string more_info= "", string process="") {
            this.code = code;
            this.message = message;
            this.more_info = more_info;
            this.process = process;
        }

        public CustomException() { }

        public CustomStatus convertToCustomStatus() {
            CustomStatus customStatus = new CustomStatus();
            customStatus.code = this.code;
            customStatus.message = this.message;
            customStatus.more_info = this.more_info;
            customStatus.process = this.process;
            return customStatus;
        }


    }

    public class ExceptionsCodes{
        public const string ProcessOk = "OK"; //OK
        public const string loginError = "LG0001"; //Login error in Api
        public const string loginDataError = "LG0002"; //Login data incomplete
        public const string UnexpectedException = "LG0000"; //UnexpectedException
        public const string ApiException = "API0001"; //Api Exception 
        public const string NotImplemented = "DDL0001"; //Dll NotImplemented
    }


    public class LoginErrorException : CustomException {
        public LoginErrorException() {
            this.code = ExceptionsCodes.loginDataError;
            this.message = "Login error.";
        }

        public LoginErrorException(string moreInfo) {
            this.code = ExceptionsCodes.loginDataError;
            this.message = "Login error.";
            this.more_info = moreInfo;
        }
    }

    public class LoginDataErrorException : CustomException {
        public LoginDataErrorException() {
            this.code = ExceptionsCodes.loginDataError;
            this.message = "Login data incomplete.";
        }

        public LoginDataErrorException(string moreInfo) {
            this.code = ExceptionsCodes.loginDataError;
            this.message = "Login data incomplete.";
            this.more_info = moreInfo;
        }
    }

}
