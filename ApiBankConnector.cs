using ApiBank.Connector.Banks;
using ApiBank.Connector.Request;
using ApiBank.Connector.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiBank.Connector {
    public class ApiBankConnector : BankConnector {

        private BankConnector _connector;

        public ApiBankConnector(ConfigRequest request)
        {
            switch (request.Bank)
            {
                case BanksEnums.Bind:
                    this._connector = new BindConnector(request);
                    break;
                case BanksEnums.Valores:
                    this._connector = new ValoresConnector(request);
                    break;
            }
        }

        public override AccountsResponse GetAccounts(string bank_id = "", string view_id = "")
        {
            return _connector.GetAccounts(bank_id, view_id);
        }
        
        public override Person GetPersonByCUIT(string cuit)
        {
            return _connector.GetPersonByCUIT(cuit);
        }

        public override ViewsResponse GetViewsByBankId(string bank_id)
        {
            return _connector.GetViewsByBankId(bank_id);
        }

        public override AccountsResponse GetAccount(string AccountId = "", string bank_id = "", string view_id = "")
        {
            return _connector.GetAccount(AccountId, bank_id, view_id);
        }

        public override MovementsResponse GetMovements(MovementsRequest request)
        {
            return _connector.GetMovements(request);
        }
    }
}
