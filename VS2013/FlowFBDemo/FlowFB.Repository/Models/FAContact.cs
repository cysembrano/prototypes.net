using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowFB.Repository.Models
{
    public class FAContact
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string TrimmedNA
        {
            get
            {
                return Name.Trim() + Address.Trim();
            }
        }
    }
}
