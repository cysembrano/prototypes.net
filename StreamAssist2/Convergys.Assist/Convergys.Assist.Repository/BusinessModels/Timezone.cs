using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convergys.Assist.Repository.BusinessModels
{
    public class Timezone
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal? Offset { get; set; }
    }
}
