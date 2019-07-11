using ApiBank.Connector.BankExceptionsModels;
using ApiBank.Connector.Banks.ValoresRequest;
using ApiBank.Connector.Banks.ValoresResponse;
using ApiBank.Connector.Encriptation;
using ApiBank.Connector.Request;
using ApiBank.Connector.Response;
using Newtonsoft.Json;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;

namespace ApiBank.Connector.Banks {
    internal class ValoresConnector : BankConnector {

        public ValoresConnector(ConfigRequest request) {
            this.config = request;
        }

        public override Person GetPersonByCUIT(string cuit)
        {
            Person result = new Person();
            result.executeStatus= new CustomStatus(ExceptionsCodes.NotImplemented, "NotImplemented");
            return result;

        }

        public override ViewsResponse GetViewsByBankId(string bank_id)
        {
            ViewsResponse result = new ViewsResponse();
            result.executeStatus = new CustomStatus(ExceptionsCodes.NotImplemented, "NotImplemented");
            return result;
        }

        CookieContainer cookieContainer;

        public ValoresLoginResponse doLogin() {
            try {

                string publicKey = "<RSAKeyValue><Modulus>teI1ma/XrHTfJ2xXwG0JIBsCr3rO4HXISVywBBAp91iE3R3PJlXh4D+JuRD1h2E874m7XZdwIjBjLlSjf3pNrvuaqP63w6b6xZwGMMHrzRjKm/sJBiuqtFyMYpt2TEIIDnHntNpO7GCstagQMd9w71C9T59hjB2QMY28a00RWA4Vnf/3luhEOpO7Sy7wVZ9gxkrnnramWY0J5hFd1N7hdUx5mmtFAsbsI2+97jnk5Wk6tGzEmYhR06UM7+C5QnGlyk3ICaUJcSPsbZq7NTQEWyQMWOaOgIrQCQUCLo1OuwqLblmgoXBINWp/+rvM80ytHYWn8h0i/KMk5B6K+Elsbw==</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";

                LoginValoresRequest login = new LoginValoresRequest();
                login.login = config.User;
                login.password = RSAEncript.EncryptText(config.Password, 1024, publicKey); 

                login.terminal = config.Terminal;
                login.channel = config.Channel;
                login.identification = config.Identification;

                if (config.Terminal == "" || config.Channel == "" || config.Identification == "") {
                    throw new CustomException(ExceptionsCodes.loginDataError, "Login Data Error", "Complete User, Password, termina, channel or identification", null);
                }

                var request = (HttpWebRequest)WebRequest.Create(config.Url + "security/login");
                request.ContentType = "application/json";
                request.Method = "POST";

                if (cookieContainer != null)
                    request.CookieContainer = cookieContainer;
                else
                    request.CookieContainer = new CookieContainer();

                using (var streamWriter = new StreamWriter(request.GetRequestStream())) {
                    string json = JsonConvert.SerializeObject(login);
                    streamWriter.Write(json);
                }

                var httpResponse = (HttpWebResponse)request.GetResponse();

                ValoresLoginResponse response = new ValoresLoginResponse();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream())) {
                    var result = streamReader.ReadToEnd();
                    response = JsonConvert.DeserializeObject<ValoresLoginResponse>(result);
                }

                if (response.sessionId == null) {
                    throw new CustomException(ExceptionsCodes.loginError, "Unauthorized Error", "", "security/login");
                }

                cookieContainer = request.CookieContainer;

                request = (HttpWebRequest)WebRequest.Create(config.Url + "enrollment/getUserEntityInformation");
                request.ContentType = "application/json";
                request.Method = "POST";

                if (cookieContainer != null)
                    request.CookieContainer = cookieContainer;
                else
                    request.CookieContainer = new CookieContainer();

                using (var streamWriter = new StreamWriter(request.GetRequestStream())) {
                    streamWriter.Write("{}");
                }

                httpResponse = (HttpWebResponse)request.GetResponse();

