using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowFB.Repository.Models
{
    public partial class FAPurchase
    {
        public int PurchaseID { get; set; }
        public Nullable<int> ProjectID { get; set; }
        public Nullable<int> Status { get; set; }
        public string Notes { get; set; }
        public Nullable<System.DateTime> DateChanged { get; set; }
        public Nullable<System.DateTime> Destruction { get; set; }
        public string PurchaseInvoiceNumber { get; set; }
        public string PurchaseInvoiceDescription { get; set; }
        public Nullable<decimal> PurchaseInvoiceTotal { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public Nullable<System.DateTime> DateStarted { get; set; }
        public string Comments { get; set; }
        public string ContactName { get; set; }
        public string ContactAddress { get; set; }
        public string PurchaseStatus { get; set; }
    }
}
