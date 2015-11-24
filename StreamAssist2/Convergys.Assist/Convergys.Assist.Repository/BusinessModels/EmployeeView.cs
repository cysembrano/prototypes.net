using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Convergys.Assist.Repository.Enums;
using Newtonsoft.Json;

namespace Convergys.Assist.Repository.BusinessModels
{
    public class EmployeeView
    {
        public decimal EmpId { get; set; }
        public string EmailAddress { get; set; }
        public string NewPwd { get; set; }
        private int _roleId;
        public Nullable<int> RoleId 
        {
            get
            {
                return _roleId;
            }
            set
            {
                if (value.HasValue)
                    _roleId = value.Value;
                else
                    _roleId = 0;
            }
        }
        public string FirstName { get; set; }
        public string Team { get; set; }
        public string SiteName { get; set; }
        public string ManagerName { get; set; }
        public string AppEntitlement { get; set; }
        public string JobTitle { get; set; }
        public Nullable<int> TeamId { get; set; }
        public string Timezone { get; set; }
        public Nullable<decimal> HrDifference { get; set; }
        public Nullable<bool> TimeZoneDST { get; set; }
        public Nullable<int> TimeZoneId { get; set; }
        public Nullable<decimal> ManagerId { get; set; }
        public Nullable<int> EmpNumber { get; set; }
        public string SiteAbbreviation { get; set; }
        public Nullable<decimal> SiteId { get; set; }

        public Enum_Roles Role
        {
            get
            {
                return (Enum_Roles)Enum.ToObject(
                                                  typeof(Enum_Roles),
                                                  this.RoleId.GetValueOrDefault()
                                                 );
            }
        }

    }




}
