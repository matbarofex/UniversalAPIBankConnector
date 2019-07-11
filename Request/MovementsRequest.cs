using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiBank.Connector.Request {
    public class MovementsRequest {

        public String AccountId { get; set; }
        public string initialDate { get; set; }
        public string finalDate { get; set; }

        //Bind
        public String bank_id { get; set; }
        public String view_id { get; set; }

        //Valores
        public int productId { get; set; }
        public int currencyId { get; set; }

        public MovementsRequest() {            
        }

    }
}
