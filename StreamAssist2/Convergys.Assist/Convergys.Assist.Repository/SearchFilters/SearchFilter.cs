using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convergys.Assist.Repository.SearchFilters
{
    public class SearchFilter
    {
        public string Keyword { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public int? Status { get; set; }
        public int? TeamId { get; set; }
        public decimal? EmpId { get; set; }
    }
}
