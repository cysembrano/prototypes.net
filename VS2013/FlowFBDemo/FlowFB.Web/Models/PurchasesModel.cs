using FlowFB.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlowFB.Web.Models
{
    public class PurchasesModel : BaseModel
    {
        public IEnumerable<FAPurchase> Purchases { get; set; }
    }
}