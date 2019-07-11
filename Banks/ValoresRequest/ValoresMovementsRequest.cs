using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiBank.Connector.Banks.ValoresRequest {
    public class ValoresMovementsRequest {

        public int productId { get; set; }
        public int currencyId { get; set; }
        public string productNumber { get; set; }
        public string productAlias { get; set; }
        public int numberOfMovements { get; set; }
        public string sequential { get; set; }
        public string uniqueSequential { get; set; }
        public int dateFormatId { get; set; }
        public string mode { get; set; }
        public string serviceId { get; set; }
        public string initialDate { get; set; }
        public string finalDate { get; set; }

        public ValoresMovementsRequest() { }

        public ValoresMovementsRequest(int productId, int currencyId, string productNumber, string productAlias, int numberOfMovements, string sequential, string uniqueSequential, int dateFormatId, string mode, string serviceId, string initialDate, string finalDate) {
            this.productId = productId;
            this.currencyId = currencyId;
            this.productNumber = productNumber;
            this.productAlias = productAlias;
            this.numberOfMovements = numberOfMovements;
            this.sequential = sequential;
            this.uniqueSequential = uniqueSequential;
            this.dateFormatId = dateFormatId;
            this.mode = mode;
            this.serviceId = serviceId;
            this.initialDate = initialDate;
            this.finalDate = finalDate;
        }

    }
}
