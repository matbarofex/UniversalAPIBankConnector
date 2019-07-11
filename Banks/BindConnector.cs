using ApiBank.Connector.BankExceptionsModels;
using ApiBank.Connector.Request;
using ApiBank.Connector.Response;
using Newtonsoft.Json;
using Pathoschild.Http.Client;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace ApiBank.Connector.Banks {
    internal class BindConnector : BankConnector {

        public BindConnector(ConfigRequest request)
        {
            this.config = request;
        }

        public override AccountsResponse GetAccounts(string bank_id, string view_id)
        {
            AccountsResponse result = new AccountsResponse();
            try {

                LoginBindResponse loginData = doLogin();
                ///banks/322/accounts/owner
                string urlData = String.Format("banks/{0}/accounts/{1}", bank_id, view_id);

                string JsonResult = new FluentClient(config.Url)
                    .GetAsync(urlData)
                    .WithHeader("Authorization", "JWT " + loginData.token)
                    .WithOptions(true)
                    .AsString().Result;

                try {
                    result.accounts = JsonConvert.DeserializeObject<List<Account>>(JsonResult);
                } catch (Exception) { }

                result.executeStatus = getExecuteStatus(JsonResult, urlData);

            } catch (CustomException cEx) {
                result.executeStatus = cEx.convertToCustomStatus();
            } catch (Exception ex) {
                result.executeStatus = new CustomException(ExceptionsCodes.UnexpectedException, "", ex.Message).convertToCustomStatus();
            }
            return result;
        }

        public override Person GetPersonByCUIT(string cuit)
        {
            Person result = new Person();
            try {
                LoginBindResponse loginData = doLogin();

                string urlData = String.Format("persons/{0}/banks", cuit);

                string JsonResult = new FluentClient(config.Url)
                    .GetAsync(urlData)
                    .WithHeader("Authorization", "JWT " + loginData.token)
                    .WithOptions(true)
                    .AsString().Result;

                result = JsonConvert.DeserializeObject<Person>(JsonResult);
                CustomStatus bindException;

                result.executeStatus = getExecuteStatus(JsonResult, urlData);

            } catch (CustomException cEx) {
                result.executeStatus = cEx.convertToCustomStatus();
            } catch (Exception ex) {
                result.executeStatus = new CustomException(ExceptionsCodes.UnexpectedException, "", ex.Message).convertToCustomStatus();
            }
            return result;

        }

        public override ViewsResponse GetViewsByBankId(string bank_id)
        {
            ViewsResponse result = new ViewsResponse();

            try {
                LoginBindResponse loginData = doLogin();
                string urlData = String.Format("banks/{0}/accounts", bank_id);

                string JsonResult = new FluentClient(config.Url)
                    .GetAsync(urlData)
                    .WithHeader("Authorization", "JWT " + loginData.token)
                    .WithOptions(true)
                    .AsString().Result;

                result.views = JsonConvert.DeserializeObject<List<View>>(JsonResult);

                result.executeStatus = getExecuteStatus(JsonResult, urlData);

            } catch (CustomException cEx) {
                result.executeStatus = cEx.convertToCustomStatus();
            } catch (Exception ex) {
                result.executeStatus = new CustomException(ExceptionsCodes.UnexpectedException, "", ex.Message).convertToCustomStatus();
            }

            return result;
        }

        private LoginBindResponse doLogin()
        {
            try {
                LoginBindRequest login = new LoginBindRequest();
                login.username = config.User;
                login.password = config.Password;

                if (config.User == "" || config.Password == "") {
                    throw new CustomException(ExceptionsCodes.loginDataError, "Login Data Error", "Complete User and Password", null);
                }

                return new FluentClient(config.Url).PostAsync("login/jwt", login).As<LoginBindResponse>().Result;
            } catch (CustomException exC) {
                throw exC;
            }
            catch (Exception ex) {
                if (ex.InnerException.ToString().Contains("Unauthorized")) {
                    throw new CustomException(ExceptionsCodes.loginError, "Unauthorized Error", ex.Message, "login/jwt");
                }
                throw new CustomException(ExceptionsCodes.UnexpectedException, "Unexpected Error", ex.Message, "login/jwt");
            }
        }

        public override AccountsResponse GetAccount(string AccountId, string bank_id, string view_id)
        {
            AccountsResponse result = new AccountsResponse();

            try {

                LoginBindResponse loginData = doLogin();
                //banks/322/accounts/21-1-99999-4-6/owner
                string urlData = String.Format("banks/{0}/accounts/{1}/{2}", bank_id, AccountId, view_id);

                string JsonResult = new FluentClient(config.Url)
                    .GetAsync(urlData)
                    .WithHeader("Authorization", "JWT " + loginData.token)
                    .WithOptions(true)
                    .AsString().Result;

                result.accounts = new List<Account>();
                result.accounts.Add(JsonConvert.DeserializeObject<Account>(JsonResult));

                result.executeStatus = getExecuteStatus(JsonResult, urlData);

            } catch (CustomException cEx) {
                result.executeStatus = cEx.convertToCustomStatus();
            } catch (Exception ex) {
                result.executeStatus = new CustomException(ExceptionsCodes.UnexpectedException, "", ex.Message).convertToCustomStatus();
            }

            return result;
        }

        public override MovementsResponse GetMovements(MovementsRequest request)
        {
            MovementsResponse result = new MovementsResponse();

            try {
                LoginBindResponse loginData = doLogin();
                //banks/:bank_id/accounts/:account_id/:view_id/transactions
                string urlData = String.Format("banks/{0}/accounts/{1}/{2}/transactions", request.bank_id, request.AccountId, request.view_id);

                DateTime dt = DateTime.ParseExact(request.finalDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                request.finalDate = dt.ToString("yyyy-MM-dd");

                dt = DateTime.ParseExact(request.initialDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                request.initialDate = dt.ToString("yyyy-MM-dd");

                string JsonResult = new FluentClient(config.Url)
                    .GetAsync(urlData)
                    .WithHeader("Authorization", "JWT " + loginData.token)
                    .WithHeader("obp_from_date", request.initialDate)
                    .WithHeader("obp_to_date", request.finalDate)
                    .WithOptions(true)
                    .AsString().Result;

                try {
                    result.transactions = JsonConvert.DeserializeObject<List<Transactions>>(JsonResult);
                } catch {
                }

                result.executeStatus = getExecuteStatus(JsonResult, urlData);

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
                    return  new CustomStatus(ExceptionsCodes.ApiException, bindException.message, bindException.code, urlData);
                }
            } catch (Exception) {
                return new CustomStatus();
            }            
        }
    }

    internal class LoginBindResponse {
        public String token { get; set; }
        public String expires_in { get; set; }
    }

    internal class LoginBindRequest {
        public String username { get; set; }
        public String password { get; set; }
    }

}
