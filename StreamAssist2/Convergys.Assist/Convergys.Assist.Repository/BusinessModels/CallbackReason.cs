using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convergys.Assist.Repository.BusinessModels
{
    public class CallbackReason
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int? TeamId { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public decimal? ModifiedBy { get; set; }
    }
}
