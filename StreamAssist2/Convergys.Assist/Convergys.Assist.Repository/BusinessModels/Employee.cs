using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convergys.Assist.Repository.BusinessModels
{
    public class Employee
    {
        public decimal EmpId { get; set; }
        public Nullable<int> EmpNumber { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string NickName { get; set; }
        public Nullable<decimal> SiteId { get; set; }
        public Nullable<int> TeamId { get; set; }
        public Nullable<decimal> ManagerId { get; set; }

        public Site Site { get; set; }
        public Team Team { get; set; }

        public Employee Manager { get; set; }
    }
}
