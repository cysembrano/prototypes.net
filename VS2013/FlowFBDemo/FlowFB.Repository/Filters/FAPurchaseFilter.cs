using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowFB.Repository.Filters
{
    public class FAPurchaseFilter
    {
        public string PurchaseInvoiceNumber { get; set; }
        public int ProjectID { get; set; }
        public int? Status { get; set; }
        public string TrimmedNameAddress { get; set; }
    }
}
