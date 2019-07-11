using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiBank.Connector.Response {

    public class  ViewsResponse : CustomStatus {
        public List<View> views { get; set; }
    }

    public class View {
        public String id { get; set; }
        public String label { get; set; }
        public String bank_id { get; set; }
        public IEnumerable<ViewsAvailable> views_available { get; set; }
    }

    public class ViewsAvailable {
        public String id { get; set; }
        public String shortName { get; set; }
        //public Boolean public { get; set; }
    }
}
