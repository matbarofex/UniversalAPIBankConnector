using ApiBank.Connector.Request;
using ApiBank.Connector.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiBank.Connector.Banks {
    public abstract class BankConnector {

        public ConfigRequest config;
        //public abstract AccountAlias GetAccountByCBU(string cbu);

        public Person GetBanksByCUIT(string cuit)
        {
            throw new NotImplementedException();
        }

        public abstract MovementsResponse GetMovements(MovementsRequest request);

        public abstract Person GetPersonByCUIT(string cuit);

        public abstract ViewsResponse GetViewsByBankId(string bank_id);

        public abstract AccountsResponse GetAccounts(string bank_id, string view_id);

        public abstract AccountsResponse GetAccount(string AccountId, string bank_id, string view_id);

    }
}
