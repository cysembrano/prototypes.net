using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convergys.Assist.Repository.BusinessModels
{
    public class EmployeeTeamView
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public decimal EmpId { get; set; }
        public int? EmpNumber { get; set; }
        public decimal? ManagerId { get; set; }
        public string FirstName { get; set; }
        public int? RoleId { get; set; }
    }
}