                UserEntityInformationResponse responseData;

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream())) {
                    var result = streamReader.ReadToEnd();
                    responseData = JsonConvert.DeserializeObject<UserEntityInformationResponse>(result);
                }

                response.userInformation = responseData;

                return response;

            } catch (CustomException exC) {
                throw exC;
            } catch (Exception ex) {
                throw new CustomException(ExceptionsCodes.UnexpectedException, "Unexpected Error", ex.Message, "security/login");
            }
        }

        public override AccountsResponse GetAccounts(string bank_id = "", string view_id = "")
        {
            AccountsResponse result = new AccountsResponse();
            try {

                ValoresLoginResponse loginData = doLogin();

                string urlData = "accounts/getOwnAccounts";

                var request = (HttpWebRequest)WebRequest.Create(config.Url + urlData);
                request.ContentType = "application/json";
                request.Method = "POST";

                if (cookieContainer != null)
                    request.CookieContainer = cookieContainer;
                else
                    request.CookieContainer = new CookieContainer();

                using (var streamWriter = new StreamWriter(request.GetRequestStream())) {
                    streamWriter.Write("{}");
                }

                var httpResponse = (HttpWebResponse)request.GetResponse();

                ValoresAccountsResponse response = new ValoresAccountsResponse();

                string JsonResult = "";
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream())) {
                    JsonResult = streamReader.ReadToEnd();
                    response = JsonConvert.DeserializeObject<ValoresAccountsResponse>(JsonResult);
                }

                if (response.message != null) {
                    result.executeStatus = new CustomStatus(ExceptionsCodes.ApiException, response.message.message, response.message.code, "accounts/getMovementsDetail");
                    return result;
                }

                result = response.convertToStandar();
                result.executeStatus = new CustomStatus();

                return result;

            } catch (CustomException cEx) {
                result.executeStatus = cEx.convertToCustomStatus();
            } catch (Exception ex) {
                result.executeStatus = new CustomException(ExceptionsCodes.UnexpectedException, "", ex.Message).convertToCustomStatus();
            }

            return result;
        }

        public override MovementsResponse GetMovements(MovementsRequest request) {
            MovementsResponse result = new MovementsResponse();

            try {

                ValoresLoginResponse loginData = doLogin();

                string urlData = "accounts/getMovementsDetail";

                var requestM = (HttpWebRequest)WebRequest.Create(config.Url + urlData);
                requestM.ContentType = "application/json";
                requestM.Method = "POST";

                if (cookieContainer != null)
                    requestM.CookieContainer = cookieContainer;
                else
                    requestM.CookieContainer = new CookieContainer();

                //CONVERT DATE
                DateTime dt = DateTime.ParseExact(request.finalDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                request.finalDate = dt.ToString("MM/dd/yyyy");

                dt = DateTime.ParseExact(request.initialDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                request.initialDate = dt.ToString("MM/dd/yyyy");

                ValoresMovementsRequest valoresMovementsRequest = new ValoresMovementsRequest();
                valoresMovementsRequest.productNumber = request.AccountId;
                valoresMovementsRequest.initialDate = request.initialDate;
                valoresMovementsRequest.finalDate = request.finalDate;

                //Valores
                valoresMovementsRequest.productId = request.productId;
                valoresMovementsRequest.currencyId = request.currencyId;

                valoresMovementsRequest.numberOfMovements = 100;
                valoresMovementsRequest.sequential = "0";
                valoresMovementsRequest.uniqueSequential = "0";
                valoresMovementsRequest.mode = "0";
                valoresMovementsRequest.serviceId = "1";

                using (var streamWriter = new StreamWriter(requestM.GetRequestStream())) {
                    string json = JsonConvert.SerializeObject(valoresMovementsRequest);
                    streamWriter.Write(json);
                }

                var httpResponse = (HttpWebResponse)requestM.GetResponse();

                ValoresMovementsReponse response = new ValoresMovementsReponse();

                string JsonResult = "";
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream())) {
                    JsonResult = streamReader.ReadToEnd();
                    response = JsonConvert.DeserializeObject<ValoresMovementsReponse>(JsonResult);
                }

                if (response.message != null) {
                    result.executeStatus = new CustomStatus(ExceptionsCodes.ApiException, response.message.message, response.message.code, "accounts/getMovementsDetail");
                    return result;
                }

                result = response.convertToStandar(request.currencyId);
                result.executeStatus = new CustomStatus();

                return result;

            } catch (FormatException cEx) {
                result.executeStatus = new CustomException(ExceptionsCodes.UnexpectedException, "", cEx.Message).convertToCustomStatus();
            } catch (CustomException cEx) {
                result.executeStatus = cEx.convertToCustomStatus();
            } catch (Exception ex) {
                result.executeStatus = new CustomException(ExceptionsCodes.UnexpectedException, "", ex.Message).convertToCustomStatus();
            }

            return result;

        }

        private CustomStatus getExecuteStatus(string JsonResult, string urlData) {
            try {
                CustomStatus bindException = JsonConvert.DeserializeObject<CustomStatus>(JsonResult);
                if (bindException.code == null) {
                    return new CustomStatus();
                } else {
                    return new CustomStatus(ExceptionsCodes.ApiException, bindException.message, bindException.code, urlData);
                }
            } catch (Exception) {
                return new CustomStatus();
            }
        }

        public override AccountsResponse GetAccount(string AccountId, string bank_id, string view_id)
        {
            AccountsResponse result = new AccountsResponse();

            result = GetAccounts();

            if (result.accounts != null) {
                Account account = new Account();
                account = result.accounts.FirstOrDefault(a => a.id == AccountId);

                result.accounts.Clear();
                result.accounts.Add(account);

                result.executeStatus = new CustomStatus();
            }
            
            return result;
        }
    }    

    internal class LoginValoresRequest {
        public string login { get; set; }
        public string password { get; set; }
        public string terminal { get; set; }
        public string channel { get; set; }
        public string identification { get; set; }
    }

    internal class test { }

}
