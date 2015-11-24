using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Convergys.Assist.Repository.BusinessModels;
using Convergys.Assist.Repository.Enums;

namespace Convergys.Assist.Repository.SearchFilters
{
    public class CallbackSearchFilter : SearchFilter
    {
        public IList<int> TeamIds { get; set; } 
    }
}
