using ApiBank.Connector.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiBank.Connector.Banks.ValoresResponse {
    class ValoresAccountsResponse {
        public bool success { get; set; }
        public Message message { get; set; }
        public string searchMode { get; set; }
        public List<AccountsArray> accountsArray { get; set; }

        public AccountsResponse convertToStandar() {

            AccountsResponse response = new AccountsResponse();

            response.accounts = new List<Account>();

            foreach (var acc in this.accountsArray) {

                Account account = new Account();
                account.id = acc.productNumber;
                account.label = acc.productAlias;
                account.number = acc.productId;
                account.Type = acc.productName;

                account.owners = new List<Owners>();
                Owners owner = new Owners();
                owner.id = acc.ownerId;
                owner.display_name = acc.ownerName;
                account.owners.Add(owner);

                account.balance = new Balance();
                account.balance.currency = acc.currencyId;
                account.balance.amount = acc.availableBalance;
                account.bank_id = acc.idLocalBank;

                response.accounts.Add(account);
            }

            return response;
        }

    }

    public class AccountsArray {
        public decimal availableBalance { get; set; }
        public decimal drawBalance { get; set; }
        public string currencyId { get; set; }
        public string currencySymbol { get; set; }
        public string currencyName { get; set; }
        public string productAlias { get; set; }
        public string productId { get; set; }
        public string productNumber { get; set; }
        public string productName { get; set; }
        public string productAbbreviation { get; set; }
        public decimal accountingBalance { get; set; }
        public string aliasName { get; set; }
        public string ownerName { get; set; }
        public string ownerId { get; set; }
        public string idLocalBank { get; set; }
        public string expirationDate { get; set; }
        public string rate { get; set; }
    }
}
