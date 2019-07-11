using ApiBank.Connector.Response;
using System.Collections.Generic;

namespace ApiBank.Connector.Banks.ValoresResponse {
    class ValoresMovementsReponse {
        public bool success { get; set; }
        public Message message { get; set; }
        public string searchMode { get; set; }
        public List<AccountStatementArray> accountStatementArray { get; set; }

        public MovementsResponse convertToStandar(int currencyId) {
            MovementsResponse response = new MovementsResponse();

            response.transactions = new List<Transactions>();

            foreach (var mov in this.accountStatementArray) {
                Transactions transaction = new Transactions();
                transaction.id = mov.transactionId + "|" + mov.sequential + "|" + mov.alternateCode;
                transaction.counterparty = new CunterParty();
                transaction.counterparty.id = mov.documentNumber;
                transaction.details = new Details();
                transaction.details.description = mov.description;
                transaction.details.posted = mov.processDate;
                transaction.details.value = new Value();
                transaction.details.value.currency = currencyId.ToString();
                transaction.details.value.amount = decimal.Parse(mov.amount.ToString());
                transaction.details.reference_number = mov.reference;
                transaction.details.new_balance = new NewBalance();
                transaction.details.new_balance.currency = currencyId.ToString();
                transaction.details.new_balance.amount = decimal.Parse(mov.availableBalance.ToString());

                response.transactions.Add(transaction);
            }

            return response;
        }

    }

    public class AccountStatementArray {
        public object account { get; set; }
        public double accountingBalance { get; set; }
        public int alternateCode { get; set; }
        public double amount { get; set; }
        public double availableBalance { get; set; }
        public object cause { get; set; }
        public object causeId { get; set; }
        public object concept { get; set; }
        public double creditsAmount { get; set; }
        public double debitsAmount { get; set; }
        public string description { get; set; }
        public string documentNumber { get; set; }
        public string hour { get; set; }
        public object image { get; set; }
        public double internationalChecksBalance { get; set; }
        public double localChecksBalance { get; set; }
        public int numberOfMovements { get; set; }
        public object office { get; set; }
        public int operationType { get; set; }
        public double ownChecksBalance { get; set; }
        public string reference { get; set; }
        public int sequential { get; set; }
        public string signDC { get; set; }
        public double totalChecksBalance { get; set; }
        public string transactionDate { get; set; }
        public object type { get; set; }
        public object typeDC { get; set; }
        public int uniqueSequential { get; set; }
        public string processDate { get; set; }
        public int transactionId { get; set; }
        public string beneficiary { get; set; }
    }

    public class Message {
        public string code { get; set; }
        public string message { get; set; }
    }
}
