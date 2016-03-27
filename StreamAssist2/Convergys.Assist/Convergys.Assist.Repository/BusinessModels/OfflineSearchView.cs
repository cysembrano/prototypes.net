using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Convergys.Assist.Repository.Enums;

namespace Convergys.Assist.Repository.BusinessModels
{
    public class OfflineSearchView
    {
        public int OfflineLogId { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
        public string Team { get; set; }
        public Nullable<decimal> EmpId { get; set; }
        public Nullable<int> Status { get; set; }
        public string OfflineContactType { get; set; }
        public string OfflineActivityType { get; set; }
        public int TeamId { get; set; }
        public string CaseIdentity { get; set; }
        public string Comments { get; set; }
        public Nullable<decimal> OfflineActivityTypeId { get; set; }
        public Nullable<decimal> OfflineContactTypeId { get; set; }
        public string FirstName { get; set; }
        public Nullable<int> TotalElapsedTime { get; set; }

        public Enum_OfflineStatus OfflineStatus
        {
            get { return (Enum_OfflineStatus)Enum.ToObject(typeof(Enum_OfflineStatus), this.Status); }
        }
    }
}
