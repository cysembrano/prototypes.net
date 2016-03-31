using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convergys.Assist.Repository.SearchFilters
{
    public class OfflineSearchFilter : SearchFilter
    {
        public IList<int> TeamIds { get; set; } 
    }
}
