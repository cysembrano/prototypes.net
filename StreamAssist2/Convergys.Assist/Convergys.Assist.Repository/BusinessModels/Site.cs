using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convergys.Assist.Repository.BusinessModels
{
    public partial class Site
    {
        public decimal SiteId { get; set; }
        public string SiteName { get; set; }
        public string Location { get; set; }
        public string CountryId { get; set; }
        public string SiteAbbreviation { get; set; }
        public Nullable<int> RegionId { get; set; }
        public Nullable<bool> Active { get; set; }
        public Nullable<int> SiteOffset { get; set; }

        public virtual List<Employee> Employees { get; set; }
        public virtual List<Team> Teams { get; set; }
    }
}
