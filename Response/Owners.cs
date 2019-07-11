using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiBank.Connector.Response {
    public class Owners {
        public String id { get; set; }
        public String display_name { get; set; }
        public String id_type { get; set; }
        public Boolean is_physical_person { get; set; }
    }
}
