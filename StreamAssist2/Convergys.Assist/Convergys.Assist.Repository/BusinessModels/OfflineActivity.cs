using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convergys.Assist.Repository.BusinessModels
{
    public class OfflineActivity
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int? TeamId { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<System.DateTime> DateChanged { get; set; }
        public Nullable<decimal> ModifiedBy { get; set; }
    }
}
