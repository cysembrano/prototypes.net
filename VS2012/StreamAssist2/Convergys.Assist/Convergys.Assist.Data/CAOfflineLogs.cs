//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Convergys.Assist.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class CAOfflineLogs
    {
        public int OfflineLogId { get; set; }
        public Nullable<decimal> TeamId { get; set; }
        public Nullable<decimal> OfflineActivityTypeId { get; set; }
        public Nullable<decimal> OfflineContactTypeId { get; set; }
        public string CaseIdentity { get; set; }
        public string Comments { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
        public Nullable<decimal> EmpId { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<int> TotalElapsedTime { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}
